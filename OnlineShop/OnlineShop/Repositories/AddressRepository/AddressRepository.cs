using inceputproiectMds.Models.Entities;
using OnlineShop.Data;
using OnlineShop.Repositories.GenericRepository;

namespace OnlineShop.Repositories.AddressRepository
{
    public class AddressRepository : GenericRepository<Address>, IAddressRepository
    {
        public AddressRepository(ProiectMDSContext context) : base(context)
        {
        }
    }
}
