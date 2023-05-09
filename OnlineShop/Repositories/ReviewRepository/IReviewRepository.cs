using inceputproiectMds.Models.Entities;
using inceputproiectMds.Repositories.GenericRepository;

namespace OnlineShop.Repositories.ReviewRepository
{
    public interface IReviewRepository: IGenericRepository<Review>
    {

        public  Task<List<Review>> GetAllReviewsOfProductAsync(Guid id);
    }
}
