using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Contracts;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Domain.Models;

[Table(nameof(Seat))]
[Index(nameof(FlightId), nameof(RowNumber), nameof(ColumnNumber), IsUnique = true)]
public class Seat
    : IDbModel
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [ForeignKey(nameof(Flight))]
    public int FlightId { get; set; }
    public virtual Flight Flight { get; set; } = null!;
    
    public int RowNumber { get; set; }
    
    public int ColumnNumber { get; set; }
    
    public SeatType Type { get; set; }
    
    // Setup in DbContext
    public virtual Ticket? Ticket { get; set; }
    
    // This property is only used for operations, not stored in the database
    [NotMapped]
    public bool IsAvailable => Ticket?.Cancelled ?? true;
}