using inceputproiectMds.Models.Entities;
using inceputproiectMds.Repositories.GenericRepository;

namespace OnlineShop.Repositories.CartRepository
{
<<<<<<< HEAD
    public interface ICartRepository: IGenericRepositoryAssociative<Cart>
    {

        public  Task<List<Cart>> GetCartByUserIdAsync(Guid userId);

        public Task<List<Cart>> GetCartsByProductIdAsync(Guid productId);

        public Task<Cart> GetCartByUserIdAndProductIdAsync(Guid userId, Guid productId);
=======
    public interface ICartRepository: IGenericRepository<Cart>
    {
>>>>>>> parent of 092e248 (Altered file structure)
    }
}
