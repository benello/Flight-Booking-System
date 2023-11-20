using DataAccess.Contracts;
using DataAccess.DataContext;
using Domain.Models;

namespace DataAccess.Repositories;

public class SeatDbRepository
    : IRepository<Seat>
{
    private readonly AirlineDbContext dbContext;
    
    public SeatDbRepository(AirlineDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public bool Add(Seat entity)
    {
        dbContext.Seats.Add(entity);
        return dbContext.SaveChanges() > 0;
    }

    public bool Update(Seat entity)
    {
        dbContext.Seats.Update(entity);
        return dbContext.SaveChanges() > 0;
    }

    public bool Delete(Seat entity)
    {
        dbContext.Seats.Remove(entity);
        return dbContext.SaveChanges() > 0;
    }

    public Seat? Get(int id)
    {
        return dbContext.Seats.Find(id);
    }

    public IQueryable<Seat> GetAll()
    {
        return dbContext.Seats;
    }
}