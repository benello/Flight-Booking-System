using Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DataContext;

// Inherits from IdentityDbContext so that we can use Identity framework with this DbContext
// Custom user class is passed so that we can use our own user class instead of the default one
public class AirlineDbContext
    : IdentityDbContext<User>
{
    public DbSet<Flight> Flights { get; set; }
    
    public DbSet<Ticket> Tickets { get; set; }
    
    public DbSet<Passport> Passports { get; set; }
    
    public override DbSet<User> Users { get; set; }

    public AirlineDbContext(DbContextOptions<AirlineDbContext> options)
        : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Ticket>()
            .HasOne( ticket => ticket.Seat)
            .WithOne()
            .HasForeignKey<Ticket>(ticket => ticket.SeatFk)
            .OnDelete(DeleteBehavior.NoAction);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        // Explicitly use lazy loading for navigational properties in models
        optionsBuilder.UseLazyLoadingProxies();
    }
}