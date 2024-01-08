using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using Domain.Contracts;

namespace Domain.Models;

[Table(nameof(Flight))]
[SuppressMessage("ReSharper", "ClassWithVirtualMembersNeverInherited.Global")]
[DataContract]
public class Flight
    : IDbModel
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [DataMember]
    public int Id { get; set; }
    
    [DataMember]
    public int Rows { get; set; }
    
    [DataMember]
    public int Columns { get; set; }
    
    [DataMember]
    public DateTime DepartureDate { get; set; }
    
    [DataMember]
    public DateTime ArrivalDate { get; set; }

    [DataMember]
    public string CountryFrom { get; set; } = null!;

    [DataMember]
    public string CountryTo { get; set; } = null!;
    
    [Range(0, double.MaxValue)]
    [DataMember]
    public double WholeSalePrice { get; set; }
    
    [Range(0, double.MaxValue)]
    [DataMember]
    public double? CommissionRate { get; set; }

    // When a foreign key attribute is set on a navigation property, it informs Entity Framework that this property should
    // be mapped to a foreign key column in the class type model. (In this case, the FlightFk column in the Seat table)
    [ForeignKey(nameof(Seat.FlightId))]
    [IgnoreDataMember]
    public virtual ICollection<Seat> Seats { get; set; }
    
    [ForeignKey(nameof(Ticket.FlightId))]
    [IgnoreDataMember]
    public virtual ICollection<Ticket> Tickets { get; set; }

    [SuppressMessage("ReSharper", "VirtualMemberCallInConstructor")]
    public Flight()
    {
        Seats = new HashSet<Seat>();
        Tickets = new HashSet<Ticket>();
    }
}