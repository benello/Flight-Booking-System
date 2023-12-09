using DataAccess.Contracts;
using DataAccess.DataContext;
using Domain.Models;
using Microsoft.EntityFrameworkCore.Storage;

namespace DataAccess.Repositories;

public class SeatDbRepository
    : ISeatsRepository
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
    
    public bool AddRange(IEnumerable<Seat> entities)
    {
        dbContext.Seats.AddRange(entities);
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

    public Seat? Get(int id) => dbContext.Seats.Find(id);

    public IQueryable<Seat> GetAll() => dbContext.Seats;

    public bool SeatExists(int seatId) => dbContext.Seats.Any(seat => seat.Id == seatId);
    
    public IQueryable<Seat> GetFlightSeats(int flightId) => dbContext.Seats.Where(seat => seat.FlightId == flightId);
    
    public bool SeatBelongsToFlight(int seatId, int flightId) => dbContext.Seats.Any(seat => seat.Id == seatId
                                                                    && seat.FlightId == flightId);

    public IDbContextTransaction BeginTransaction() => dbContext.Database.BeginTransaction();
}