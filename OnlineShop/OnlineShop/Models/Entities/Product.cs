using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using inceputproiectMds.Models.Base;

namespace inceputproiectMds.Models.Entities
{
    public class Product: BaseEntity 
    {
       

        [Required(ErrorMessage = "Titlul este obligatoriu")]
        public string Title { get; set; }

        [MaxLength(100, ErrorMessage = "Lungimea maxima trebuie sa fie de 100 caractere")]
        [Required(ErrorMessage = "Descrierea produsului este obligatorie")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Pretul produsului este obligatoriu")]
        public double Price { get; set; }

        public double Stars { get; set; }

        public string? Photo { get; set; }

        [Required(ErrorMessage = "Categoria este obligatorie")]
        public Guid ?CategoryId { get; set; }

        public Boolean Approved { get; set; }

        public Guid? UserId { get; set; }

        public int Quantity { get; set; }
        public virtual User? User { get; set; } //un produs este pus de un singur user

        public virtual Category? Category { get; set; }

        public virtual ICollection<Review>? Reviews { get; set; }

        public virtual ICollection<Cart>? Carts { get; set; }

        public virtual ICollection<ProductOrder>? Orders { get; set; }

        [NotMapped]
        public IEnumerable<SelectListItem>? Categ { get; set; }
    }
}
