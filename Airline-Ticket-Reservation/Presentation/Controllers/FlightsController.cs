using Application.Contracts;
using Application.Enums;
using Application.Extensions;
using Application.Services;
using Application.ViewModels;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Airline_Ticket_Reservation.Controllers;

public class FlightsController 
    : Controller
{
    private readonly IAirlineService airlineService;
    private readonly IPassportService passportService;
    private readonly UserManager<User> userManager;
    private readonly SignInManager<User> signInManager;

    
    public FlightsController(IAirlineService airlineService, IPassportService passportService,
        UserManager<User> userManager, SignInManager<User> signInManager)
    {
        this.airlineService = airlineService;
        this.passportService = passportService;
        this.userManager = userManager;
        this.signInManager = signInManager;
    }
    
    // GET
    public IActionResult Index(int page = 1, int pageSize = 10)
    {
        var availableFlights = airlineService.GetAvailableFlights()
            .Paginate(page, pageSize)
            .Select(flight => flight.ToListFlightViewModel(airlineService));
        
        return View(availableFlights);
    }

    public IActionResult Book(int flightId)
    {
        try
        {
            var flight = airlineService.GetFlight(flightId);
            if (flight == null)
                throw new Exception("Flight does not exist.");
            
            var availableSeats = airlineService.GetAvailableSeats(flight.Id).ToArray();

            // Check if flight is full in memory since the seats are still needed for the view
            if (!availableSeats.Any())
                throw new Exception("Flight is full.");
            
            var newTicket = flight.ToCreateTicketViewModel(availableSeats);
            
            return View(newTicket);
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
        }
        
        return RedirectToAction(nameof(Index));
    }
    
    [HttpPost]
    public IActionResult Book(CreateTicketViewModel ticketToAdd, Passport passport, [FromServices] FileService fileService)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                // If flight id is invalid, the flight has departed. Therefore, ticketed cannot be booked
                if (ModelState.GetFieldValidationState(nameof(CreateTicketViewModel.FlightId)) == ModelValidationState.Invalid)
                    throw new ArgumentException("Flight does not exist anymore.");
                
                // If seat is invalid, reset it to 0 so that the seat dropdown is not selected
                if (ModelState.GetFieldValidationState(nameof(CreateTicketViewModel.SeatId)) == ModelValidationState.Invalid)
                    ticketToAdd.SeatId = 0;
                
                var availableSeats = airlineService.GetAvailableSeats(ticketToAdd.FlightId).ToArray();
                // Check if flight is full in memory since the seats are still needed for the view
                if (!availableSeats.Any())
                    throw new ArgumentException("Flight is full.");

                ticketToAdd.AvailableSeats = availableSeats;
                return View(ticketToAdd);
            }

            if (!passportService.PassportExists(ticketToAdd.PassportNumber))
            {
                var relativePath = fileService.SaveFile(ticketToAdd.PassportImage, FileCategory.Passport);
                passport.Image = relativePath;
                passportService.AddPassport(passport);   
            }
            
            //ticketToAdd.PassportNumber = passport.PassportNumber;
            airlineService.BookTicket(ticketToAdd.ToTicket());
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
        }
        
        return RedirectToAction(nameof(Index));
    }
    
    public IActionResult Cancel(int ticketId)
    {
        try
        {
            airlineService.CancelTicket(ticketId);
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
        }

        return RedirectToAction(nameof(Tickets), new {passportNumber = userManager.GetUserAsync(User).Result.PassportNumber});
    }
    
    
    public IActionResult Tickets(int page = 1, int pageSize = 10)
    {
        if (!signInManager.IsSignedIn(User))
        {
            return RedirectToAction(nameof(Index));
        }

        var passportNumber = userManager.GetUserAsync(User).Result.PassportNumber ?? string.Empty;
        var tickets = airlineService.GetTickets(passportNumber)
            .Paginate(page, pageSize)
            .Select(ticket => ticket.ToListTicketViewModel()).ToList();
        
        return View(tickets);
    }
}