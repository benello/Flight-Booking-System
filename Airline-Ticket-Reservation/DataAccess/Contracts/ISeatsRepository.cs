using Domain.Models;

namespace DataAccess.Contracts;

public interface ISeatsRepository
    : IRepository<Seat>
{
    bool SeatExists(int seatId);
    bool SeatBelongsToFlight(int seatId, int flightId);
    IQueryable<Seat> GetFlightSeats(int flightId);
}