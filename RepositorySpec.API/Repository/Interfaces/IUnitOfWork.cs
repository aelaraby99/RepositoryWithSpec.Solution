using RepositorySpec.API.Models;

namespace RepositorySpec.API.Repository.Interfaces
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IRepository<TEntity> GenericRepository<TEntity>() where TEntity : BaseEntity;
        Task CompleteAsync();
    }
}
