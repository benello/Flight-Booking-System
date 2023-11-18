using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models;

[Table(nameof(Passport))]
public class Passport
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
    public string? PassportNumber { get; set; }
    
    [Required]
    public string? Image { get; set; }
}