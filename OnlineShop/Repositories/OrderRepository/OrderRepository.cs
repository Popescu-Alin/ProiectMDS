using inceputproiectMds.Models.Entities;
using OnlineShop.Data;
using OnlineShop.Repositories.GenericRepository;

namespace OnlineShop.Repositories.OrderRepository
{
    public class OrderRepository: GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(ProiectMDSContext context) : base(context)
        {
        }
    }
}
