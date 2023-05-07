using inceputproiectMds.Models.Entities;
using OnlineShop.Data;
using OnlineShop.Repositories.GenericRepository;

namespace OnlineShop.Repositories.AddressRepository
{
    public class ReviewRepository : GenericRepository<Address>, IAddressRepository
    {
        public ReviewRepository(ProiectMDSContext context) : base(context)
        {
        }
    }
}
