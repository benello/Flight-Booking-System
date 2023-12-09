using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Domain.Contracts;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Domain.Models;

[Table(nameof(Seat))]
[Index(nameof(FlightId), nameof(RowNumber), nameof(ColumnNumber), IsUnique = true)]
[DataContract]
public class Seat
    : IDbModel
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [DataMember]
    public int Id { get; set; }
    
    [ForeignKey(nameof(Flight))]
    [DataMember]
    public int FlightId { get; set; }
    
    [IgnoreDataMember]
    public virtual Flight Flight { get; set; } = null!;
    
    [DataMember]
    public int RowNumber { get; set; }
    
    [DataMember]
    public int ColumnNumber { get; set; }
    
    [DataMember]
    public SeatType Type { get; set; }
    
    // Setup in DbContext
    [IgnoreDataMember]
    public virtual Ticket? Ticket { get; set; }
}