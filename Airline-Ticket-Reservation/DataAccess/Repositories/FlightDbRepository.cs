using DataAccess.Contracts;
using DataAccess.DataContext;
using Domain.Models;

namespace DataAccess.Repositories;

public class FlightDbRepository
    : IFlights
{
    private readonly AirlineDbContext airlineDbContext;
    
    public FlightDbRepository(AirlineDbContext airlineDbContext)
    {
        this.airlineDbContext = airlineDbContext;
    }

    public IQueryable<Flight> GetFlights()
    {
        return airlineDbContext.Flights;
    }
    
    public Flight? GetFlight(int id)
    {
        return airlineDbContext.Flights.Find(id);
    }
}