using System.Text.Json.Serialization;

namespace OnlineShop.Models.DTOs
{
    public class LoginDTO
    {
        public String Email { get; set; }
        
        public String Password { get; set; }

        [JsonConstructor]
        public LoginDTO() { }
    }
}
