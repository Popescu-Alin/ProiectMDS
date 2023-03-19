using inceputproiectMds.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace inceputproiectMds.Models.Entities
{
    public class Review:BaseEntity
    {
       

        [MaxLength(300, ErrorMessage = "Lungimea maxima trebuie sa fie de 300 caractere")]
        [Required(ErrorMessage = "Continutul este obligatoriu")]
        public string Content { get; set; }

        public DateTime Date { get; set; }

        public int Stars { get; set; }
        public Guid? ProductId { get; set; }

        public Guid? UserId { get; set; }

        public virtual User? User { get; set; } //un review este pus de un singur user

        public virtual Product? Product { get; set; }
    }
}
