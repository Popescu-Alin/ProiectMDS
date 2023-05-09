using inceputproiectMds.Models.Base;
using Microsoft.AspNetCore.Identity;

namespace inceputproiectMds.Models.Entities
{
    public class User: IdentityUser<Guid>
    {
        public User() : base() { }


        //public virtual ICollection<Product>? Products { get; set; }
        public virtual ICollection<Cart>? Carts { get; set;}
        public virtual ICollection<Review>? Reviews { get; set;}
        public virtual ICollection<UserAddress>? UserAddresses { get; set; }
        public virtual ICollection<UserRole>? UserRoles { get; set; }    
        public virtual ICollection<UserCard>? UserCards { get; set; }


        
    }
}
