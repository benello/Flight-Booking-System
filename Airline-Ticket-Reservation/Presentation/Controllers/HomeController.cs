using Airline_Ticket_Reservation.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using DataAccess.Contracts;
using Domain.Models;

namespace Airline_Ticket_Reservation.Controllers
{
    public class HomeController : Controller
    {
        private ITickets ticketRepo;
        private IFlights flightRepo;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ITickets ticketRepo, IFlights flightRepo, ILogger<HomeController> logger)
        {
            this.ticketRepo = ticketRepo;
            this.flightRepo = flightRepo;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}