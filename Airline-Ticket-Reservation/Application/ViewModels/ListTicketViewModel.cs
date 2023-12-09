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

    public bool CanCancel => !Cancelled && DepartureDate > DateTime.UtcNow;
}

public static class ListTicketViewModelExtensions
{
    public static IQueryable<ListTicketViewModel> ToListTicketViewModels(this IQueryable<Ticket> ticketsQueryable)
    {
        return from ticket in ticketsQueryable
            select new ListTicketViewModel
            {
                Id = ticket.Id,
                Cancelled = ticket.Cancelled,
                PricePaid = ticket.PricePaid,
                DepartureDate = ticket.Flight.DepartureDate,
                ArrivalDate = ticket.Flight.ArrivalDate,
                CountryFrom = ticket.Flight.CountryFrom,
                CountryTo = ticket.Flight.CountryTo,
                SeatType = ticket.Seat == null ? null : ticket.Seat.Type,
            };
    }
}