using inceputproiectMds.Models.Entities;
using OnlineShop.Data;
using OnlineShop.Repositories.GenericRepository;

namespace OnlineShop.Repositories.CardRepository
{
    public class CartRepository : GenericRepository<Card>, ICartRepository
    {
        public CartRepository(ProiectMDSContext context) : base(context)
        {
        }
    }
}
