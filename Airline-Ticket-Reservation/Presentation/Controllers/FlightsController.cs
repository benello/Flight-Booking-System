using Application.Contracts;
using Application.Pagination;
using Application.ViewModels;
using DataAccess.Contracts;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Airline_Ticket_Reservation.Controllers;

public class FlightsController 
    : Controller
{
    private readonly IAirlineService airlineService;
    private readonly UserManager<User> userManager;
    private readonly SignInManager<User> signInManager;
    
    public FlightsController(IAirlineService airlineService, UserManager<User> userManager, SignInManager<User> signInManager)
    {
        this.airlineService = airlineService;
        this.userManager = userManager;
        this.signInManager = signInManager;
    }
    
    public IActionResult Index(int page = 1, int pageSize = 10)
    {
        var availableFlights = airlineService.GetAvailableFlights()
            .ToListFlightViewModels();

        var paginatedFlights = availableFlights.ToPaginationInfo(pageSize, page);
        
        return View(paginatedFlights);
    }

    public IActionResult Book(int flightId)
    {
        try
        {
            var targetFlight = airlineService.GetFlight(flightId);
            var availableSeatIds = airlineService.GetAvailableSeats(flightId)
                .Select(seat => seat.Id)
                .ToHashSet();
            
            // Check if flight is full in memory since the seat ids are still needed for the view
            if (!availableSeatIds.Any())
                throw new InvalidOperationException("Flight is full.");
            
            var newTicket = targetFlight.ToCreateTicketViewModel(availableSeatIds);

            if (signInManager.IsSignedIn(User))
            {
                var passportTask = userManager.GetUserAsync(User);
                passportTask.Wait();
                var passport = passportTask.Result?.Passport;
                newTicket.PassportNumber = passport?.PassportNumber ?? string.Empty;
            }
            
            return View(newTicket);
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
            // swallow
        }
        
        return RedirectToAction(nameof(Index));
    }
    
    [HttpPost]
    public IActionResult Book(CreateTicketViewModel createTicket)
    {
        try
        {
            var ticket = createTicket.ToTicket();
            
            if (!ModelState.IsValid)
            {
                // If flight id is invalid, the flight has departed. Therefore, ticket cannot be booked
                if (ModelState.GetFieldValidationState(nameof(CreateTicketViewModel.FlightId)) == ModelValidationState.Invalid)
                    throw new ArgumentException("Flight does not exist anymore.");
                
                // If seat is invalid, reset it to 0 so that the seat dropdown is not selected
                if (ModelState.GetFieldValidationState(nameof(CreateTicketViewModel.SeatId)) == ModelValidationState.Invalid)
                    createTicket.SeatId = 0;
                
                // Do not continue if flight is full
                if (airlineService.FlightFull(createTicket.FlightId))
                    throw new ArgumentException("Flight is full.");

                createTicket.AllSeats = airlineService.GetFlightSeats(createTicket.FlightId);
                createTicket.AvailableSeatIds = airlineService.GetAvailableSeats(createTicket.FlightId)
                    .Select(seat => seat.Id)
                    .ToHashSet();
                
                return View(createTicket);
            }
            
            airlineService.BookTicket(ticket, createTicket.PassportImage);
            
            TempData["Success"] = "Ticket booked successfully.";
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
            // swallow
        }
        
        return RedirectToAction(nameof(Index));
    }
    
    public IActionResult Cancel(int ticketId)
    {
        try
        {
            airlineService.CancelTicket(ticketId);
            TempData["Success"] = "Ticket cancelled successfully.";
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
            // swallow
        }

        return RedirectToAction(nameof(Tickets), new {passportNumber = userManager.GetUserAsync(User).Result?.PassportNumber});
    }
    
    
    public IActionResult Tickets([FromServices] ITicketsRepository ticketsRepository, int page = 1, int pageSize = 10)
    {
        try
        {
            if (!signInManager.IsSignedIn(User))
            {
                TempData["warning"] = "You must be logged in to access Tickets";
                return RedirectToAction(nameof(Index));
            }

            var passportTask = userManager.GetUserAsync(User);
            passportTask.Wait();

            var passportNumber = passportTask.Result?.PassportNumber ?? string.Empty;
            var userTickets = ticketsRepository.GetUserTickets(passportNumber)
                .ToListTicketViewModels();

            var paginatedTickets = userTickets.ToPaginationInfo(pageSize, page);
            return View(paginatedTickets);
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
            // swallow
        }
        
        return RedirectToAction(nameof(Index));
    }
}