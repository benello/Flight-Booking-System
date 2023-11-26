using Domain.Models;

namespace Application.Contracts;

public interface IFlightService
{
    void AddFlight(Flight flight);
    bool FlightExists(int flightId);
    Flight? GetFlight(int flightId);
    IEnumerable<Flight> GetAllFlights();
    IEnumerable<Flight> GetAvailableFlights();
}