
using inceputproiectMds.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using OnlineShop.Models.DTOs;
using OnlineShop.Services.UserService;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MovieTracker.Services.UserService
{
    public class UserService : IUserService
    {
        
        private readonly UserManager<User> _userManager;

        public UserService(UserManager<User> userManager)
        {
            _userManager = userManager;
            
        }

        public async Task<bool> RegisterUserAsync(RegisterDTO dto) //functia returneaza true daca s a putut adauga un user si false altfel
        {
            var registerUser = new User();
            registerUser.Email = dto.Email;
            registerUser.UserName = dto.UserName;
           
            var result = await _userManager.CreateAsync(registerUser, dto.Password);//adaug un user in baza de date
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(registerUser, "User");  //ii dau rolul de user
                return true;
            }

            return false;
        }

        public async Task<string> LoginUser(LoginDTO dto)
        {
            User user = await _userManager.FindByEmailAsync(dto.Email);

            if (user != null && await _userManager.CheckPasswordAsync(user, dto.Password))
            {

                var tokenHandler = new JwtSecurityTokenHandler();
                var token = await GenerateJwtToken(user);    
                return tokenHandler.WriteToken(token);
            }
            

            return null;
        }

        private async Task<SecurityToken> GenerateJwtToken(User user)
        {
            // Create the JWT security token handler
            var tokenHandler = new JwtSecurityTokenHandler();


            // Create the JWT signing credentials
            var secretKey = "iohwefhwefbwefwebfwenfwk";
            var key = Encoding.ASCII.GetBytes(secretKey);

            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);
            
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email)
            };
            var roles = await _userManager.GetRolesAsync(user);

            //Add roles to the claim
            foreach (var role in roles.ToList())
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = signingCredentials
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return token;

            
        }
    }
}
