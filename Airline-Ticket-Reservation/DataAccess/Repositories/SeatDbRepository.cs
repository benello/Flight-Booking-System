using DataAccess.Contracts;
using DataAccess.DataContext;
using Domain.Models;

namespace DataAccess.Repositories;

public class SeatDbRepository
    : ISeats
{
    private readonly AirlineDbContext dbContext;
    
    public SeatDbRepository(AirlineDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    
    public bool AddSeat(Seat seat)
    {
        dbContext.Seats.Add(seat);

        return dbContext.SaveChanges() > 0;
    }

    public Seat? GetSeat(int id)
    {
        return dbContext.Seats.Find(id);
    }

    public IQueryable<Seat> GetFlightSeats(int flightId)
    {
        return dbContext.Seats.Where(seat => seat.FlightFk == flightId);
    }
}