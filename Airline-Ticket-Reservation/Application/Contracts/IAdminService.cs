using Application.ViewModels;
using Application.ViewModels.Admin;
using Domain.Models;

namespace Application.Contracts;

public interface IAdminService
{
    void AddFlightWithSeats(Flight flight);
    StatisticsViewModel GetStatistics();
    Ticket GetTicket(int ticketId);
}