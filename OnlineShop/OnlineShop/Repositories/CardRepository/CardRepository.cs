using inceputproiectMds.Models.Entities;
using OnlineShop.Data;
using OnlineShop.Repositories.GenericRepository;

namespace OnlineShop.Repositories.CardRepository
{
    public class CardRepository : GenericRepository<Card>, ICardRepository
    {
        public CardRepository(ProiectMDSContext context) : base(context)
        {
        }
    }
}
