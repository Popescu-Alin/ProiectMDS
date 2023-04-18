using inceputproiectMds.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
