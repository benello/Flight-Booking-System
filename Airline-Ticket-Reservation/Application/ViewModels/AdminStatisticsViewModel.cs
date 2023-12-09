using System.ComponentModel.DataAnnotations;

namespace Application.ViewModels;

public class AdminStatisticsViewModel
{
    [Display(Name = "Total Flights")]
    public int TotalFlights { get; set; }
    
    [Display(Name = "Total Tickets")]
    public int TotalTickets { get; set; }
    
    [Display(Name = "Total Passengers")]
    public int TotalPassengers { get; set; }
    
    [Display(Name = "Total Revenue")]
    public double TotalRevenue { get; set; }
    
    [Display(Name = "Flights This Month")]
    public int TotalFlightsThisMonth { get; set; }
}