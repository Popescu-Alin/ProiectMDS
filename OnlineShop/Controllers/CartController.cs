using inceputproiectMds.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Repositories.ProductsRepository;
using OnlineShop.Repositories.CartRepository;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace OnlineShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : Controller
    {
        
        private readonly UserManager<User> _userManager;
        private readonly IProductRepository _productRepository;
        private readonly ICartRepository _cartRepository;

        public CartController(UserManager<User> userManager, IProductRepository productRepository, ICartRepository cartRepository)
        {
            _userManager = userManager;
            _productRepository = productRepository;
            _cartRepository = cartRepository;
        }

        [HttpGet]
        [Authorize]
        public async Task<List<Cart>> GetCartUser()
        {
            var idUser = new Guid(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            
            return await _cartRepository.GetCartByUserIdAsync(idUser);

        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddToCart(Guid productId)
        {
            var product = await _productRepository.FindByIdAsync(productId);
            var userId = new Guid(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (product == null)
            {
                return NotFound("Product dose not exist");
            }

            if (product.Quantity <= 0)
            {
                return BadRequest("Product is not in stock");
            }

            Cart cartAux = await _cartRepository.GetCartByUserIdAndProductIdAsync(userId, productId);  

            if (cartAux != null) {//daca produsul exista deja in cart maresc cu 1 cantitatea 
                cartAux.Quantity += 1;
                _cartRepository.Update(cartAux);
            }
            else//altfel il adaug ca produs nou
            {
                var cart = new Cart()
                {
                    ProductId = productId,
                    UserId = userId,
                    Quantity = 1
                };
                await _cartRepository.CreateAsync(cart);
            }

            var saved = await _cartRepository.SaveAsync();
            return saved ? Ok() : BadRequest("A aparut o eroare");
        }


        [HttpPut("IncreaseQuantity{id}")]
        [Authorize]
        public async Task<IActionResult> IncreaseQuantity(Guid productId)
        {
            var product = await _productRepository.FindByIdAsync(productId);
            var userId = new Guid(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (product == null)
            {
                return NotFound("Product dose not exist");
            }

            if (product.Quantity <= 0)
            {
                return BadRequest("Product is not in stock");
            }

            Cart cartAux = await _cartRepository.GetCartByUserIdAndProductIdAsync(userId, productId);

            if (cartAux == null)
                return BadRequest("Product is not in your cart");

            cartAux.Quantity += 1;
            _cartRepository.Update(cartAux);

            var saved = await _cartRepository.SaveAsync();
            return saved ? Ok() : BadRequest("A aparut o eroare");
        }

        [HttpPut("DecreaseQuantity{id}")]
        [Authorize]
        public async Task<IActionResult> DecreaseQuantity(Guid productId)
        {
            var product = await _productRepository.FindByIdAsync(productId);
            var userId = new Guid(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (product == null)
            {
                return NotFound("Product dose not exist");
            }

            Cart cartAux = await _cartRepository.GetCartByUserIdAndProductIdAsync(userId, productId);

            if (cartAux == null)
                return BadRequest("Product is not in your cart");

            if (cartAux.Quantity <= 1)
            {
                _cartRepository.Delete(cartAux);
            }
            else
            {
                cartAux.Quantity -= 1;
                _cartRepository.Update(cartAux);
            }
            var saved = await _cartRepository.SaveAsync();
            return saved ? Ok() : BadRequest("A aparut o eroare");
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> Delete(Guid productId)
        {
            var product = await _productRepository.FindByIdAsync(productId);
            if (product == null)
            {
                return NotFound("Product dose not exist");
            }

            Guid userId = new Guid(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            Cart cart = await _cartRepository.GetCartByUserIdAndProductIdAsync(userId, productId);
            
            if (cart == null)
                return NotFound("Product is not in your cart");
            
            _cartRepository.Delete(cart);
            var saved = await _cartRepository.SaveAsync();
            return saved ? Ok() : BadRequest("A aparut o eroare");
        }
    }
}
