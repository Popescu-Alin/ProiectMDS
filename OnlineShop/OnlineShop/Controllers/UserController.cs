using inceputproiectMds.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Models.DTOs;
using System.Security.Claims;

namespace OnlineShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;

        public UserController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        [Authorize(Roles ="Admin")]
        public List<User> Index() //get all, returneaza lista de useri
        {
            
            return _userManager.Users.ToList();

        }

        [HttpGet]
        [Authorize]
        [Route("Profile")]
        public async Task<UserDTO> Profile() //get userul logat
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);
            return new(user);
        }

        [HttpPut("Update")]
        [Authorize]
        public  async Task<IActionResult> Update(UserDTO dto){
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);
            
            var emailExist= await _userManager.FindByEmailAsync(dto.Email);
            if (emailExist != null && user.Email!=dto.Email) {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Error", Message = "Email already exists!" });
            }

            var userNameExist = await _userManager.FindByNameAsync(dto.UserName);
            if (userNameExist != null && user.UserName!=dto.UserName)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Error", Message = "UserName already exists!" });
            }

            user.UserName = dto.UserName;
            user.Email = dto.Email;

            var result=await _userManager.UpdateAsync(user);
            if(!result.Succeeded) {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Error", Message = "An error has occurred" });
            }
            return Ok("Operation done successfully");
        }

        [HttpPut("UpdateRole")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateRole(string id, string role) //update rolul unui user, doar Adminul are dreptul acesta
        {
            var user = await _userManager.FindByIdAsync(id); // userul careia ii schimb rolul
            if (user == null) //verific sa existe in baza de date
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Error", Message = "User not found" });
            }
            if (new List<string> { "Admin", "User", "Colaborator" }.Contains(role)==false) //verific sa existe rolul in baza de date
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Error", Message = "Role not found" });
            }
            
            var oldRole= await _userManager.GetRolesAsync(user); //rolul vechi

            if (oldRole != null && oldRole.Count != 0) { //verific ca lista de roluri sa nu fie goala sau nula
                var resultRemoveFromRole = await _userManager.RemoveFromRoleAsync(user, oldRole.First()); //sterg rolul vechi
                if (!resultRemoveFromRole.Succeeded)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Error", Message = "An error has occurred" });
                } 
            }

            var resultAddToRole = await _userManager.AddToRoleAsync(user, role);
            if (!resultAddToRole.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Error", Message = "An error has occurred" });
            }
            return Ok("Operation done successfully");
        }   

        [HttpDelete]
        [Authorize]
        [Route("Delete")]
        public async Task<IActionResult> Delete() //delete userul logat
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);
            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Error", Message = "An error has occurred" });
            }
            return Ok("Operation done successfully");
        }



        [HttpDelete]
        [Authorize(Roles = "Admin")]
        [Route("Delete/{id}")]
        //delete un user dupa id, doar Adminul are dreptul acesta
        public async Task<IActionResult> Delete(string id) 
        {
            var user = await _userManager.FindByIdAsync(id);
            if(user == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Error", Message = "User not found" });
            }
            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Error", Message = "An error has occurred" });
            }
            return Ok("Operation done successfully");
        }

        //test token de sters la final
        [HttpGet]
        [Route("test")]
        public List<String> Index22() //get all, returneaza lista de useri
        {
            var c = User.Claims.ToList();
            List<String> result = new List<String>();
            foreach (var item in c)
            {
                result.Add(item.ToString());
            }
            return result;

        }
    }
}
