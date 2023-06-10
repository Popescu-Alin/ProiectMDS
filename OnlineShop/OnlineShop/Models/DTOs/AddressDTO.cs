using System.Text.Json.Serialization;

namespace OnlineShop.Models.DTOs
{
    public class AddressDTO
    {
       
        public String Sector { get; set; }
        public String Street { get; set; }
        public int Number { get; set; }
        public string Others { get; set; }

        [JsonConstructor]
        public AddressDTO() { }
    }
}
