using inceputproiectMds.Models.Entities;
<<<<<<< HEAD:OnlineShop/Repositories/CartRepository/CartRepository.cs
<<<<<<< HEAD
using Microsoft.EntityFrameworkCore;
=======
>>>>>>> 092e24880e1fba1f81168a843069f81a1c063986
=======
>>>>>>> parent of d5dc1de (AllFromAlinNeedMoreTestes):OnlineShop/OnlineShop/Repositories/CartRepository/CartRepository.cs
using OnlineShop.Data;
using OnlineShop.Repositories.GenericRepository;

namespace OnlineShop.Repositories.CartRepository
{
<<<<<<< HEAD:OnlineShop/Repositories/CartRepository/CartRepository.cs
<<<<<<< HEAD
    public class CartRepository : GenericRepositoryAssociative<Cart>, ICartRepository
=======
    public class CartRepository : GenericRepository<Cart>, ICartRepository
>>>>>>> 092e24880e1fba1f81168a843069f81a1c063986
=======
    public class CartRepository : GenericRepository<Cart>, ICartRepository
>>>>>>> parent of d5dc1de (AllFromAlinNeedMoreTestes):OnlineShop/OnlineShop/Repositories/CartRepository/CartRepository.cs
    {
        public CartRepository(ProiectMDSContext context) : base(context)
        {
        }
<<<<<<< HEAD:OnlineShop/Repositories/CartRepository/CartRepository.cs
<<<<<<< HEAD

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
=======
>>>>>>> 092e24880e1fba1f81168a843069f81a1c063986
=======
>>>>>>> parent of d5dc1de (AllFromAlinNeedMoreTestes):OnlineShop/OnlineShop/Repositories/CartRepository/CartRepository.cs
    }
}
