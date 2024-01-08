using DataAccess.Contracts;
using DataAccess.DataContext;
using Domain.Models;
using Microsoft.EntityFrameworkCore.Storage;

namespace DataAccess.Repositories.Db;

public class TicketDbRepository
    : ITicketsRepository
{
    private readonly AirlineDbContext dbContext;
    
    public TicketDbRepository(AirlineDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    
    public bool Add(Ticket entity)
    {
        dbContext.Tickets.Add(entity);
        return dbContext.SaveChanges() > 0;
    }
    
    public bool AddRange(IEnumerable<Ticket> entities)
    {
        dbContext.Tickets.AddRange(entities);
        return dbContext.SaveChanges() > 0;
    }

    public bool Update(Ticket entity)
    {
        dbContext.Tickets.Update(entity);
        return dbContext.SaveChanges() > 0;
    }

    public bool Delete(Ticket entity)
    {
        dbContext.Tickets.Remove(entity);
        return dbContext.SaveChanges() > 0;
    }

    public Ticket? Get(int id) => dbContext.Tickets.Find(id);

    public IQueryable<Ticket> GetAll() => dbContext.Tickets;

    public bool TicketExists(int ticketId) => dbContext.Tickets.Any(ticket => ticket.Id == ticketId);

    public IQueryable<Ticket> GetFlightTickets(int flightId) => dbContext.Tickets.Where(ticket => ticket.FlightId == flightId);

    public IQueryable<Ticket> GetUserTickets(string passportNumber) => dbContext.Tickets.Where(ticket => ticket.PassportNumber == passportNumber);
}