using inceputproiectMds.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Models.DTOs;
using OnlineShop.Repositories.AddressRepository;
using OnlineShop.Repositories.CategoryRepositories;

namespace OnlineShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : Controller
    {

        private readonly IAddressRepository _addressRepo; //tabela pe care o folosesc

        public AddressController(IAddressRepository repo) //constructor
        {
            _addressRepo = repo;
        }
        [HttpGet]
        [AllowAnonymous]
        public Task<List<Address>> Index() //get all, returneaza lista de categorii
        {
            return _addressRepo.GetAllAsync();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> New(AddressDTO dto) //adauga o categorie noua
        {
            
            
            _addressRepo.CreateAsync(new Address(dto)); //adauga o categorie noua, in clasa Category am un constructor care primeste un CategoryDTO
            bool saved = await _addressRepo.SaveAsync();  //salvez modificarile si verific daca e ok
            return saved ? Ok() : BadRequest("A aparut o eroare");
            

            return BadRequest();

        }


        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid id) //sterg o categorie
        {
            
         
            Address? address = await _addressRepo.FindByIdAsync(id); //obtin categoria pe care vreau sa o sterg

            if (address == null) //daca nu exista
            {
                return NotFound("Nu exista categoria");
            }

            _addressRepo.Delete(address); //sterg categoria
            bool saved = await _addressRepo.SaveAsync(); //salvez modificarile si verific daca e ok
            return saved ? Ok() : BadRequest("A aparut o eroare");
            
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Update(AddressDTO dto, Guid id) //modific o categorie
        {
            Address? address = await _addressRepo.FindByIdAsync(id); //obtin categoria pe care vreau sa o modific
            if (address == null) //daca nu exista
            {
                return NotFound("Nu exista categoria");
            }
            address.Sector = dto.Sector;
            address.Street = dto.Street;
            address.Number = dto.Number;
            address.Others = dto.Others;

            _addressRepo.Update(address); //modific categoria
            bool saved = await _addressRepo.SaveAsync(); //salvez modificarile si verific daca e ok
            return saved ? Ok() : BadRequest("A aparut o eroare");
        }
    }
}
