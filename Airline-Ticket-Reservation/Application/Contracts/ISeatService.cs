using Domain.Models;

namespace Application.Contracts;

public interface ISeatService
{
    IEnumerable<Seat> GetAvailableSeats(int flightId);
    int GetAvailableSeatsCount(int flightId);
    bool IsSeatAvailable(int seatId);
    bool SeatExists(int seatId);
    bool SeatBelongsToFlight(int seatId, int flightId);
}