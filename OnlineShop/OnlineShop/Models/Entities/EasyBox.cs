using inceputproiectMds.Models.Base;

namespace inceputproiectMds.Models.Entities
{
    public class EasyBox: BaseEntity
    {
        public String Name { get; set; }
        public Guid ?AddressId { get; set; }
        public virtual Address? Address{ get; set; }
    }
}
