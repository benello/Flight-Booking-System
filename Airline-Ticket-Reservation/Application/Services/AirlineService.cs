using Application.Contracts;
using Application.Enums;
using DataAccess.Contracts;
using Domain.Models;
using Microsoft.AspNetCore.Http;

namespace Application.Services;

public class AirlineService
    : IAirlineService
{
    private readonly IFlightsRepository flightsRepository;
    private readonly ISeatsRepository seatsRepository;
    private readonly ITicketsRepository ticketsRepository;
    private readonly IPassportRepository passportRepository;
    private readonly FileService fileService;

    public AirlineService(IFlightsRepository flightsRepo, ISeatsRepository seatsRepo, ITicketsRepository ticketRepo,
            IPassportRepository passportRepo, FileService fileService)
    {
        flightsRepository = flightsRepo;
        seatsRepository = seatsRepo;
        ticketsRepository = ticketRepo;
        passportRepository = passportRepo;
        this.fileService = fileService;
    }
    
    public void BookTicket(Ticket ticket, IFormFile passportImage)
    {
        if (!flightsRepository.FlightExists(ticket.FlightId))
            throw new InvalidOperationException("Ticket cannot be booked as flight does not exist");
        
        if (!passportRepository.PassportExists(ticket.PassportNumber))
        {
            var passport = new Passport()
            {
                PassportNumber = ticket.PassportNumber,
                Image = fileService.SaveFile(passportImage, FileCategory.Passport),
            };
            passportRepository.Add(passport);   
        }
            
        ticketsRepository.Add(ticket);
    }

    public void CancelTicket(int ticketId)
    {
        var ticket = ticketsRepository.Get(ticketId)
                     ?? throw new ArgumentException("Ticket does not exist.", nameof(ticketId));
        
        ticket.Cancelled = true;
        ticket.SeatId = null;
        
        ticketsRepository.Update(ticket);
    }

    public bool FlightDeparted(int flightId)
    {
        var flight = GetFlight(flightId);

        return flight.DepartureDate <= DateTime.UtcNow;
    }

    public bool SeatBooked(int seatId)
    {
        if (!seatsRepository.SeatExists(seatId))
            throw new ArgumentException("Seat does not exist.", nameof(seatId));

        return seatsRepository.GetAll()
            .Any(seat => seat.Id == seatId && seat.Ticket != null && !seat.Ticket.Cancelled);
    }
    
    public bool FlightFull(int flightId)
    {
        var flight = GetFlight(flightId);

        var totalTickets = flight.Tickets.AsQueryable().Count(ticket => !ticket.Cancelled);
        return totalTickets >= flight.Rows * flight.Columns;
    }
    
    public Flight GetFlight(int flightId)
    {
        return flightsRepository.Get(flightId) ?? 
               throw new ArgumentException("Flight does not exist.", nameof(flightId));
    }
    
    public IQueryable<Flight> GetAvailableFlights()
    {
        return flightsRepository.GetAll().Where(flight => flight.DepartureDate > DateTime.UtcNow 
                                                    && flight.Tickets.Count(ticket => !ticket.Cancelled) < flight.Rows * flight.Columns);
    }

    public IQueryable<Seat> GetFlightSeats(int flightId)
    {
        if (flightsRepository.FlightExists(flightId))
            throw new ArgumentException("Flight does not exist", nameof(flightId));
        
        return seatsRepository.GetFlightSeats(flightId);
    }
    
    public IQueryable<Seat> GetAvailableSeats(int flightId)
    {
        var allFlightSeats = seatsRepository.GetAll().Where(seat => seat.FlightId == flightId);
        var takenSeats = allFlightSeats.Where(seat => seat.Ticket != null && !seat.Ticket.Cancelled);

        return allFlightSeats.Except(takenSeats);
    }

    public IQueryable<Ticket> GetFlightTickets(int flightId)
    {
        throw new NotImplementedException();
    }
}