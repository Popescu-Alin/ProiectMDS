using inceputproiectMds.Models.Entities;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data;
using OnlineShop.Repositories.GenericRepository;

namespace OnlineShop.Repositories.CartRepository
{
    public class CartRepository : GenericRepositoryAssociative<Cart>, ICartRepository
    {
        public CartRepository(ProiectMDSContext context) : base(context)
        {
        }

        public async Task<Cart> GetCartByUserIdAndProductIdAsync(Guid userId, Guid productId)
        {
            return  await _table.Where(x => x.UserId == userId && x.ProductId == x.ProductId).FirstOrDefaultAsync() ;
        }

        public async Task<List<Cart>> GetCartByUserIdAsync(Guid userId)
        {
            return  await _table.Where(x => x.UserId==userId).ToListAsync();
        }

        public async Task<List<Cart>> GetCartsByProductIdAsync(Guid productId)
        {
            return await _table.Where(x => x.ProductId==productId).ToListAsync();
        }
    }
}
