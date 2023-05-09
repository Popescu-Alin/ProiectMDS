using inceputproiectMds.Models.Base;
using OnlineShop.Models.DTOs;
using System.ComponentModel.DataAnnotations;

namespace inceputproiectMds.Models.Entities
{
    public class Card : BaseEntity
    {
        [Key]
        public Guid CardId { get; set; }
        public string CardNumber { get; set; }
        public DateTime ExpirationDate { get; set; }
        public virtual ICollection<UserCard>? UserCards { get; set; }
        public virtual ICollection<Order> OrderCards { get; set; }
        public Card() { }
        public Card(CardDTO card)
        {
            CardId = new Guid();
            CardNumber = card.CardNumber;
            ExpirationDate = card.ExpirationDate;
            DateCreated = DateTime.Now;
            DateModified = DateTime.Now;
        }
    }
}
