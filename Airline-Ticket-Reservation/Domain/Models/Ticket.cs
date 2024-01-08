using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Domain.Contracts;

namespace Domain.Models;

[Table(nameof(Ticket))]
[DataContract]
public class Ticket
    : IDbModel
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [DataMember]
    public int Id { get; set; }
    
    [Range(0, double.MaxValue)]
    [DataMember]
    public double PricePaid { get; set; }
    
    [DataMember]
    public bool Cancelled { get; set; }

    [ForeignKey(nameof(Passport))] 
    [DataMember]
    public string PassportNumber { get; set; } = null!;
    [IgnoreDataMember]
    public virtual Passport? Passport { get; set; }
    
    [ForeignKey(nameof(Flight))]
    [DataMember]
    public int FlightId { get; set; }

    [IgnoreDataMember]
    public virtual Flight Flight { get; set; } = null!;
    
    // Foreign key is set in DbContext
    [DataMember]
    public int? SeatId { get; set; }
    [IgnoreDataMember]
    public virtual Seat? Seat { get; set; }
}