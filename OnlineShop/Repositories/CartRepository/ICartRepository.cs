using inceputproiectMds.Models.Entities;
using inceputproiectMds.Repositories.GenericRepository;

namespace OnlineShop.Repositories.CartRepository
{
    public interface ICartRepository: IGenericRepositoryAssociative<Cart>
    {

        public  Task<List<Cart>> GetCartByUserIdAsync(Guid userId);

        public Task<List<Cart>> GetCartsByProductIdAsync(Guid productId);

        public Task<Cart> GetCartByUserIdAndProductIdAsync(Guid userId, Guid productId);
    }
}
