using inceputproiectMds.Models.Entities;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data;
using OnlineShop.Repositories.GenericRepository;

namespace OnlineShop.Repositories.ReviewRepository
{
    public class ReviewRepository : GenericRepository<Review>, IReviewRepository
    {
        public ReviewRepository(ProiectMDSContext context) : base(context)
        {
        }

        public async Task<List<Review>> GetAllReviewsOfProductAsync(Guid id)
        {
            var reviews =  await _table.ToListAsync();
            List<Review> reviewsToSend = new List<Review>();
            foreach (var review in reviews)
            {
                review.Product = null;
                if (review.ProductId == id)
                {
                    reviewsToSend.Add(review);
                }
            }
            return reviewsToSend;
        }
    }
}
