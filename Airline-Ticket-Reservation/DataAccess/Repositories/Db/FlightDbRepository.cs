using DataAccess.Contracts;
using DataAccess.DataContext;
using Domain.Models;
using Microsoft.EntityFrameworkCore.Storage;

namespace DataAccess.Repositories.Db;

public class FlightDbRepository
    : IFlightsRepository
{
    private readonly AirlineDbContext dbContext;
    
    public FlightDbRepository(AirlineDbContext dbContext)
    {
        this.dbContext = dbContext;
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
}