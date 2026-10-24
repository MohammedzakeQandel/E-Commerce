using Domain.Contrects;
using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    public class BaseSpecification<TEntity,Tkey>:ISpecification<TEntity,Tkey> where TEntity : BaseEntity<Tkey>
    {
        #region Where
        public Expression<Func<TEntity, bool>>? Criteria { get; private set; }
        public BaseSpecification(Expression<Func<TEntity, bool>>? criteria)
        {
            Criteria = criteria;
        }
        #endregion

        #region Include
        public List<Expression<Func<TEntity, object>>>? Includes { get; private set; } = new();
        protected void AddInclude(Expression<Func<TEntity, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }
        #endregion

        #region Order
        public Expression<Func<TEntity, object>> OrderBy { get; private set; }
        protected void AddOrderBy(Expression<Func<TEntity, object>> OrderByExpression) => OrderBy = OrderByExpression;
        public Expression<Func<TEntity, object>> OrderByDesending { get; private set; }

       

        protected void AddOrderByDesending(Expression<Func<TEntity, object>> AddOrderByDesending) => OrderByDesending = AddOrderByDesending;

        #endregion

        public int Skip { get; private set; }

        public int Take { get; private set; }

        public bool IsPaginated { get; private set; }

        protected void ApplyPagination(int pageSize , int pageIndex)
        {
            IsPaginated = true;
            Take = pageSize;
            Skip=(pageIndex-1)*pageSize;
        }

    }
}
