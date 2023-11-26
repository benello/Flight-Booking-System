namespace Application.Contracts;

public interface IAirlineService
    : IFlightService, ISeatService, ITicketService
{
    
}