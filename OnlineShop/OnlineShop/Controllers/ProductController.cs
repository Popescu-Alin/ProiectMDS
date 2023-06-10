using inceputproiectMds.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Models.DTOs;
using OnlineShop.Models.Entities;
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
        public async Task<ProductList> Index()//returneaza toate produsele din magazin
        {
            var products = await _productRepo.GetAllAsync();
            foreach (Product x in products) {
                await AddCategoryAndUser(x);
            }
            return new()
            {
                Products = products.Select(x => new ProductSendDTO(x)).ToList(),
                Total = await _productRepo.GetNumberOfProducts()
            };
        }


        [HttpGet]
        [Route("GetAllRange")]
        [AllowAnonymous]
        public async Task<ProductList> Index(int offset, int limit,bool sorted)//returneaza toate produsele din magazin din anumit interval,sortate dupa pret
        {
            var products = await _productRepo.GetAllAsync(offset, limit,sorted);
            foreach (Product x in products)
            {
                await AddCategoryAndUser(x);
            }
            return new() { Products=products.Select(x => new ProductSendDTO(x)).ToList(),
                            Total=await _productRepo.GetNumberOfProducts()};
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Colaborator")]
        public async Task<IActionResult> New([FromForm] ProductDTO dto)//adaug un produs in magazin
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
        public async Task<IActionResult> Update([FromForm] ProductDTO dto ,Guid id)//fac update la un produs din magazin
        {
            var product = await _productRepo.FindByIdAsync(id);

            if (product == null) //verific sa existe produsul cu un anumit id
                return NotFound("No product with that id");
            

            if (product.UserId.ToString() != User.FindFirst(ClaimTypes.NameIdentifier).Value)//verific daca produsul apartine userului care face update
                return Unauthorized("You don't have acces to this product");

            var img = dto.Photo;

            product.Quantity=dto.Quantity;
            product.Price=dto.Price;
            product.Description=dto.Description;
            product.CategoryId=dto.CategoryId;
            product.Title=dto.Title;

           

            if (img != null) {
                DeleteImgAsync(product.Photo);//sterg imaginea veche
                product.Photo= await SaveImgAsync(img);//o modific cu cea noua
            }
            
            _productRepo.Update(product);
            bool saved = await _productRepo.SaveAsync(); //salvez modificarile si verific daca e ok
            return saved ? Ok() : BadRequest("A aparut o eroare");
        }



        [HttpDelete]
        [Route("Delete{id}")]
        [Authorize(Roles = "Admin,Colaborator")]
        public async Task<IActionResult> Delete(Guid id)//sterg un produs din magazin
        {
            var product = await _productRepo.FindByIdAsync(id);
            if (product == null)   //verific ca prousul cu un anumit id sa existe
                return NotFound("No product with this id");


            if (product.UserId.ToString() != User.FindFirst(ClaimTypes.NameIdentifier).Value)//verific daca produsul apartine userului care face update
                return Unauthorized("You don't have acces to this product");

            DeleteImgAsync(product.Photo);//sterg poza
            _productRepo.Delete(product);//sterg prousul
            bool saved = await _productRepo.SaveAsync(); //salvez modificarile si verific daca e ok
            return saved ? Ok() : BadRequest("A aparut o eroare");
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("Get{id}")]
        public async Task<IActionResult> Get(Guid id)//returneaza un produs din magazin
        {
            var product = await _productRepo.FindByIdAsync(id);
            if (product == null) //verific sa existe produsul cu un anumit id
                return NotFound("No product with that id");
            
            await AddCategoryAndUser(product);
            return Ok(new ProductSendDTO(product));
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
