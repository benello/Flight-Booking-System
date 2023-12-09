using Application.Contracts;
using DataAccess;
using DataAccess.Contracts;
using Domain.Enums;
using Domain.Models;

namespace Application.Services;

public class AdminService
    : IAdminService
{
    private readonly IFlightsRepository flightsRepository;
    private readonly ISeatsRepository seatsRepository;

    public AdminService(IFlightsRepository flightsRepo, ISeatsRepository seatsRepo)
    {
        flightsRepository = flightsRepo;
        seatsRepository = seatsRepo;
    }

    public void AddFlightWithSeats(Flight flight)
    {
        using var uow = flightsRepository.BeginTransaction();

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
    
    private SeatType CalculateSeatType(int column, int totalColumns)
    {
        if (column == 1 || column == totalColumns)
            return SeatType.Window;
        if (Convert.ToDouble(column).Equals(totalColumns / 2.0) || column == (totalColumns / 2) + 1)
            return SeatType.Aisle;
        return SeatType.Middle;
    }
}