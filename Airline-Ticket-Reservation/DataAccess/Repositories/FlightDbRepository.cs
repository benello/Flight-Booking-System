using DataAccess.Contracts;
using DataAccess.DataContext;
using Domain.Models;

namespace DataAccess.Repositories;

public class FlightDbRepository
    : IRepository<Flight>
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

    public Flight? Get(int id)
    {
        return dbContext.Flights.Find(id);
    }

    public IQueryable<Flight> GetAll()
    {
        return dbContext.Flights;
    }
}