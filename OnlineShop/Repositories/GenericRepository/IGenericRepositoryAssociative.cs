using inceputproiectMds.Models.Base;

namespace inceputproiectMds.Repositories.GenericRepository
{
    public interface IGenericRepositoryAssociative<TEntity> where TEntity : BaseEntity
    {
        //operatii de crud ce le putem face in tabel

        // get all data
        Task<List<TEntity>> GetAllAsync();

        // create
        Task CreateAsync(TEntity entity);
        

        // update
        void Update(TEntity entity);
       

        // delete
        void Delete(TEntity entity);
       


        // save
        Task<bool> SaveAsync();
    }
}
