using System.ComponentModel.DataAnnotations.Schema;
using Domain.Contracts;
using Microsoft.AspNetCore.Identity;

namespace Domain.Models;

public class User
    : IdentityUser, IDbModel
{
    [ForeignKey(nameof(Passport))]
    public string? PassportNumber { get; set; }
    public virtual Passport? Passport { get; set; }
    
    public User()
    { }
    
    public User(string userName)
        : base(userName)
    { }
}