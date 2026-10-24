using Domain.Contrects;
using Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Presistence
{
    public static class SpecificationEvaluator
    {
        public static IQueryable<TEntity> CreateQuery<TEntity, TKey>(IQueryable<TEntity> inputQuery,
            ISpecification<TEntity, TKey> specification)where TEntity : BaseEntity<TKey>
        {
            var query = inputQuery;

            if (specification.Criteria is not null)
                query = query.Where(specification.Criteria);

            if (specification.OrderBy is not null)
                query = query.OrderBy(specification.OrderBy);

            
            if (specification.OrderByDesending is not null)
                query = query.OrderByDescending(specification.OrderByDesending);

            if (specification.Includes is not null && specification.Includes.Count > 0)
            {

                foreach (var item in specification.Includes)
                    query = query.Include(item);
            }

            if (specification.IsPaginated)
            {
                query=query.Skip(specification.Skip).Take(specification.Take);
            }
            return query;
        }
    }
}