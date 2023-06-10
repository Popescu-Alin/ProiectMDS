using inceputproiectMds.Models.Entities;
using inceputproiectMds.Repositories.GenericRepository;

namespace OnlineShop.Repositories.UserCardRepository
{
    public interface IUserCardRepository : IGenericRepositoryAssociative<UserCard>
    {
        public Task<List<UserCard>> GetUserCardsByUserIdAsync(Guid userId);

        public Task<List<UserCard>> GetUserCardsByCardIdAsync(Guid cardID);

        public Task<UserCard> GetUserCardByUserIdAndCardIdAsync(Guid userId, Guid cardID);
    }
}
