using inceputproiectMds.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Models.DTOs;
using OnlineShop.Repositories.AddressRepository;
using OnlineShop.Repositories.CardRepository;
using OnlineShop.Repositories.CategoryRepositories;

namespace OnlineShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardController : Controller
    {

        private readonly ICardRepository _cardRepo; //tabela pe care o folosesc

        public CardController(ICardRepository repo) //constructor
        {
            _cardRepo = repo;
        }
        [HttpGet]
        [AllowAnonymous]
        public Task<List<Card>> Index() //get all, returnez toate cardurile
        {
            return _cardRepo.GetAllAsync();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> New(CardDTO dto) //adauga un card nou
        {
            await _cardRepo.CreateAsync(new Card(dto));//adauga un card nou, in clasa Card am un constructor care primeste un CardDTO
            bool saved = await _cardRepo.SaveAsync();  //salvez modificarile si verific daca e ok
            return saved ? Ok() : BadRequest("A aparut o eroare");
        }


        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid id) //sterg un card
        { 
            Card? card = await _cardRepo.FindByIdAsync(id); //obtin cardul pe care vreau sa il sterg

            if (card == null) //daca nu exista
            {
                return NotFound("Nu exista cardul");
            }

            _cardRepo.Delete(card); //sterg cardul
            bool saved = await _cardRepo.SaveAsync(); //salvez modificarile si verific daca e ok
            return saved ? Ok() : BadRequest("A aparut o eroare");
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Update(CardDTO dto, Guid id) //modific un card
        {
            Card? card = await _cardRepo.FindByIdAsync(id); //obtin cardul pe care vreau sa il modific
            if (card == null) //daca nu exista
            {
                return NotFound("Nu exista cardul");
            }
            card.CardNumber = dto.CardNumber;
            card.ExpirationDate = dto.ExpirationDate;

            _cardRepo.Update(card); //modific cardul
            bool saved = await _cardRepo.SaveAsync(); //salvez modificarile si verific daca e ok
            return saved ? Ok() : BadRequest("A aparut o eroare");
        }
    }
}
