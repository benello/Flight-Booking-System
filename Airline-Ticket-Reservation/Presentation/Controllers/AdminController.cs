using Application.Contracts;
using Application.Pagination;
using Application.ViewModels;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Airline_Ticket_Reservation.Controllers;

[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly IAdminService adminService;
    
    public AdminController(IAdminService adminService)
    {
        this.adminService = adminService;
    }
    
    // GET
    public IActionResult Index()
    {
        /*var flightsQuery = flightService.GetAllFlights();
        var ticketsQuery = ticketService.GetAllTickets();
        var statistics = new AdminStatisticsViewModel()
        {
            TotalFlights = flightsQuery.Count(),
            TotalFlightsThisMonth = flightsQuery.Count(flight => flight.DepartureDate.Month == DateTime.UtcNow.Month),
            TotalPassengers = ticketsQuery.Count(ticket => !ticket.Cancelled),
            TotalTickets = ticketsQuery.Count(),
            TotalRevenue = Math.Round(ticketsQuery.Where(ticket => !ticket.Cancelled)
                .Sum(ticket => ticket.PricePaid), 2),
        };*/

        var statistics = new AdminStatisticsViewModel();
        
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
    
    public IActionResult Flights(int page = 1, int pageSize = 10)
    {
        /*var flights = flightService.GetAllFlights()
            .ToListFlightViewModels();
        
        var paginatedFlights = flights.ToPaginationInfo(pageSize, page);*/
        
        return View(/*paginatedFlights*/);
    }
    
    public IActionResult Tickets(int flightId, int page = 1, int pageSize = 10)
    {
        /*var tickets = ticketService.GetFlightTickets(flightId)
            .ToListTicketViewModels();
        
        var paginatedTickets = tickets.ToPaginationInfo(pageSize, page);*/
        
        return View(/*paginatedTickets*/);
    }
}