using DataAccess.Contracts;
using Domain.Models;
using EntityFrameworkCore.Triggered;

namespace DataAccess.Triggers;

public class CreateSeatsAfterFlightAdded
    : IAfterSaveTrigger<Flight>
{
    private IRepository<Seat> seatRepo;

    public CreateSeatsAfterFlightAdded(IRepository<Seat> seatRepo)
    {
        this.seatRepo = seatRepo;
    }
    
    public Task AfterSave(ITriggerContext<Flight> context, CancellationToken cancellationToken)
    {
        if (context.ChangeType == ChangeType.Added)
        {
            // Add seats of flight
            for (int row = 0; row < context.Entity.Rows; row++)
            {
                for (int column = 1; column <= context.Entity.Columns; column++)
                {
                    var newSeat = new Seat()
                    {
                        RowNumber = row,
                        ColumnNumber = column,
                        FlightId = context.Entity.Id,
                    };
                    seatRepo.Add(newSeat);
                }
            }
        }

        return Task.CompletedTask;
    }
}