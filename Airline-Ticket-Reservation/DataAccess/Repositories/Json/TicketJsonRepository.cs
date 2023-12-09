using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using Domain.Models;
using DataAccess.Contracts;
using DataAccess.DataContext;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.Json;

public class TicketJsonRepository
    : ITicketsRepository
{
    private readonly string filePath;
    private readonly AirlineDbContext dbContext;

    public TicketJsonRepository(string filePath, AirlineDbContext dbContext)
    {
        this.filePath = filePath;
        this.dbContext = dbContext;
        this.dbContext.SavedChanges += SaveChanges;
        
        if (File.Exists(this.filePath))
        {
            using var fs = new FileStream(filePath, FileMode.Open);
            var tickets = (Ticket[]) (new DataContractJsonSerializer(typeof(Ticket[]), new DataContractJsonSerializerSettings{EmitTypeInformation = EmitTypeInformation.Never}).ReadObject(fs) ?? Array.Empty<Ticket>());
            foreach (var ticket in tickets)
            {
                // This is a simple approach to insert or ignore records
                if (!this.dbContext.Tickets.Any(t => t.Id == ticket.Id))
                    this.dbContext.Tickets.Add(ticket);
            }
        }
    }

    public bool Add(Ticket entity)
    {
        dbContext.Tickets.Add(entity);
        return dbContext.SaveChanges() > 0;
    }

    public bool AddRange(IEnumerable<Ticket> entities)
    {
        dbContext.Tickets.AddRange(entities);   
        return dbContext.SaveChanges() > 0;
    }

    public bool Update(Ticket entity)
    {
        dbContext.Tickets.Update(entity);
        return dbContext.SaveChanges() > 0;
    }

    public bool Delete(Ticket entity)
    {
        dbContext.Tickets.Remove(entity);
        return dbContext.SaveChanges() > 0;
    }

    public Ticket? Get(int id) => dbContext.Tickets.FirstOrDefault(ticket => ticket.Id == id);

    public IQueryable<Ticket> GetAll() => dbContext.Tickets;

    public bool TicketExists(int ticketId) => dbContext.Tickets.Any(ticket => ticket.Id == ticketId);

    public IQueryable<Ticket> GetFlightTickets(int flightId) => dbContext.Tickets.Where(ticket => ticket.FlightId == flightId).AsQueryable();

    public IQueryable<Ticket> GetUserTickets(string passportNumber) => dbContext.Tickets.Where(ticket => ticket.PassportNumber == passportNumber).AsQueryable();

    private void SaveChanges(object? sender, SavedChangesEventArgs? args)
    {
        using var fs = new FileStream(filePath, FileMode.OpenOrCreate);
        new DataContractJsonSerializer(typeof(Ticket[]), new DataContractJsonSerializerSettings{EmitTypeInformation = EmitTypeInformation.Never}).WriteObject(fs, dbContext.Tickets.ToArray());
    }
}