using DataAccess.Contracts;
using Domain.Enums;
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
            var batchSize = 100;
            var seats = new List<Seat>();
            for (int row = 0; row < context.Entity.Rows; row++)
            {
                for (int column = 0; column < context.Entity.Columns; column++)
                {
                    // batch insert
                    
                    
                    SeatType seatType;
                    if (column == 1 || column == context.Entity.Columns)
                        seatType = SeatType.Window;
                    else if (column == (context.Entity.Columns / 2) || column == (context.Entity.Columns / 2)+1)
                        seatType = SeatType.Aisle;
                    else
                        seatType = SeatType.Middle;
                
                    var newSeat = new Seat()
                    {
                        FlightId = context.Entity.Id,
                        Type = seatType,
                        RowNumber = row,
                        ColumnNumber = column,
                    };
                    seats.Add(newSeat);
                }
                
                if (seats.Count >= batchSize)
                {
                    seatRepo.AddRange(seats);
                    seats.Clear();
                }
            }

            if (seats.Count > 0)
            {
                seatRepo.AddRange(seats);
            }
        }

        return Task.CompletedTask;
    }
}