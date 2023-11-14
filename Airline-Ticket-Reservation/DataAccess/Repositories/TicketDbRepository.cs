using DataAccess.DataContext;
using Domain.Contracts;

namespace DataAccess.Repositories;

public class TicketDbRepository
    : ITickets
{
    private AirlineDbContext airlineDbContext;
    
    public TicketDbRepository(AirlineDbContext airlineDbContext)
    {
        this.airlineDbContext = airlineDbContext;
    }
    
    public void Book()
    {
        throw new NotImplementedException();
    }

    public void Cancel()
    {
        throw new NotImplementedException();
    }

    public void GetTicket()
    {
        throw new NotImplementedException();
    }
}