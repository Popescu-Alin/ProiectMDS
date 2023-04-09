using inceputproiectMds.Repositories.GenericRepository;
using inceputproiectMds.Models.Entities;
using OnlineShop.Repositories.GenericRepository;
using OnlineShop.Data;
using Microsoft.EntityFrameworkCore;

namespace OnlineShop.Repositories.ProductsRepository
{
    public class ProductsRepository : GenericRepository<Product>, IProductsRepository
    {
        public ProductsRepository(ProiectMDSContext context) : base(context)
        {
        }

        public async Task<List<Product>> FindRange(List<Guid> produseIds)
        {
            return await _table.Where(x => produseIds.Contains(x.ProductId)).ToListAsync();
        }

        //produsele ordonate dupa data de expirare
        public async Task<List<Product>> GetAllProduseOrdonate()
        {
            return await _table.OrderBy(x => x.Price).ToListAsync();
        }

       
       
    }
}
