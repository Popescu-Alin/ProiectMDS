using inceputproiectMds.Models.Entities;
using OnlineShop.Data;
using OnlineShop.Repositories.GenericRepository;

namespace OnlineShop.Repositories.CartRepository
{
    public class CartRepository : GenericRepository<Cart>, ICartRepository
    {
        public CartRepository(ProiectMDSContext context) : base(context)
        {
        }
    }
}
