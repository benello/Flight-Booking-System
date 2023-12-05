using Application.Contracts;
using Application.Pagination;
using Application.ViewModels;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Airline_Ticket_Reservation.Controllers;

//[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly IAirlineService airlineService;
    
    public AdminController(IAirlineService airlineService)
    {
        this.airlineService = airlineService;
    }
    
    // GET
    public IActionResult Index()
    {
        var flightsQuery = airlineService.GetAllFlights().AsQueryable();
        var ticketsQuery = airlineService.GetAllTickets().AsQueryable();
        var statistics = new AdminStatisticsViewModel()
        {
            TotalFlights = flightsQuery.Count(),
            TotalFlightsThisMonth = flightsQuery.Count(flight => flight.DepartureDate.Month == DateTime.UtcNow.Month),
            TotalPassengers = ticketsQuery.Count(ticket => !ticket.Cancelled),
            TotalTickets = ticketsQuery.Count(),
            TotalRevenue = ticketsQuery.Where(ticket => !ticket.Cancelled)
                .Sum(ticket => ticket.PricePaid),
        };
        
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
            airlineService.AddFlight(flight);
            TempData["Success"] = "Flight added successfully.";
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
        }
        
        return RedirectToAction(nameof(Index));
    }
    
    public IActionResult Flights(int page = 1, int pageSize = 10)
    {
        var flights = airlineService.GetAllFlights()
            .AsQueryable()
            .ToListFlightViewModels();
        
        var paginatedFlights = flights.ToPaginationInfo(pageSize, page);
        
        return View(paginatedFlights);
    }
    
    public IActionResult Tickets(int flightId, int page = 1, int pageSize = 10)
    {
        var tickets = airlineService.GetFlightTickets(flightId)
            .AsQueryable()
            .ToListTicketViewModels();
        
        var paginatedTickets = tickets.ToPaginationInfo(pageSize, page);
        
        return View(paginatedTickets);
    }
}