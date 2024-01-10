using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using Domain.Models;
using DataAccess.Contracts;
using DataAccess.DataContext;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.Json;

public class FlightJsonRepository
    : IFlightsRepository
{
    private readonly string filePath;
    private readonly AirlineDbContext dbContext;

    public FlightJsonRepository(string filePath, AirlineDbContext dbContext)
    {
        this.filePath = filePath;
        this.dbContext = dbContext;
        this.dbContext.SavedChanges += SaveChanges;
        
        if (File.Exists(this.filePath))
        {
            using var fs = new FileStream(filePath, FileMode.Open);
            var flights = (Flight[]) (new DataContractJsonSerializer(typeof(Flight[]), new DataContractJsonSerializerSettings{EmitTypeInformation = EmitTypeInformation.Never}).ReadObject(fs) ?? Array.Empty<Flight>());
            foreach (var flight in flights)
            {
                // This is a simple approach to insert or ignore records
                if (!this.dbContext.Flights.Any(f => f.Id == flight.Id))
                    this.dbContext.Flights.Add(flight);
            }
            
            this.dbContext.SaveChanges();
        }
    }

    public bool Add(Flight entity)
    {
        dbContext.Flights.Add(entity);
        return dbContext.SaveChanges() > 0;
    }

    public bool AddRange(IEnumerable<Flight> entities)
    {
        dbContext.Flights.AddRange(entities);
        return dbContext.SaveChanges() > 0;
    }

    public bool Update(Flight entity)
    {
        dbContext.Flights.Update(entity);
        return dbContext.SaveChanges() > 0;
    }

    public bool Delete(Flight entity)
    {
        dbContext.Flights.Remove(entity);
        return dbContext.SaveChanges() > 0;
    }

    public Flight? Get(int id) => dbContext.Flights.Find(id);

    public IQueryable<Flight> GetAll() => dbContext.Flights;

    public bool FlightExists(int flightId) => dbContext.Flights.Any(flight => flight.Id == flightId);

    private void SaveChanges(object? sender, SavedChangesEventArgs? args)
    {
        using var fs = new FileStream(filePath, FileMode.OpenOrCreate);
        new DataContractJsonSerializer(typeof(Flight[]), new DataContractJsonSerializerSettings{EmitTypeInformation = EmitTypeInformation.Never}).WriteObject(fs, dbContext.Flights.ToArray());
    }
}