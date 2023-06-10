using inceputproiectMds.Models.Entities;
using OnlineShop.Repositories.GenericRepository;
using OnlineShop.Data;
using Microsoft.EntityFrameworkCore;

namespace OnlineShop.Repositories.ProductsRepository
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(ProiectMDSContext context) : base(context)
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

        public async Task<List<Product>> GetProduseByCategory(Guid categoryId)
        {
            return await _table.Where(x => x.CategoryId == categoryId).ToListAsync();
        }

        public async Task<List<Product>> GetProduseByUser(Guid userId)
        {
            return await _table.Where(x => x.UserId == userId).ToListAsync();
        }

        public async Task<List<Product>> GetAllAsync(int offset, int limit, bool sorted)
        {
            if (sorted)
            {
                return await _table.OrderBy(x => x.Price).Skip(offset).Take(limit).ToListAsync();
            }
            else
            {
                return await _table.Skip(offset).Take(limit).ToListAsync();
            }
        }

        public async Task<int> GetNumberOfProducts()
        {
            return await _table.CountAsync();
        }
    }
}
