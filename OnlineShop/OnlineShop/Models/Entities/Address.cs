using inceputproiectMds.Models.Base;

namespace inceputproiectMds.Models.Entities
{
    public class Address: BaseEntity
    {
        public String Sector { get; set; }
        public String Street { get; set; }
        public int Number { get; set; }
        public string Others { get; set; }

        public virtual ICollection<Order>? Orders { get; set; }
        public virtual ICollection<UserAddress>? UserAddresses { get; set; }
    }
}
