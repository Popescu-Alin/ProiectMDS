using inceputproiectMds.Models.Entities;
using inceputproiectMds.Repositories.GenericRepository;

namespace OnlineShop.Repositories.ProductsRepository
{
    public interface IProductsRepository: IGenericRepository<Product>
    {
        public Task<List<Product>> FindRange(List<Guid> produseIds);

        public Task<List<Product>> GetAllProduseOrdonate();

       
    }
}
