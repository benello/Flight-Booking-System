using DataAccess.Contracts;
using DataAccess.DataContext;
using Domain.Models;

namespace DataAccess.Repositories;

public class TicketDbRepository
    : IRepository<Ticket>
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

    public Ticket? Get(int id)
    {
        return dbContext.Tickets.Find(id);
    }

    public IQueryable<Ticket> GetAll()
    {
        return dbContext.Tickets;
    }
}