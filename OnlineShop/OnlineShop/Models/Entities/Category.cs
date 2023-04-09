using System.ComponentModel.DataAnnotations;

namespace inceputproiectMds.Models.Entities
{
    public class Category
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Numele categoriei este obligatoriu")]
        public string CategoryName { get; set; }

        public virtual ICollection<Product>? Products { get; set; }
    }
}
