using System.Text.Json.Serialization;

namespace OnlineShop.Models.DTOs
{
    public class RegisterDTO
    {
        public String Email { get; set; }
        public String UserName { get; set; }
        public String Password { get; set; }

        [JsonConstructor]
        public RegisterDTO() { }

    }
}
