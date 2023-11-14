using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Domain.Models;

public class User
    : IdentityUser
{
    [ForeignKey(nameof(Passport))]
    public string? PassportFk { get; set; }
    public virtual Passport? Passport { get; set; }
    
    public User()
    { }
    
    public User(string userName)
        : base(userName)
    { }
}