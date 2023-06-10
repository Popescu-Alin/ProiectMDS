using inceputproiectMds.Models.Entities;
using inceputproiectMds.Repositories.GenericRepository;
using OnlineShop.Data;
using OnlineShop.Repositories.GenericRepository;

namespace OnlineShop.Repositories.EasyBoxRepository
{
    public class EasyBoxRepository : GenericRepository<EasyBox>, IEasyBoxRepository
    {
        public EasyBoxRepository(ProiectMDSContext context) : base(context)
        {
        }
    }
}
