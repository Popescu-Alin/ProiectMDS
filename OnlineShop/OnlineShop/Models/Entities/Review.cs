using inceputproiectMds.Models.Base;
using OnlineShop.Models.DTOs;
using System.ComponentModel.DataAnnotations;

namespace inceputproiectMds.Models.Entities
{
    public class Review:BaseEntity
    {
        [Key]
        public Guid ReviewId { get; set; }

        [MaxLength(300, ErrorMessage = "Lungimea maxima trebuie sa fie de 300 caractere")]
        [Required(ErrorMessage = "Continutul este obligatoriu")]
        public string Content { get; set; }

        public DateTime Date { get; set; }

        public int Stars { get; set; }
        public Guid ProductId { get; set; }

        public Guid? UserId { get; set; }

        public virtual User? User { get; set; } //un review este pus de un singur user

        public virtual Product? Product { get; set; }

        public Review()
        {
        }
        public Review(ReviewDTO reviewDTO)
        {
            ReviewId = new ();
            Content = reviewDTO.Content;
            Stars = reviewDTO.Stars;
            ProductId = reviewDTO.ProductId;
            UserId =null;
            Date = DateTime.Now;
            DateCreated = DateTime.Now;
            DateModified= DateTime.Now;
        }
    }
}
