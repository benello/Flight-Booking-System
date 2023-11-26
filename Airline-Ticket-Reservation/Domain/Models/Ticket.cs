using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Contracts;

namespace Domain.Models;

[Table(nameof(Ticket))]
public class Ticket
    : IDbModel
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [Range(0, double.MaxValue)]
    public double PricePaid { get; set; }
    
    public bool Cancelled { get; set; }

    [ForeignKey(nameof(Passport))] 
    public string PassportNumber { get; set; } = null!;
    public virtual Passport? Passport { get; set; }
    
    [ForeignKey(nameof(Flight))]
    public int FlightId { get; set; }

    public virtual Flight Flight { get; set; } = null!;
    
    // Foreign key is set in DbContext
    public int? SeatId { get; set; }
    public virtual Seat? Seat { get; set; }
}