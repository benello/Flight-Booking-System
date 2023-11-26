using System.ComponentModel.DataAnnotations;
using Application.Contracts;
using Application.Validators;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Application.ViewModels;

public class CreateTicketViewModel
{
    public IEnumerable<Seat>? AvailableSeats { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "Passport cannot be left blank")]
    public string PassportNumber { get; set; } = null!;

    [Required(ErrorMessage = "Passport image must be uploaded")]
    public IFormFile PassportImage { get; set; } = null!;
    
    [Display(Name = "Seat")]
    [Required(ErrorMessage = "Seat must be selected"), SingleSeatBooking(nameof(FlightId))]
    public int SeatId { get; set; }

    [Display(Name = "Price to pay")] 
    public double PriceToPay {get; set; }

    [HiddenInput(DisplayValue = false)]
    [FutureFlightBooking]
    public int FlightId { get; set; }
    
}

public static class BookTicketViewModelExtensions
{
    public static CreateTicketViewModel ToCreateTicketViewModel(this Flight flight, ISeatService service)
    {
        var availableSeats = service.GetAvailableSeats(flight.Id).ToArray();

        // Check if flight is full in memory since the seats are still needed for the view
        if (!availableSeats.Any())
            throw new Exception("Flight is full.");
        
        return new CreateTicketViewModel
        {
            FlightId = flight.Id,
            AvailableSeats = availableSeats,
            PriceToPay =  (flight.CommissionRate ?? 0) * flight.WholeSalePrice + flight.WholeSalePrice,
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