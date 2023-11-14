using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models;

[Table(nameof(Passport))]
public class Passport
{
    // Composite key from userid and passport no. set in DbContext
    [ForeignKey(nameof(User))]
    public string? UserFk { get; set; }
    
    public int PassportNumber { get; set; }
    
    public virtual User? User { get; set; }
    
    [Required]
    public string? Image { get; set; }
}