using Application.Contracts;
using Application.ViewModels;
using Application.ViewModels.Admin;
using DataAccess.Contracts;
using Domain.Enums;
using Domain.Models;

namespace Application.Services;

public class AdminService
    : IAdminService
{
    private readonly IFlightsRepository flightsRepository;
    private readonly ISeatsRepository seatsRepository;
    private readonly ITicketsRepository ticketsRepository;    private readonly ITransaction transactionProvider;

    public AdminService(IFlightsRepository flightsRepo, ISeatsRepository seatsRepo, ITicketsRepository ticketsRepo, ITransaction transactionProvider)
    {
        flightsRepository = flightsRepo;
        seatsRepository = seatsRepo;
        ticketsRepository = ticketsRepo;
        this.transactionProvider = transactionProvider;
    }

    public void AddFlightWithSeats(Flight flight)
    {
        using var uow = transactionProvider.BeginTransaction();

        try
        {
            flightsRepository.Add(flight);

            var seats = Enumerable.Range(0, flight.Rows * flight.Columns).Select(i => new Seat()
            {
                FlightId = flight.Id,
                Type = CalculateSeatType((i % flight.Columns) + 1, flight.Columns),
                RowNumber = (i / flight.Columns) + 1,
                ColumnNumber = (i % flight.Columns) + 1,
            });

            seatsRepository.AddRange(seats);
            uow.Commit();
        }
        catch
        {
            uow.Rollback();
            throw;
        }
    }

    public StatisticsViewModel GetStatistics()
    {
        var flightsQuery = flightsRepository.GetAll();
        var ticketsQuery = ticketsRepository.GetAll();
        return new StatisticsViewModel()
        {
            TotalFlights = flightsQuery.Count(),
            TotalFlightsThisMonth = flightsQuery.Count(flight => flight.DepartureDate.Month == DateTime.UtcNow.Month),
            TotalPassengers = ticketsQuery.Count(ticket => !ticket.Cancelled),
            TotalTickets = ticketsQuery.Count(),
            TotalRevenue = Math.Round(ticketsQuery.Where(ticket => !ticket.Cancelled)
                .Sum(ticket => ticket.PricePaid), 2),
        };
    }

    public Ticket GetTicket(int ticketId)
    {
        return ticketsRepository.Get(ticketId) ??
               throw new ArgumentException("Ticket does not exist", nameof(ticketId));
    }

    private static SeatType CalculateSeatType(int column, int totalColumns)
    {
        if (column == 1 || column == totalColumns)
            return SeatType.Window;
        if (Convert.ToDouble(column).Equals(totalColumns / 2.0) || column == (totalColumns / 2) + 1)
            return SeatType.Aisle;
        return SeatType.Middle;
    }
}
