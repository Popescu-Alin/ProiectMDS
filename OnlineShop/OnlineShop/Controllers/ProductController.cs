using inceputproiectMds.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Repositories.CardRepository;
using OnlineShop.Repositories.ProductsRepository;
using System.Data;

namespace OnlineShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IProductOrderRepository _productRepo;
       

        public ProductController(IProductOrderRepository repo)
        {
            _productRepo = repo;
        }
        [HttpGet]

        
        public Task<List<Product>> Index()
        {
            return _productRepo.GetAllAsync();
        }
    }
}
