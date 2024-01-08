using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Domain.Contracts;

namespace Domain.Models;

[Table(nameof(Passport))]
[DataContract]
public class Passport
    : IDbModel
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
    [DataMember]
    public string PassportNumber { get; set; } = null!;
    
    [DataMember]
    public string? Image { get; set; }
}