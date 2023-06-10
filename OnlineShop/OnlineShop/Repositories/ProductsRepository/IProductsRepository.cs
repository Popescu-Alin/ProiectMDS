using inceputproiectMds.Models.Entities;
using inceputproiectMds.Repositories.GenericRepository;

namespace OnlineShop.Repositories.ProductsRepository
{
    public interface IProductRepository: IGenericRepository<Product>
    {
        public Task<List<Product>> FindRange(List<Guid> produseIds);
        public Task<List<Product>> GetAllProduseOrdonate();
        public Task<List<Product>> GetProduseByCategory(Guid categoryId);
        public Task<List<Product>> GetProduseByUser(Guid userId);
        public Task<List<Product>> GetAllAsync(int offset, int limit,bool sorted);
        public Task<int> GetNumberOfProducts();

       
    }
}
