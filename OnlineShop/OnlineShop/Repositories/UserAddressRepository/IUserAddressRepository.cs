using inceputproiectMds.Models.Entities;
using inceputproiectMds.Repositories.GenericRepository;

namespace OnlineShop.Repositories.UserAddressRepository
{
    public interface IUserAddressRepository : IGenericRepositoryAssociative<UserAddress>
    {
        public Task<List<UserAddress>> GetUseAddressByUserIdAsync(Guid userId);

        public Task<List<UserAddress>> GetUseAddressByAddressIdAsync(Guid addressId);

        public Task<UserAddress> GetUseAddresstByUserIdAndAddressIdAsync(Guid userId, Guid addressId);
    }
}
