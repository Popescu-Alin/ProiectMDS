using inceputproiectMds.Models.Base;

namespace inceputproiectMds.Models.Entities
{
    public class Card:BaseEntity
    {
        public DateTime ExpirationDate { get; set; }
        public virtual ICollection<UserCard>? UserCards { get; set; }
        public virtual ICollection<Order> OrderCards { get; set; }
    }
}
