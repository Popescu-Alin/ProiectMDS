<<<<<<< HEAD
﻿using inceputproiectMds.Models.Entities;
=======
﻿using inceputproiectMds.Repositories.GenericRepository;
using inceputproiectMds.Models.Entities;
>>>>>>> 092e24880e1fba1f81168a843069f81a1c063986
using OnlineShop.Repositories.GenericRepository;
using OnlineShop.Data;
using Microsoft.EntityFrameworkCore;

namespace OnlineShop.Repositories.ProductsRepository
{
<<<<<<< HEAD
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(ProiectMDSContext context) : base(context)
=======
    public class ProductOrderRepository : GenericRepository<Product>, IProductOrderRepository
    {
        public ProductOrderRepository(ProiectMDSContext context) : base(context)
>>>>>>> 092e24880e1fba1f81168a843069f81a1c063986
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

<<<<<<< HEAD
        public async Task<List<Product>> GetProduseByCategory(Guid categoryId)
        {
            return await _table.Where(x => x.CategoryId == categoryId).ToListAsync();
        }

        public async Task<List<Product>> GetProduseByUser(Guid userId)
        {
            return await _table.Where(x => x.UserId == userId).ToListAsync();
        }

        public async Task<List<Product>> GetAllAsync(int offset, int limit)
        {
            return await _table.Skip(offset).Take(limit).ToListAsync();
        }
        
        
        
       

=======
>>>>>>> 092e24880e1fba1f81168a843069f81a1c063986
       
       
    }
}
