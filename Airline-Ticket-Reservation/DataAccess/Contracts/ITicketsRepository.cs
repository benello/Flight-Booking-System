using Domain.Models;

namespace DataAccess.Contracts;

public interface ITicketsRepository
    : IRepository<Ticket, int>
{
    bool TicketExists(int ticketId);
    IQueryable<Ticket> GetFlightTickets(int flightId);
    IQueryable<Ticket> GetUserTickets(string passportNumber);
}