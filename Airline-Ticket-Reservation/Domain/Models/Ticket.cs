using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models;

[Table(nameof(Ticket))]
public class Ticket
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [Required]
    public int Row { get; set; }
    
    [Required]
    public int Columns { get; set; }
    
    // To create a composite key, the [ForeignKey] attribute must be applied to the navigation property
    [ForeignKey($"{nameof(UserFk)}, {nameof(PassportNumber)}")]
    public virtual Passport? Passport { get; set; }
    [Required]
    public string? UserFk { get; set; }
    [Required]
    public int PassportNumber { get; set; }
    
    [Required]
    [Range(0, double.MaxValue)]
    public double PricePaid { get; set; }
    
    public bool Cancelled { get; set; }
}