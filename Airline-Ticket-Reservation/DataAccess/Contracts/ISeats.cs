using Domain.Models;

namespace DataAccess.Contracts;

public interface ISeats
{
    bool AddSeat(Seat seat);
    Seat? GetSeat(int id);
    IQueryable<Seat> GetFlightSeats(int flightId);
}