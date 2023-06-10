using inceputproiectMds.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Repositories.AddressRepository;
using OnlineShop.Repositories.UserAddressRepository;
using System.Security.Claims;

namespace OnlineShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAddressController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IAddressRepository _addressRepo;
        private readonly IUserAddressRepository _userAddressRepo;

        public UserAddressController(UserManager<User> userManager, IAddressRepository addressRepo, IUserAddressRepository userAddressRepo)
        {
            _userManager = userManager;
            _addressRepo = addressRepo;
            _userAddressRepo = userAddressRepo;
        }

        [HttpGet]
        [Authorize]
        public async Task<List<UserAddress>> GetUserAddresses()
        {
            var idUser = new Guid(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            return await _userAddressRepo.GetUseAddressByUserIdAsync(idUser);

        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddToUserAddress(Guid addressId)
        {
            var address = await _addressRepo.FindByIdAsync(addressId);
            var userId = new Guid(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            
            if (address == null)
                return NotFound("Address dose not exist");
            
            UserAddress userAddressAux = await _userAddressRepo.GetUseAddresstByUserIdAndAddressIdAsync(userId, addressId);
            
            if (userAddressAux != null)
            {
                return BadRequest("Address is already exists in your list");
            }
            UserAddress userAddress = new UserAddress();
            userAddress.UserId = userId;
            userAddress.AddressId = addressId;
            await _userAddressRepo.CreateAsync(userAddress);

            var saved = await _userAddressRepo.SaveAsync();
            return saved ? Ok() : BadRequest("A aparut o eroare");
        }


        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> Delete(Guid addressId)
        {
            var address = await _addressRepo.FindByIdAsync(addressId);
            var userId = new Guid(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (address == null)
                return NotFound("Address dose not exist");

            UserAddress userAddress = await _userAddressRepo.GetUseAddresstByUserIdAndAddressIdAsync(userId, addressId);   
            if (userAddress == null)
            {
                return BadRequest("Address is not in your list");
            }
            _userAddressRepo.Delete(userAddress);
            var saved = await _userAddressRepo.SaveAsync();
            return saved ? Ok() : BadRequest("A aparut o eroare");
        }
    }
}
