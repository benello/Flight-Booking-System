using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models;

[Table(nameof(Ticket))]
public class Ticket
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [Range(0, double.MaxValue)]
    public double PricePaid { get; set; }
    
    public bool Cancelled { get; set; }
    
    [ForeignKey(nameof(Passport))]
    public string? PassportFk { get; set; }
    public virtual Passport? Passport { get; set; }
    
    [ForeignKey(nameof(Flight))]
    public int FlightFk { get; set; }
    public virtual Flight? Flight { get; set; }
    
    // Foreign key is set in DbContext
    public int SeatFk { get; set; }
    public virtual Seat Seat { get; set; } = null!;
}