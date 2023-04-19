using inceputproiectMds.Models.Base;

namespace inceputproiectMds.Models.Entities
{
    public class UserAddress: BaseEntity
    {
        public Guid ?AddressId { get; set; }
        public Guid ?UserId { get; set; }
        public virtual Address? Address { get; set; }
        public virtual User? User { get; set; }  
    }
}
