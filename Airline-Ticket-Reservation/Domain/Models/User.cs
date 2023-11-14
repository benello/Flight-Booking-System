using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Domain.Models;

public class User
    : IdentityUser
{
    public User()
        : base()
    { }
    public User(string name)
        : base(name)
    {
    }
    
    // The InverseProperty attribute is applied to the navigation property in Passport to specify
    // the corresponding navigation property in Passport (i.e. User property) as the property to inverse from.
    [InverseProperty(nameof(Passport.User))]
    public virtual ICollection<Passport>? Passports { get; set; }
}