using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models;

[Table(nameof(Flight))]
public class Flight
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [Required]
    public int Rows { get; set; }
    
    [Required]
    public int Columns { get; set; }
    
    [Required]
    public DateTime DepartureDate { get; set; }
    
    [Required]
    public DateTime ArrivalDate { get; set; }
    
    [Required]
    public string? CountryFrom { get; set; }
    
    [Required]
    public string? CountryTo { get; set; }
    
    [Required]
    [Range(0, double.MaxValue)]
    public double WholeSalePrice { get; set; }
    
    [Range(0, double.MaxValue)]
    public double CommissionRate { get; set; }
}