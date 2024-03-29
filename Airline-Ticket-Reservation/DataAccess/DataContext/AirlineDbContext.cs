using Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace DataAccess.DataContext;

// Inherits from IdentityDbContext so that we can use Identity framework with this DbContext
// Custom user class is passed so that we can use our own user class instead of the default one
public class AirlineDbContext
    : IdentityDbContext<User>
{
    public DbSet<Flight> Flights { get; set; } = null!;

    public DbSet<Ticket> Tickets { get; set; } = null!;

    public DbSet<Passport> Passports { get; set; } = null!;
    
    public DbSet<Seat> Seats { get; set; } = null!;

    public override DbSet<User> Users { get; set; } = null!;

    public AirlineDbContext(DbContextOptions<AirlineDbContext> options)
        : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Ticket>()
            .HasOne( ticket => ticket.Seat)
            .WithOne(seat => seat.Ticket)
            .HasForeignKey<Ticket>(ticket => ticket.SeatId)
            .OnDelete(DeleteBehavior.NoAction);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        // Explicitly use lazy loading for navigational properties in models
        optionsBuilder.UseLazyLoadingProxies();
        optionsBuilder.ConfigureWarnings(warning => warning.Ignore(InMemoryEventId.TransactionIgnoredWarning));
    }
}