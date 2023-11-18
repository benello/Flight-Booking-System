using DataAccess.DataContext;
using Domain.Contracts;

namespace DataAccess.Repositories;

public class FlightDbRepository
    : IFlights
{
    private AirlineDbContext airlineDbContext;
    
    public FlightDbRepository(AirlineDbContext airlineDbContext)
    {
        this.airlineDbContext = airlineDbContext;
    }
    
    public void GetFlight()
    {
        throw new NotImplementedException();
    }

    public void GetFlights()
    {
        throw new NotImplementedException();
    }
}