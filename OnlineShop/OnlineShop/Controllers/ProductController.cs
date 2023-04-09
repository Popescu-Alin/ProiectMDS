using inceputproiectMds.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Repositories.CardRepository;
using OnlineShop.Repositories.ProductsRepository;

namespace OnlineShop.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductsRepository _productRepo;
       

        public ProductController(IProductsRepository repo)
        {
            _productRepo = repo;
        }
        public Task<List<Product>> Index()
        {
            return _productRepo.GetAllAsync();
        }
    }
}
