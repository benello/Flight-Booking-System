using Application.Contracts;
using DataAccess.Contracts;
using Domain.Enums;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Services;

public class AirlineService
    : IAirlineService
{
    private readonly IRepository<Flight> flightRepo;
    private readonly IRepository<Seat> seatRepo;
    private readonly IRepository<Ticket> ticketRepo;

    public AirlineService(IRepository<Flight> flightRepo, IRepository<Seat> seatRepo, IRepository<Ticket> ticketRepo)
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
    
    public bool FlightExists(int flightId)
    {
        return flightRepo.GetAll().Any(flight => flight.Id == flightId);
    }
    
    public bool FlightFull(int flightId)
    {
        return !GetAvailableSeats(flightId)
            .AsQueryable()
            .Any();
    }
    
    public Flight? GetFlight(int flightId)
    {
        return flightRepo.Get(flightId);
    }

    public IEnumerable<Flight> GetAllFlights()
    {
        return flightRepo.GetAll();
    }

    public IEnumerable<Flight> GetAvailableFlights()
    {
         return flightRepo.GetAll()
            .Where(flight => flight.DepartureDate > DateTime.UtcNow 
                && flight.Tickets.Count(ticket => !ticket.Cancelled) < flight.Rows * flight.Columns);    
    }

    public IEnumerable<Seat> GetFlightSeats(int flightId)
    {
        return seatRepo.GetAll()
            .Where(seat => seat.FlightId == flightId);
    }
    
     public IEnumerable<Seat> GetAvailableSeats(int flightId)
    {
        var allFlightSeats = seatRepo.GetAll()
            .Where(seat => seat.FlightId == flightId);
            
        var takenSeats = ticketRepo.GetAll()
            .Where(ticket => ticket.FlightId == flightId && ticket.SeatId.HasValue)
            .Select(ticket => ticket.Seat!);

        return allFlightSeats.Except(takenSeats);
    }
    
    public int GetAvailableSeatsCount(int flightId)
    {
        return GetAvailableSeats(flightId)
            .AsQueryable()
            .Count();
    }

    public bool IsSeatAvailable(int seatId)
    {
        if (!SeatExists(seatId))
            throw new ArgumentException("Seat does not exist.");
        
        return !ticketRepo.GetAll().Any(ticket => ticket.SeatId == seatId);
    }
    
    public bool SeatExists(int seatId)
    {
        return seatRepo.GetAll().Any(seat => seat.Id == seatId);
    }
    
    public bool SeatBelongsToFlight(int seatId, int flightId)
    {
        return seatRepo.GetAll().Any(seat => seat.Id == seatId && seat.FlightId == flightId);
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