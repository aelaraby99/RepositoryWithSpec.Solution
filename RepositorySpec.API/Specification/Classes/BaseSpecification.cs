using RepositorySpec.API.Models;
using RepositorySpec.API.Specification.Interfaces;
using System.Linq.Expressions;

namespace RepositorySpec.API.Specification.Classes
{
    public class BaseSpecification<T> : ISpecification<T> where T : BaseEntity
    {
        //This Class can help centralize common specification logic and make it easier to create new specifications. This approach adheres to the DRY (Don't Repeat Yourself) principle and enhances code maintainability.
        public BaseSpecification( Expression<Func<T , bool>> _Criteria )
        {
            Criteria = _Criteria;
        }
        public Expression<Func<T , bool>>? Criteria { get; }
        public List<Expression<Func<T , object>>> Includes { get; } = new List<Expression<Func<T , object>>>();
        public Expression<Func<T , object>>? OrderBy { get; private set; }
        public Expression<Func<T , object>>? OrderByDesc { get; private set; }
        public int Skip { get; private set; }
        public int Take { get; private set; }
        public bool IsPagingEnabled { get; private set; } = false;

        protected void AddInclude( Expression<Func<T , object>> includeExpression )
        {
            Includes.Add(includeExpression);
        }
        protected void AddOrderBy( Expression<Func<T , object>> orderByExpression , bool isDesc = false )
        {
            if (isDesc)
            {
                OrderByDesc = orderByExpression;
            }
            else
            {
                OrderBy = orderByExpression;
            }
        }
        protected void ApplyPagination( int skip , int take )
        {
            IsPagingEnabled = true;
            Skip = skip;
            Take = take;
        }

    }
}
