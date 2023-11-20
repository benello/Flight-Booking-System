using Application.Contracts;
using Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Airline_Ticket_Reservation.Controllers;

public class FlightsController : Controller
{
    private readonly IFlightService flightService;
    
    public FlightsController(IFlightService flightService)
    {
        this.flightService = flightService;
    }
    
    // GET
    public IActionResult Index()
    {
        var availableFlights = flightService.GetAvailableFlights()
                                .Select(flight => flight.ToListFlightViewModel());
        return View(availableFlights);
    }
}