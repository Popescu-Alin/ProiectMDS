using inceputproiectMds.Models.Base;
using Microsoft.AspNetCore.Identity;

namespace inceputproiectMds.Models.Entities
{
    public class Role : IdentityRole<Guid>
    {
        public string RoleName { get; set; }
        public ICollection<UserRole> UserRoles { get; set;}
    }
}
