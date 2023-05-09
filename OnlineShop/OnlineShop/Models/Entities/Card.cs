using inceputproiectMds.Models.Base;
<<<<<<< HEAD
using OnlineShop.Models.DTOs;
=======
>>>>>>> parent of 092e248 (Altered file structure)
using System.ComponentModel.DataAnnotations;

namespace inceputproiectMds.Models.Entities
{
<<<<<<< HEAD
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
=======
    public class Card:BaseEntity
    {
        [Key]
        public Guid CardId { get; set; }
        public DateTime ExpirationDate { get; set; }
        public virtual ICollection<UserCard>? UserCards { get; set; }
        public virtual ICollection<Order> OrderCards { get; set; }
>>>>>>> parent of 092e248 (Altered file structure)
    }
}
