using Domain.Models;

namespace DataAccess.Contracts;

public interface IFlights
{
    Flight? GetFlight(int id);
    IQueryable<Flight> GetFlights();
}