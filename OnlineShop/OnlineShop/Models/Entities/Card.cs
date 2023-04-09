using inceputproiectMds.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace inceputproiectMds.Models.Entities
{
    public class Card
    {
        [Key]
        public Guid CardId { get; set; }
        public DateTime ExpirationDate { get; set; }
        public virtual ICollection<UserCard>? UserCards { get; set; }
        public virtual ICollection<Order> OrderCards { get; set; }
    }
}
