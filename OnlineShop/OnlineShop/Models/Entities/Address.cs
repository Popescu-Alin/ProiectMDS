using inceputproiectMds.Models.Base;
using OnlineShop.Models.DTOs;
using System.ComponentModel.DataAnnotations;

namespace inceputproiectMds.Models.Entities
{
    public class Address:BaseEntity
    {
        [Key]
        public Guid AddressId { get; set; }
        public String Sector { get; set; }
        public String Street { get; set; }
        public int Number { get; set; }
        public string Others { get; set; }

        public virtual ICollection<Order>? Orders { get; set; }
        public virtual ICollection<UserAddress>? UserAddresses { get; set; }

        public Address() { }
        public Address(AddressDTO address)
        {
            AddressId = new Guid();
            Sector = address.Sector;
            Street = address.Street;
            Number = address.Number;
            Others = address.Others;
            DateCreated = DateTime.Now;
            DateModified = DateTime.Now;
        }
    }
}
