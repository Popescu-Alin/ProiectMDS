using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using inceputproiectMds.Models.Base;

namespace inceputproiectMds.Models.Entities
{
    public class Order: BaseEntity
    {
        [Key]
        public Guid OrderId { get; set; }
        public Guid AddressId { get; set; }
        public Guid UserId { get; set; }
        public Guid ?CardId { get; set; }
        public double TotalSum { get; set; }

       // [MinLength(10, ErrorMessage = "O adresa completa are mai mult de 10 caractere")]
       // [Required(ErrorMessage = "Adresa este obligatorie")]
        
        public DateTime Date { get; set; }
        public virtual User? User { get; set; }
        public virtual Address? Address { get; set; }
        public virtual Card? Card { get; set; }
        public virtual ICollection<ProductOrder>? ProductOrders { get; set; }
    }
}
