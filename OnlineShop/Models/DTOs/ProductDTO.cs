using inceputproiectMds.Models.Entities;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OnlineShop.Models.DTOs
{
    public class ProductDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public Guid CategoryId { get; set; }
        public int Quantity { get; set; }

        public IFormFile Photo { get; set; }

        [JsonConstructor]
        public ProductDTO()
        {
        }
    }
}
