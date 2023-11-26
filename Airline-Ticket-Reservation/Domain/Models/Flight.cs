using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Domain.Contracts;

namespace Domain.Models;

[Table(nameof(Flight))]
public class Flight
    : IDbModel
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    public int Rows { get; set; }
    
    public int Columns { get; set; }
    
    public DateTime DepartureDate { get; set; }
    
    public DateTime ArrivalDate { get; set; }

    public string CountryFrom { get; set; } = null!;

    public string CountryTo { get; set; } = null!;
    
    [Range(0, double.MaxValue)]
    public double WholeSalePrice { get; set; }
    
    [Range(0, double.MaxValue)]
    public double? CommissionRate { get; set; }

    // When a foreign key attribute is set on a navigation property, it informs Entity Framework that this property should
    // be mapped to a foreign key column in the class type model. (In this case, the FlightFk column in the Seat table)
    [ForeignKey(nameof(Seat.FlightId))] 
    public virtual ICollection<Seat> Seats { get; set; } = null!;

    [SuppressMessage("ReSharper", "VirtualMemberCallInConstructor")]
    public Flight()
    {
        Seats = new HashSet<Seat>();
    }
}