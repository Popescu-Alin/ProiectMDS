using inceputproiectMds.Models.Entities;
using System.Text.Json.Serialization;

namespace OnlineShop.Models.DTOs
{
    public class UserDTO
    {

        public String UserName { get; set; }
        public String Email { get; set; }

        
        public UserDTO() { }

        [JsonConstructor]
        public UserDTO(string userName, string email)
        { 
            UserName = userName;
            Email = email;
        }

        public UserDTO(User user)
        {
            UserName = user.UserName;
            Email = user.Email;
        }


    }
}
