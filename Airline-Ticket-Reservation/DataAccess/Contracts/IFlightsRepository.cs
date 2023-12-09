using Domain.Models;

namespace DataAccess.Contracts;

public interface IFlightsRepository
    : IRepository<Flight, int>
{
    bool FlightExists(int flightId);
}