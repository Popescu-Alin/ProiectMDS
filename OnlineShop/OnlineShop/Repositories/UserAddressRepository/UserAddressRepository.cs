using inceputproiectMds.Models.Entities;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data;
using OnlineShop.Repositories.GenericRepository;

namespace OnlineShop.Repositories.UserAddressRepository
{
    public class UserAddressRepository : GenericRepositoryAssociative<UserAddress>, IUserAddressRepository
    {
        public UserAddressRepository(ProiectMDSContext context) : base(context)
        {
        }

        public async Task<UserAddress> GetUseAddresstByUserIdAndAddressIdAsync(Guid userId, Guid addressId)
        {
            return await _table.Where(x => x.UserId == userId && x.AddressId == addressId).FirstOrDefaultAsync();
        }

        public async Task<List<UserAddress>> GetUseAddressByUserIdAsync(Guid userId)
        {
            return await _table.Where(x => x.UserId==userId).ToListAsync();
        }

        public async Task<List<UserAddress>> GetUseAddressByAddressIdAsync(Guid addressId)
        {
            return await _table.Where(x=> x.AddressId==addressId).ToListAsync();
        }
    }
}
