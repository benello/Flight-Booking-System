using Domain.Models;

namespace Application.Contracts;

public interface ITicketService
{
    void BookTicket(Ticket ticket);
    void CancelTicket(int ticketId);
    IEnumerable<Ticket> GetTickets(string passportNumber);
}