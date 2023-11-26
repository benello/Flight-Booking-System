using Domain.Enums;
using Domain.Models;

namespace Application.ViewModels;

public class ListTicketViewModel
{
    public int Id { get; set; }
    
    public bool Cancelled { get; set; }
    
    public double PricePaid { get; set; }
    
    public DateTime DepartureDate { get; set; }
    
    public DateTime ArrivalDate { get; set; }
    
    public string CountryFrom { get; set; } = null!;
    
    public string CountryTo { get; set; } = null!;
    
    public SeatType? SeatType { get; set; }
}

public static class ListTicketViewModelExtensions
{
    public static ListTicketViewModel ToListTicketViewModel(this Ticket ticket)
    {
        return new ListTicketViewModel
        {
            Id = ticket.Id,
            PricePaid = ticket.PricePaid,
            Cancelled = ticket.Cancelled,
            DepartureDate = ticket.Flight.DepartureDate,
            ArrivalDate = ticket.Flight.ArrivalDate,
            CountryFrom = ticket.Flight.CountryFrom,
            CountryTo = ticket.Flight.CountryTo,
            SeatType = ticket.Seat?.Type
        };
    }
}