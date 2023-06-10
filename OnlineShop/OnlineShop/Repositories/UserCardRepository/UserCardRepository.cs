using inceputproiectMds.Models.Entities;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data;
using OnlineShop.Repositories.GenericRepository;

namespace OnlineShop.Repositories.UserCardRepository
{
    public class UserCardRepository : GenericRepositoryAssociative<UserCard>, IUserCardRepository
    {
        public UserCardRepository(ProiectMDSContext context) : base(context)
        {
        }

        public async Task<UserCard> GetUserCardByUserIdAndCardIdAsync(Guid userId, Guid cardID)
        {
            return await _table.Where(x => x.UserId == userId && x.CardId == cardID).FirstOrDefaultAsync();
        }

        public async Task<List<UserCard>> GetUserCardsByCardIdAsync(Guid cardID)
        {
            return await _table.Where(x => x.CardId == cardID).ToListAsync();
        }

        public async Task<List<UserCard>> GetUserCardsByUserIdAsync(Guid userId)
        {
            return await _table.Where(x => x.UserId == userId).ToListAsync();
        }
    }
}
