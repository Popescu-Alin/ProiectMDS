using inceputproiectMds.Repositories.GenericRepository;
using inceputproiectMds.Models.Entities;
using OnlineShop.Repositories.GenericRepository;
using OnlineShop.Data;
using Microsoft.EntityFrameworkCore;

namespace OnlineShop.Repositories.ProductOrderRepository
{
    public class ProductOrderRepository : GenericRepository<ProductOrder>, IProductOrderRepository
    {
        public ProductOrderRepository(ProiectMDSContext context) : base(context)
        {
        }

      
       
       
    }
}
