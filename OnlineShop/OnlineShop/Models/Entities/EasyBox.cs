using inceputproiectMds.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace inceputproiectMds.Models.Entities
{
    public class EasyBox: BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
        public String Name { get; set; }
        public Guid ?AddressId { get; set; }
        public virtual Address? Address{ get; set; }
    }
}
