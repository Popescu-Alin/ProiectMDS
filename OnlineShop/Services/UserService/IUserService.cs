
using OnlineShop.Models.DTOs;

namespace OnlineShop.Services.UserService
{
    public interface IUserService
    {
        Task<bool> RegisterUserAsync(RegisterDTO dto);
        Task<string> LoginUser(LoginDTO dto);
    }
}

