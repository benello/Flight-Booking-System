using Application.Contracts;
using DataAccess.Contracts;
using Domain.Enums;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Services;

public class FlightService
    : IFlightService
{
    private readonly IRepository<Flight> flightRepo;
    private readonly IRepository<Seat> seatRepo;
    private readonly IRepository<Ticket> ticketRepo;

    public FlightService(IRepository<Flight> flightRepo, IRepository<Seat> seatRepo, IRepository<Ticket> ticketRepo)
    {
        this.flightRepo = flightRepo;
        this.seatRepo = seatRepo;
        this.ticketRepo = ticketRepo;
    }

    public void AddFlight(Flight flight)
    {
        var flightAdded = flightRepo.Add(flight);
        if (!flightAdded)
            throw new DbUpdateException("Flight could not be added.");

        // Add seats of flight
        for (int row = 1; row <= flight.Rows; row++)
        {
            for (int column = 1; column <= flight.Columns; column++)
            {
                SeatType seatType;
                if (column == 1 || column == flight.Columns)
                    seatType = SeatType.Window;
                else if (column == (flight.Columns / 2) || column == (flight.Columns / 2)+1)
                    seatType = SeatType.Aisle;
                else
                    seatType = SeatType.Middle;
                
                var newSeat = new Seat()
                {
                    FlightId = flight.Id,
                    Type = seatType,
                    RowNumber = row,
                    ColumnNumber = column,
                };
                seatRepo.Add(newSeat);
            }
        }
    }
    
    public bool IsFlightFull(int flightId)
    {
        var flightSeats = seatRepo.GetAll().Where(seat => seat.FlightId == flightId) ?? throw new ArgumentException("Flight does not exist.", nameof(flightId));
        return flightSeats.Any(seat => IsSeatAvailable(seat.Id));
    }

    public bool IsSeatAvailable(int seatId)
    {
        var seat = seatRepo.Get(seatId) ?? throw new ArgumentException("Seat does not exist.", nameof(seatId));
        return seat.IsAvailable;
    }

    public Flight? GetFlight(int flightId)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Flight> GetAllFlights()
    {
        return flightRepo.GetAll();
    }

    public IEnumerable<Flight> GetAvailableFlights()
    {
        // IsFlightFull() is not used here because the query would have to be executed with only half of the filers since
        // ef core cannot translate the IsFlightFull() method to sql and therefore would have to load partially filtered flights into memory
         return flightRepo.GetAll()
            .Where(flight => flight.DepartureDate > DateTime.UtcNow && flight.Seats.Any(seat => seat.Ticket == null));    
    }

    public void BookTicket(Ticket ticket)
    {  
        if (ticket.SeatId.HasValue && !IsSeatAvailable(ticket.SeatId.Value))
            throw new ArgumentException("Seat is not available.", nameof(ticket.SeatId));
        
        var ticketAdded = ticketRepo.Add(ticket);
        if (!ticketAdded)
            throw new DbUpdateException("Ticket could not be added.");
    }

    public void CancelTicket(int ticketId)
    {
        var ticket = ticketRepo.Get(ticketId) ?? throw new ArgumentException("Ticket does not exist.", nameof(ticketId));
        
        ticket.Cancelled = true;
        ticket.SeatId = null;
        
        var ticketUpdated = ticketRepo.Update(ticket);
        if (!ticketUpdated)
            throw new DbUpdateException("Ticket could not be updated.");
    }

    public IEnumerable<Ticket> GetTickets(string passportNumber)
    {
        return ticketRepo.GetAll().Where(ticket => ticket.PassportNumber == passportNumber);
    }
}