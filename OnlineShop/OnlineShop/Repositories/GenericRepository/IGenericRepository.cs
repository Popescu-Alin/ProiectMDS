using inceputproiectMds.Models.Base;

namespace inceputproiectMds.Repositories.GenericRepository
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        //operatii de crud ce le putem face in tabel

        // get all data
        Task<List<TEntity>> GetAllAsync();

        // create
        Task CreateAsync(TEntity entity);
        Task CreateRangeAsync(IEnumerable<TEntity> entities);

        // update
        void Update(TEntity entity);
        void UpdateRange(IEnumerable<TEntity> entities);

        // delete
        void Delete(TEntity entity);
        void DeleteRange(IEnumerable<TEntity> entities);

        // find 
        Task<TEntity> FindByIdAsync(Guid id);

        // save
        Task<bool> SaveAsync();
    }
}
