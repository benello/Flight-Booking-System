using Domain.Models;

namespace Application.Contracts;

public interface IFlightService
{
    void AddFlight(Flight flight);
    bool IsFlightFull(int flightId);
    bool IsSeatAvailable(int seatId);
    Flight? GetFlight(int flightId);
    IEnumerable<Flight> GetAllFlights();
    IEnumerable<Flight> GetAvailableFlights();
    void BookTicket(Ticket ticket);
    void CancelTicket(int ticketId);
    IEnumerable<Ticket> GetTickets(string passportNumber);
}