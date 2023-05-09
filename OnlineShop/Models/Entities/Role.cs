using inceputproiectMds.Models.Base;
using Microsoft.AspNetCore.Identity;

namespace inceputproiectMds.Models.Entities
{
    public class Role : IdentityRole<Guid>
    {
        public ICollection<UserRole> UserRoles { get; set;}
    }
}
