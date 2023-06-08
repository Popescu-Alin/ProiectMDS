using System.Text.Json.Serialization;

namespace OnlineShop.Models.DTOs
{
    public class CategoryDTO
    {
        public string CategoryName { get; set; }

        [JsonConstructor]
        public CategoryDTO() { }
        
    }
}
