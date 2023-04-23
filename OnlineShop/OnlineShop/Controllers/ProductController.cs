using inceputproiectMds.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Models.DTOs;
using OnlineShop.Repositories.CategoryRepositories;
using OnlineShop.Repositories.ProductsRepository;
using System.Data;
using System.Security.Claims;

namespace OnlineShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepo;
        private readonly ICategoryRepository _categoryRepo;
        private readonly UserManager<User> _userManager;



        public ProductController(IProductRepository repo, ICategoryRepository repoCateg, UserManager<User> userMan)
        {
            _productRepo = repo;
            _categoryRepo = repoCateg;
            _userManager = userMan;
        }
        [HttpGet]
        [Route("GetAll")]
        [AllowAnonymous]
        public async Task<List<ProductSendDTO>> Index()//returneaza toate produsele din magazin
        {
            var products = await _productRepo.GetAllAsync();
            foreach (Product x in products) {
                await AddCategoryAndUser(x);
            }
            return products.Select(x => new ProductSendDTO(x)).ToList();
        }


        [HttpGet]
        [Route("GetAllRange")]
        [AllowAnonymous]
        public async Task<List<ProductSendDTO>> Index(int offset, int limit)//returneaza toate produsele din magazin din anumit interval
        {
            var products = await _productRepo.GetAllAsync(offset, limit);
            foreach (Product x in products)
            {
                await AddCategoryAndUser(x);
            }
            return products.Select(x => new ProductSendDTO(x)).ToList();
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Colaborator")]
        public async Task<IActionResult> Create([FromForm] ProductDTO dto)
        {

            var product = new Product(dto);
            product.UserId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var img = dto.Photo;

            //pozele pot fi dor png sau jpg sau jpeg
            if (new List<String>() { ".jpg", ".jpeg", ".png" }.Contains(Path.GetExtension(img.FileName).ToLower()) == false)
            {
                return BadRequest();
            }

            //daca imaginea nu e nula o salvez si o adaug in baza de date
            if (img != null)
            {
                product.Photo = await SaveImgAsync(img);
            }

            //adaug produsul in baza de date
            if (ModelState.IsValid)
            {
                await _productRepo.CreateAsync(product);
                bool saved = await _productRepo.SaveAsync();  //salvez modificarile si verific daca e ok
                return saved ? Ok() : BadRequest("A aparut o eroare");
            }
            //daca nu e modelul valid
            return BadRequest();
        }


        [HttpPut]
        [Authorize(Roles = "Admin,Colaborator")]
        public async Task<IActionResult> Update([FromForm] ProductDTO dto ,Guid id)
        {
            var product = await _productRepo.FindByIdAsync(id);

            if (product == null) 
                return NotFound("No product with that id");
            

            if (product.UserId.ToString() != User.FindFirst(ClaimTypes.NameIdentifier).Value)
                return Unauthorized("You don't have acces to this product");

            var img = dto.Photo;

            product.Quantity=dto.Quantity;
            product.Price=dto.Price;
            product.Description=dto.Description;
            product.CategoryId=dto.CategoryId;
            product.Title=dto.Title;

            DeleteImgAsync(product.Photo);

            if (img != null) {
                product.Photo= await SaveImgAsync(img);
            }
            else
            {
                product.Photo = "";
            }

            _productRepo.Update(product);
            bool saved = await _productRepo.SaveAsync(); //salvez modificarile si verific daca e ok
            return saved ? Ok() : BadRequest("A aparut o eroare");
        }



        [HttpDelete]
        [Authorize(Roles = "Admin,Colaborator")]
        
        public async Task<IActionResult> Delete(Guid id)
        {
            var product = await _productRepo.FindByIdAsync(id);
            if ( product == null)
                return NotFound("No product with this id");
            

            if(product.UserId.ToString()!= User.FindFirst(ClaimTypes.NameIdentifier).Value)
                return Unauthorized("You don't have acces to this product");

            DeleteImgAsync(product.Photo);
            _productRepo.Delete(product);
            bool saved = await _productRepo.SaveAsync(); //salvez modificarile si verific daca e ok
            return saved ? Ok() : BadRequest("A aparut o eroare");
        }

        [NonAction]
        private async Task<bool> AddCategoryAndUser(Product x)
        {
            try
            {
                x.Category = await _categoryRepo.FindByIdAsync(x.CategoryId);
                x.User = await _userManager.FindByIdAsync(x.UserId.ToString());
                
                return true;

            }catch (Exception ex)
            {
                return false;
            }
        }

        [NonAction]
        private static async Task<string> SaveImgAsync(IFormFile img)
        {
            string extension = Path.GetExtension(img.FileName);// iau extensia imaginii
            string name = Path.GetFileNameWithoutExtension(img.FileName);//iau numele imaginii fara extensie
            string fileName = name + DateTime.Now.ToString("dd_MM_yy_HHmmssfff") + extension;//generez numele imaginii
            
            //generez calea imaginii, acesta fiind salvat in folderul Public/images sub denmirea de nume+data(zi_luna_an_ora_minute_secunde_milisecunde)+extensie
            string path = Path.Combine(Directory.GetCurrentDirectory(), "Public","images",//folderul unde se salveaza
                                        fileName);//numele imaginii
            //salvez imaginea
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await img.CopyToAsync(stream);
            }
            //returnez locatiaimaginii penru a o salva in baza de date
            return Path.Combine("Public", "images",fileName);
        }

        [NonAction]
        private static void DeleteImgAsync(string fileName)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(),fileName);

            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
        }
    }
}
