using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data;
using inceputproiectMds.Models.Base;
using inceputproiectMds.Repositories.GenericRepository;

namespace OnlineShop.Repositories.GenericRepository
{
    //operatiile de crud 
    public class GenericRepositoryAssociative<TEntity> : IGenericRepositoryAssociative<TEntity> where TEntity : BaseEntity
    {
        protected readonly ProiectMDSContext _context;
        protected readonly DbSet<TEntity> _table;

        public GenericRepositoryAssociative(ProiectMDSContext context)
        {
            _context = context;
            _table = _context.Set<TEntity>();
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            return await _table.AsNoTracking().ToListAsync();

        }

        // create
        

        public async Task CreateAsync(TEntity entity)
        {   
            entity.DateCreated = DateTime.Now;
            entity.DateModified = DateTime.Now;
            await _table.AddAsync(entity);
        }

       

        // update
        public void Update(TEntity entity)
        {
            entity.DateModified = DateTime.Now;
            _table.Update(entity);
        }

      

        // delete

        public void Delete(TEntity entity)
        {
            _table.Remove(entity);
        }

        // save
        public async Task<bool> SaveAsync()
        {
            try
            {
                return await _context.SaveChangesAsync() > 0;
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex);
            }

            return false;
        }


       
    }
}
