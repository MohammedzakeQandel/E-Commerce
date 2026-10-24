using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presistence.Repositories
{
    using Domain.Contracts;
    using Domain.Contrects;
    using Domain.Entity;
   
    using Microsoft.EntityFrameworkCore;
  

    namespace Presistence.Repositories
    {
        public class GenericRepository<TEntity, TKey>(StoreDbContext _dbContext) : IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
        {

            // Get a single entity by its Id
            public async Task<TEntity?> GetByIdAsync(TKey id)
            {
                return await _dbContext.Set<TEntity>().FindAsync(id);
            }

            // Get all entities
            public async Task<IEnumerable<TEntity>> GetAllAsync(bool asNoTracking = false)

                => asNoTracking ? await _dbContext.Set<TEntity>().AsNoTracking().ToListAsync()
                : await _dbContext.Set<TEntity>().ToListAsync();

            // Add a new entity
            public async Task AddAsync(TEntity e)
            {
                await _dbContext.Set<TEntity>().AddAsync(e);
            }

            // Update an existing entity
            public void Update(TEntity e)
            {
                _dbContext.Set<TEntity>().Update(e);
            }

            // Delete an existing entity
            public void Delete(TEntity e)
            {
                _dbContext.Set<TEntity>().Remove(e);
            }

            public async Task<IEnumerable<TEntity>> GetAllAsync(ISpecification<TEntity, TKey> specification)

            => await SpecificationEvaluator.CreateQuery(_dbContext.Set<TEntity>(), specification).ToListAsync();

            public async Task<TEntity?> GetByIdAsync(ISpecification<TEntity, TKey> specification)

             => await SpecificationEvaluator.CreateQuery(_dbContext.Set<TEntity>(), specification).FirstOrDefaultAsync();

            public async Task<int> CountAsync(ISpecification<TEntity, TKey> specification)
            
              => await SpecificationEvaluator.CreateQuery(_dbContext.Set<TEntity>(), specification).CountAsync();
         
        }
    }
}
