using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Contracts;

namespace Domain.Models;

[Table(nameof(Passport))]
public class Passport
    : IDbModel
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
    public string PassportNumber { get; set; } = null!;
    
    public string? Image { get; set; }
}