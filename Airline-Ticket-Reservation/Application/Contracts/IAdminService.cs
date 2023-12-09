using Domain.Models;

namespace Application.Contracts;

public interface IAdminService
{
    public void AddFlightWithSeats(Flight flight);
}