using Application.Contracts;
using Domain.Models;

namespace Application.ViewModels;

public class ListFlightViewModel
{
    // TODO: Add properties to display in the view
    public int Id { get; set; }
    
    public DateTime DepartureDate { get; set; }
    
    public DateTime ArrivalDate { get; set; }

    public string CountryFrom { get; set; } = null!;

    public string CountryTo { get; set; } = null!;
    
    public int AvailableSeats { get; set; }
}

public static class ListFlightViewModelExtensions
{
    public static ListFlightViewModel ToListFlightViewModel(this Flight flight, ISeatService? flightService = null)
    {
        return new ListFlightViewModel
        {
            Id = flight.Id,
            DepartureDate = flight.DepartureDate,
            ArrivalDate = flight.ArrivalDate,
            CountryFrom = flight.CountryFrom,
            CountryTo = flight.CountryTo,
            AvailableSeats = flightService?.GetAvailableSeatsCount(flight.Id) ?? 0,
        };
    }
}