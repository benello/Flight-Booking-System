using DataAccess.Contracts;
using DataAccess.DataContext;
using Domain.Models;
using Microsoft.EntityFrameworkCore.Storage;

namespace DataAccess.Repositories;

public class PassportDbRepository
    : IPassportRepository
{
    private readonly AirlineDbContext dbContext;
    
    public PassportDbRepository(AirlineDbContext dbContext)
    {
        this.dbContext = dbContext;
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

    public Passport? Get(int id) => dbContext.Passports.Find(id);

    public IQueryable<Passport> GetAll() => dbContext.Passports;

    public bool PassportExists(string passportNumber) => dbContext.Passports.Any(ticket => ticket.PassportNumber == passportNumber);
    
    public IDbContextTransaction BeginTransaction() => dbContext.Database.BeginTransaction();
}