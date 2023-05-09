using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OnlineShop.Models.DTOs
{
    public class ReviewDTO
    {
       

        [MaxLength(300, ErrorMessage = "Lungimea maxima trebuie sa fie de 300 caractere")]
        [Required(ErrorMessage = "Continutul este obligatoriu")]
        public string Content { get; set; }

        [Range(1, 5, ErrorMessage = "Numarul de stele trebuie sa fie intre 1 si 5")]
        public int Stars { get; set; }
        public Guid ProductId { get; set; }

        [JsonConstructor]
        public ReviewDTO() { }
    }
}
