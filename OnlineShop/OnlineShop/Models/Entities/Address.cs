using inceputproiectMds.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace inceputproiectMds.Models.Entities
{
    public class Address
    {
        [Key]
        public Guid AddressId { get; set; }
        public String Sector { get; set; }
        public String Street { get; set; }
        public int Number { get; set; }
        public string Others { get; set; }

        public virtual ICollection<Order>? Orders { get; set; }
        public virtual ICollection<UserAddress>? UserAddresses { get; set; }
    }
}
