using inceputproiectMds.Models.Base;
using OnlineShop.Models.DTOs;
using System.ComponentModel.DataAnnotations;

namespace inceputproiectMds.Models.Entities
{
    public class Category: BaseEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Numele categoriei este obligatoriu")]
        public string CategoryName { get; set; }

        public virtual ICollection<Product>? Products { get; set; }
    
        public Category(CategoryDTO categ)
        {
            Id = new Guid();
            CategoryName = categ.CategoryName;
            DateCreated = DateTime.Now;
            DateModified = DateTime.Now;

        }
        public Category() { }
    }

}
