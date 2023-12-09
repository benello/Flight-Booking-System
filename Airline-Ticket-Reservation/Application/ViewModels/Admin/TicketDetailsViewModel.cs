using Domain.Models;
using Microsoft.AspNetCore.Authentication;

namespace Application.ViewModels.Admin;

public class TicketDetailsViewModel
{
    public int Id { get; set; }
    public string PassportNumber { get; set; } = null!;
    public string? PassportImage { get; set; }
    public int? SeatNumber { get; set; }
    public double Price { get; set; }
    public bool Cancelled;
}

public static class TicketDetailsViewModelExtensions
{
    public static TicketDetailsViewModel ToTicketDetailsViewModel(this Ticket ticket)
    {
            return new TicketDetailsViewModel()
            {
                Id = ticket.Id,
                PassportNumber = ticket.PassportNumber,
                PassportImage = ticket.Passport?.Image,
                SeatNumber = ticket.SeatId,
                Price = ticket.PricePaid,
                Cancelled = ticket.Cancelled,
            };
    }
}