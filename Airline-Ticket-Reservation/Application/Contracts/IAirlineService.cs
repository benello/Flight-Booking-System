using Domain.Models;
using Microsoft.AspNetCore.Http;

namespace Application.Contracts;

public interface IAirlineService
{
    void BookTicket(Ticket ticket, IFormFile passportImage);
    void CancelTicket(int ticketId);
    bool FlightDeparted(int flightId);
    bool SeatBooked(int seatId);
    bool FlightFull(int flightId);
    Flight GetFlight(int flightId);
    IQueryable<Flight> GetAvailableFlights();
    IQueryable<Seat> GetFlightSeats(int flightId);
    IQueryable<Seat> GetAvailableSeats(int flightId);
    IQueryable<Ticket> GetFlightTickets(int flightId);
}