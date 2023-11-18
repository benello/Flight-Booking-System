using DataAccess.Contracts;
using DataAccess.DataContext;
using Domain.Models;

namespace DataAccess.Repositories;

public class TicketDbRepository
    : ITickets
{
    private readonly AirlineDbContext airlineDbContext;
    
    public TicketDbRepository(AirlineDbContext airlineDbContext)
    {
        this.airlineDbContext = airlineDbContext;
    }
    
    public bool Book(Ticket newTicket)
    {
        airlineDbContext.Tickets.Add(newTicket);
        return airlineDbContext.SaveChanges() > 0;
    }

    public bool Cancel(int id)
    {
        var ticket = GetTicket(id);
        
        if (ticket == null)
        {
            return false;
        }

        ticket.Cancelled = true;
        return airlineDbContext.SaveChanges() > 0;
    }

    public Ticket? GetTicket(int id)
    {
        return airlineDbContext.Tickets.Find(id);
    }
}