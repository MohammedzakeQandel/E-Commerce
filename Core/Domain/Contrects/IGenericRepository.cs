using Domain.Contrects;
using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface IGenericRepository<TEntity,TKey> where TEntity : BaseEntity<TKey>
    {
        // Get a single entity by its Id
        Task<TEntity> GetByIdAsync(TKey id);

        // Get all entities
        Task<IEnumerable<TEntity>> GetAllAsync( bool asNoTracking=false );

        // Get entities that match a specific condition
        //Task<IReadOnlyList<T>> GetAllAsync(
        //    Expression<Func<T, bool>>? criteria = null);

        // Add a new entity
        Task AddAsync(TEntity e);
        
        // Update an existing entity
        void Update(TEntity e);

        // Delete an existing entity
        void Delete(TEntity e);

        Task<IEnumerable<TEntity>> GetAllAsync(ISpecification<TEntity, TKey> specification);
        Task<TEntity> GetByIdAsync(ISpecification<TEntity, TKey> specification);
        Task<int> CountAsync(ISpecification<TEntity, TKey> specification);
         
    }
}
