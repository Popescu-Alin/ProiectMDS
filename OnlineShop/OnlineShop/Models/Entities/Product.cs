using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using inceputproiectMds.Models.Base;
using OnlineShop.Models.DTOs;

namespace inceputproiectMds.Models.Entities
{
    public class Product: BaseEntity
    {
        [Key]
        public Guid ProductId { get; set; }

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
        public Guid CategoryId { get; set; }

        public Boolean Approved { get; set; }

        public Guid? UserId { get; set; }

        public int Quantity { get; set; }
        public virtual User? User { get; set; } //un produs este pus de un singur user

        public virtual Category? Category { get; set; }

        public virtual ICollection<Review>? Reviews { get; set; }

        public virtual ICollection<Cart>? Carts { get; set; }

        public virtual ICollection<ProductOrder>? Orders { get; set; }  


        public Product()
        {
        }

        public Product(ProductDTO productDTO)
        {
            ProductId = new ();
            Title = productDTO.Title;
            Description = productDTO.Description;
            Price = productDTO.Price;
            Stars = 0;
            Photo = "";
            CategoryId = productDTO.CategoryId;
            UserId = null;
            Quantity = productDTO.Quantity;
            DateCreated= DateTime.Now;
            DateModified = DateTime.Now;
        }
    }
}
