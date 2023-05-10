using inceputproiectMds.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Repositories.CardRepository;
using OnlineShop.Repositories.UserAddressRepository;
using OnlineShop.Repositories.UserCardRepository;
using System.Security.Claims;

namespace OnlineShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserCardController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly ICardRepository _cardRepo;
        private readonly IUserCardRepository _userCardRepo;

        public UserCardController(UserManager<User> userManager, ICardRepository cardRepo, IUserCardRepository userCardRepo)
        {
            _userManager = userManager;
            _cardRepo = cardRepo;
            _userCardRepo = userCardRepo;
        }

        [HttpGet]
        [Authorize]
        public async Task<List<UserCard>> GetUserCards()
        {
            var idUser = new Guid(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            return await _userCardRepo.GetUserCardsByUserIdAsync(idUser);

        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddToUserCards(Guid cardId)
        {
            var card = await _cardRepo.FindByIdAsync(cardId);
            var userId = new Guid(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            if (card == null)
                return NotFound("Card does not exist");

            UserCard userCardAux = await _userCardRepo.GetUserCardByUserIdAndCardIdAsync(userId, cardId);

            if (userCardAux != null)
            {
                return BadRequest("Card already exists in your list");
            }
            UserCard userCard = new UserCard();
            userCard.UserId = userId;
            userCard.CardId = cardId;
            await _userCardRepo.CreateAsync(userCard);

            var saved = await _userCardRepo.SaveAsync();
            return saved ? Ok() : BadRequest("An error has occured");
        }


        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> Delete(Guid cardId)
        {
            var card = await _cardRepo.FindByIdAsync(cardId);
            var userId = new Guid(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (card == null)
                return NotFound("Card does not exist");

            UserCard userCard = await _userCardRepo.GetUserCardByUserIdAndCardIdAsync(userId, cardId);
            if (userCard == null)
            {
                return BadRequest("Card is not in your list");
            }
            _userCardRepo.Delete(userCard);
            var saved = await _userCardRepo.SaveAsync();
            return saved ? Ok() : BadRequest("An error has occured");
        }
    }
}
