using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models;

[Table(nameof(Flight))]
public class Flight
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    public int Rows { get; set; }
    
    public int Columns { get; set; }
    
    public DateTime DepartureDate { get; set; }
    
    public DateTime ArrivalDate { get; set; }
    
    [Required]
    public string? CountryFrom { get; set; }
    
    [Required]
    public string? CountryTo { get; set; }
    
    [Range(0, double.MaxValue)]
    public double WholeSalePrice { get; set; }
    
    [Range(0, double.MaxValue)]
    public double? CommissionRate { get; set; }

    [ForeignKey(nameof(Seat.FlightFk))] 
    public virtual ICollection<Seat> Seats { get; set; } = null!;
}