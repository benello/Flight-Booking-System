using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using Domain.Models;
using DataAccess.Contracts;
using DataAccess.DataContext;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.Json;

public class SeatJsonRepository
    : ISeatsRepository
{
    private readonly string filePath;
    private readonly AirlineDbContext dbContext;
    
    public SeatJsonRepository(string filePath, AirlineDbContext dbContext)
    {
        this.filePath = filePath;
        this.dbContext = dbContext;
        this.dbContext.SavedChanges += SaveChanges;
        
        if (File.Exists(this.filePath))
        {
            using var fs = new FileStream(filePath, FileMode.Open);
            var seats = (Seat[]) (new DataContractJsonSerializer(typeof(Seat[]), new DataContractJsonSerializerSettings{EmitTypeInformation = EmitTypeInformation.Never}).ReadObject(fs) ?? Array.Empty<Seat>());
            foreach (var seat in seats)
            {
                // This is a simple approach to insert or ignore records
                if (!this.dbContext.Seats.Any(s => s.Id == seat.Id))
                    this.dbContext.Seats.Add(seat);
            }
            
            this.dbContext.SaveChanges();
        }
    }

    public bool Add(Seat entity)
    {
        dbContext.Seats.Add(entity);
        return dbContext.SaveChanges() > 0;
    }

    public bool AddRange(IEnumerable<Seat> entities)
    {
        dbContext.Seats.AddRange(entities);
        return dbContext.SaveChanges() > 0;
    }

    public bool Update(Seat entity)
    {
        dbContext.Seats.Update(entity);
        return dbContext.SaveChanges() > 0;
    }

    public bool Delete(Seat entity)
    {
        dbContext.Seats.Remove(entity);
        return dbContext.SaveChanges() > 0;
    }

    public Seat? Get(int id) => dbContext.Seats.FirstOrDefault(seat => seat.Id == id);

    public IQueryable<Seat> GetAll() => dbContext.Seats.AsQueryable();

    public bool SeatExists(int seatId) => dbContext.Seats.Any(seat => seat.Id == seatId);

    public IQueryable<Seat> GetFlightSeats(int flightId) => dbContext.Seats.Where(seat => seat.FlightId == flightId);

    public bool SeatBelongsToFlight(int seatId, int flightId) => dbContext.Seats.Any(seat => seat.Id == seatId && seat.FlightId == flightId);
    
    private void SaveChanges(object? sender, SavedChangesEventArgs? args)
    {
        using var fs = new FileStream(filePath, FileMode.OpenOrCreate);
        new DataContractJsonSerializer(typeof(Seat[]), new DataContractJsonSerializerSettings{EmitTypeInformation = EmitTypeInformation.Never}).WriteObject(fs, dbContext.Seats.ToArray());
    }
}