using Domain.Models;

namespace Application.ViewModels;

public class ListFlightViewModel
{
    public int Id { get; set; }
    
    public DateTime DepartureDate { get; set; }
    
    public DateTime ArrivalDate { get; set; }

    public string CountryFrom { get; set; } = null!;

    public string CountryTo { get; set; } = null!;
    
    public int AvailableSeats { get; set; }
}

public static class ListFlightViewModelExtensions
{
    public static IQueryable<ListFlightViewModel> ToListFlightViewModels(this IQueryable<Flight> flightsQueryable)
    {
        return (from flight in flightsQueryable
            select new ListFlightViewModel
            {
                Id = flight.Id,
                DepartureDate = flight.DepartureDate,
                ArrivalDate = flight.ArrivalDate,
                CountryFrom = flight.CountryFrom,
                CountryTo = flight.CountryTo,
                // There might be a better way to do this but it does the job
                AvailableSeats = flight.Seats.Count() - flight.Tickets.Count(ticket => !ticket.Cancelled),
            }); 
    }
}