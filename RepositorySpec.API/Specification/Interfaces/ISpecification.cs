using RepositorySpec.API.Models;
using System.Linq.Expressions;

namespace RepositorySpec.API.Specification.Interfaces
{
    public interface ISpecification<T> where T : BaseEntity
    {
        //.Where( P => P.Id == Id) .Include( p => p.Brand).Include( p => p.Category).Skip(2).Take(10).ToListAsync()
        public Expression<Func<T , bool>>? Criteria { get; }
        public List<Expression<Func<T , object>>> Includes { get; }
        public Expression<Func<T , object>>? OrderBy { get; }
        public Expression<Func<T , object>>? OrderByDesc { get; }
        int Skip { get; }
        int Take { get; }
        bool IsPagingEnabled { get; }
    }
}
