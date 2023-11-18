using Domain.Models;

namespace DataAccess.Contracts;

public interface ITickets
{
    bool Book(Ticket newTicket);
    bool Cancel(int id);
    Ticket? GetTicket(int id);
}