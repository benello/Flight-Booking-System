using Application.Contracts;
using Application.Pagination;
using Application.ViewModels;
using Application.ViewModels.Admin;
using DataAccess.Contracts;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Airline_Ticket_Reservation.Controllers;

[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly IAdminService adminService;
    private readonly IAirlineService airlineService;
    
    public AdminController(IAdminService adminService, IAirlineService airlineService)
    {
        this.adminService = adminService;
        this.airlineService = airlineService;
    }
    
    // GET
    public IActionResult Index()
    {
        var statistics = adminService.GetStatistics();
        return View(statistics);
    }
    
    public IActionResult AddFlight()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult AddFlight(Flight flight)
    {
        try
        {
            adminService.AddFlightWithSeats(flight);
            TempData["Success"] = "Flight added successfully.";
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
        }
        
        return RedirectToAction(nameof(Index));
    }
    
    public IActionResult Flights([FromServices] IFlightsRepository flightsRepository, int page = 1, int pageSize = 10)
    {
        var flights = flightsRepository.GetAll()
            .ToListFlightViewModels();
        
        var paginatedFlights = flights.ToPaginationInfo(pageSize, page);
        ViewData["actionName"] = nameof(FlightDetails);
        ViewData["controller"] = "admin";
        return View(paginatedFlights);
    }
    
    public IActionResult FlightDetails(int flightId)
    {
        try
        {
            var targetFlight = airlineService.GetFlight(flightId);
            return View(targetFlight.ToFlightDetailsViewModel());
        }
        catch (Exception ex)
        {
            TempData["error"] = ex.Message;
            // swallow
        }

        return RedirectToAction(nameof(Index));
    }

    public IActionResult TicketDetails(int ticketId, [FromServices] ITicketsRepository ticketsRepository)
    {
        try
        {
            var ticket = adminService.GetTicket(ticketId);
            return View(ticket.ToTicketDetailsViewModel());
        }
        catch (Exception ex)
        {
            TempData["error"] = ex.Message;
            // swallow
        }

        return RedirectToAction(nameof(Index));
    }
}