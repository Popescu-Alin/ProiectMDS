using inceputproiectMds.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Models.DTOs
{
    public class ProductSendDTO
    {
        public Guid ProductId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public double Stars { get; set; }
        public string? Photo { get; set; }
        public Guid CategoryId { get; set; }
        public Boolean Approved { get; set; }
        public Guid? UserId { get; set; }
        public int Quantity { get; set; }
        public virtual UserDTO? User { get; set; } //un produs este pus de un singur user
        public virtual CategoryDTO? Category { get; set; }//un produs are o singura categorie

        public ProductSendDTO(Product product)
        {
            ProductId = product.ProductId;
            Title = product.Title;
            Description = product.Description;
            Price = product.Price;
            Stars = product.Stars;
            Photo = product.Photo;
            CategoryId = product.CategoryId;
            Approved = product.Approved;
            UserId = product.UserId;
            Quantity = product.Quantity;
            User = new() 
            {
                Email=product.User.Email,
                UserName=product.User.UserName          
            };
            Category = new()
            {
                CategoryName = product.Category.CategoryName
            };
        }

        public ProductSendDTO() { }
    }
}
