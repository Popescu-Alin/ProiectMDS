using System.Text.Json.Serialization;

namespace OnlineShop.Models.DTOs
{
    public class CardDTO
    {
        public string CardNumber { get; set; }
        public DateTime ExpirationDate { get; set; }

        [JsonConstructor]
        public CardDTO() { }
    }
}
