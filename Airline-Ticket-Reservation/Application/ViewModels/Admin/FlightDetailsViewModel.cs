using System.ComponentModel.DataAnnotations;
using Domain.Models;

namespace Application.ViewModels.Admin;

public class FlightDetailsViewModel
{
    [Display(Name = "Flight Id")]
    public int Id { get; set; }
    [Display(Name = "Country From")]
    public string CountryFrom { get; set; } = null!;
    [Display(Name = "Country To")]
    public string CountryTo { get; set; } = null!;
    [Display(Name = "Departure Date")]
    public DateTime DepartureDate { get; set; }
    [Display(Name = "Arrival Date")]
    public DateTime ArrivalDate { get; set; }
    public IEnumerable<int> TicketIds { get; set; } = null!;
}

public static class FlightDetailsViewModelExtensions
{
    public static FlightDetailsViewModel ToFlightDetailsViewModel(this Flight flight)
    {
        return new FlightDetailsViewModel()
        {
            Id = flight.Id,
            CountryFrom = flight.CountryFrom,
            CountryTo = flight.CountryTo,
            DepartureDate = flight.DepartureDate,
            ArrivalDate = flight.ArrivalDate,
            TicketIds = flight.Tickets.AsQueryable().Select(ticket => ticket.Id)
        };
    }
}