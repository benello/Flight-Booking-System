using Domain.Models;

namespace DataAccess.Contracts;

public interface IFlightsRepository
    : IRepository<Flight>
{
    bool FlightExists(int flightId);
}