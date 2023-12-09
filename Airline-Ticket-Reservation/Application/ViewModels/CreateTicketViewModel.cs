using System.ComponentModel.DataAnnotations;
using Application.Validators;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Application.ViewModels;

public class CreateTicketViewModel
{
    public IEnumerable<Seat>? AllSeats { get; set; }
    public HashSet<int>? AvailableSeatIds { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "Passport cannot be left blank")]
    public string PassportNumber { get; set; } = null!;

    [Required(ErrorMessage = "Passport image must be uploaded")]
    public IFormFile PassportImage { get; set; } = null!;
    
    [Display(Name = "Seat")]
    [Required(ErrorMessage = "Seat must be selected")]
    [SeatAvailable]
    public int SeatId { get; set; }

    [Display(Name = "Price to pay")] 
    public double PriceToPay {get; set; }

    [HiddenInput(DisplayValue = false)]
    [FutureFlight]
    public int FlightId { get; set; }
    
}

public static class BookTicketViewModelExtensions
{
    public static CreateTicketViewModel ToCreateTicketViewModel(this Flight flight, IEnumerable<int> availableSeatIds)
    {
        return new CreateTicketViewModel
        {
            FlightId = flight.Id,
            AllSeats = flight.Seats,
            AvailableSeatIds = availableSeatIds.ToHashSet(),
            PriceToPay =  Math.Round((flight.CommissionRate ?? 0) * flight.WholeSalePrice + flight.WholeSalePrice, 2),
        };
    }
    
    public static Ticket ToTicket(this CreateTicketViewModel newTicket)
    {
        return new Ticket
        {
            PricePaid = newTicket.PriceToPay,
            PassportNumber = newTicket.PassportNumber,
            FlightId = newTicket.FlightId,
            SeatId = newTicket.SeatId,
        };
    }
}