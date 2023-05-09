using Azure;
using inceputproiectMds.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Models.DTOs;
using OnlineShop.Services.UserService;

namespace OnlineShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserService _userService;

        public AuthenticateController(UserManager<User> userManager,IUserService userService)
        {
            _userManager = userManager;
            _userService = userService;
        }
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO dto)//inregisterez un user nou in baza de date
        {
            var userNameExists = await _userManager.FindByNameAsync(dto.UserName);
            if (userNameExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new  { Status = "Error", Message = "User already exists!" });

            var EmailExists = await _userManager.FindByEmailAsync(dto.Email);
            if (EmailExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Error", Message = "User already exists!" });


            var result = await _userService.RegisterUserAsync(dto);
            if (!result)
                return StatusCode(StatusCodes.Status500InternalServerError, new  { Status = "Error", Message = "User creation failed! Please check user details and try again." });

            return Ok(new  { Status = "Success", Message = "User created successfully!" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO dto)//generez un token daca logarea este reusita
        {
            var token = await _userService.LoginUser(dto);

            if (token == null)
            {
                return Unauthorized();
            }

            return Ok(new { token });
        }

    }
}
