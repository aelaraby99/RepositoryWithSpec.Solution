using RepositorySpec.API.Models;
using RepositorySpec.API.Specification.Interfaces;
using System.Linq.Expressions;

namespace RepositorySpec.API.Repository.Interfaces
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity?> GetByIdAsync( int? Id );
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task AddAsync( TEntity entity );
        Task UpdateAsync( TEntity entity );
        Task DeleteAsync( TEntity entity );
        Task DeleteByIdAsync( int? Id );

        #region Specs

        Task<IEnumerable<TEntity>> GetAllWithSpecAsync( ISpecification<TEntity> spec );
        Task<TEntity?> GetByIdWithSpecAsync( ISpecification<TEntity> spec );

        #endregion
    }
}
