using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using Domain.Models;
using DataAccess.Contracts;
using DataAccess.DataContext;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.Json;

public class PassportJsonRepository
    : IPassportRepository
{
    private readonly string filePath;
    private readonly AirlineDbContext dbContext;

    public PassportJsonRepository(string filePath, AirlineDbContext dbContext)
    {
        this.filePath = filePath;
        this.dbContext = dbContext;
        this.dbContext.SavedChanges += SaveChanges;

        if (File.Exists(this.filePath))
        {
            using var fs = new FileStream(filePath, FileMode.Open);
            var passports = (IEnumerable<Passport>) (new DataContractJsonSerializer(typeof(Passport[]), new DataContractJsonSerializerSettings{EmitTypeInformation = EmitTypeInformation.Never}).ReadObject(fs) ?? Array.Empty<Passport>());
            
            foreach (var passport in passports)
            {
                // This is a simple approach to insert or ignore records
                if (!this.dbContext.Passports.Any(p => p.PassportNumber == passport.PassportNumber))
                    this.dbContext.Passports.Add(passport);
            }
            this.dbContext.SaveChanges();
        }
    }

    public bool Add(Passport entity)
    {
        dbContext.Passports.Add(entity);
        return dbContext.SaveChanges() > 0;
    }

    public bool AddRange(IEnumerable<Passport> entities)
    {
        dbContext.Passports.AddRange(entities);
        return dbContext.SaveChanges() > 0;
    }

    public bool Update(Passport entity)
    {
        dbContext.Passports.Update(entity);
        return dbContext.SaveChanges() > 0;
    }

    public bool Delete(Passport entity)
    {
        dbContext.Passports.Remove(entity);
        return dbContext.SaveChanges() > 0;
    }

    public Passport? Get(string id) => dbContext.Passports.FirstOrDefault(passport => passport.PassportNumber == id);

    public IQueryable<Passport> GetAll() => dbContext.Passports.AsQueryable();

    public bool PassportExists(string passportNumber) => dbContext.Passports.Any(passport => passport.PassportNumber == passportNumber);
    
    private void SaveChanges(object? sender, SavedChangesEventArgs? args)
    {
        using var fs = new FileStream(filePath, FileMode.OpenOrCreate);
        new DataContractJsonSerializer(typeof(Passport[]), new DataContractJsonSerializerSettings{EmitTypeInformation = EmitTypeInformation.Never}).WriteObject(fs, dbContext.Passports.ToArray());
    }
}