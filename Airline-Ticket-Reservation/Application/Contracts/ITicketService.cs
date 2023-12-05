using Domain.Models;

namespace Application.Contracts;

public interface ITicketService
{
    void BookTicket(Ticket ticket);
    void CancelTicket(int ticketId);
    IEnumerable<Ticket> GetFlightTickets(int flightId);
    IEnumerable<Ticket> GetAllTickets();
    IEnumerable<Ticket> GetUserTickets(string passportNumber);
}