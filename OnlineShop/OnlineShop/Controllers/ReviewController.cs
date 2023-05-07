using inceputproiectMds.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Models.DTOs;
using OnlineShop.Repositories.ProductsRepository;
using OnlineShop.Repositories.ReviewRepository;
using System.Security.Claims;

namespace OnlineShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : Controller
    {
        private readonly IReviewRepository _reviewRepo;
        private readonly UserManager<User> _userManager;
        private readonly IProductRepository _productRepository;
        


        public ReviewController(IReviewRepository reviewRepository, UserManager<User> userManager,IProductRepository productRepository)
        {
            _reviewRepo = reviewRepository;
            _userManager = userManager;
            _productRepository = productRepository;
        }


        [HttpGet]
        [Route("Get{id}")]
        [AllowAnonymous]
        public async Task<List<ReviewSendDTO>?> Get(Guid id)//o functie care returneaza o lista de review-uri ale unui produs, id-ul produsului este dat ca parametru
        {
            var product = await _productRepository.FindByIdAsync(id);
            
            if (product == null)
            {
                return null; //daca nu exista acest produs se va intoarce null.
            }
            var reviwes = await _reviewRepo.GetAllReviewsOfProductAsync(id);
            foreach (var review in reviwes)
            {
                await AddUser(review);
            }
            return  reviwes.Select(x=>new ReviewSendDTO(x)).ToList();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> New(ReviewDTO dto)
        {
            var review =  new Review(dto);
            review.UserId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            if (ModelState.IsValid)
            {
                await _reviewRepo.CreateAsync(review);
                bool saved = await _reviewRepo.SaveAsync();  //salvez modificarile si verific daca e ok
                return saved ? Ok() : BadRequest("A aparut o eroare");
            }

            //daca nu e modelul valid
            return BadRequest();
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> Delete(Guid id)
        {
            var review = await _reviewRepo.FindByIdAsync(id);
            if (review == null)
            {
                return NotFound("Review does not exist");
            }
            var prod = await _productRepository.FindByIdAsync(review.ProductId);

            //daca userul curent nu e admin, sau produsul la care a fost lasat reviewul nu este al userului  sau nu
            //este review-ul userului curent  atunci nu are drepturi de delete.
            if (!(User.FindFirst(ClaimTypes.NameIdentifier).Value==review.UserId.ToString() ||User.IsInRole("Admin") ||
                User.FindFirst(ClaimTypes.NameIdentifier).Value==prod.UserId.ToString()))
            {
                return Unauthorized("You don't have acces to this Review");
            }
            
            _reviewRepo.Delete(review);
            var result = await _reviewRepo.SaveAsync();

            return result ? Ok("Succes") : BadRequest("An erroor has occured");

        }


        [NonAction]
        private async Task<bool> AddUser(Review rev)
        {
            try {
                rev.User = await _userManager.FindByIdAsync(rev.UserId.ToString());
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
