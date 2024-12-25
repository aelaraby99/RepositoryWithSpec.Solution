using RepositorySpec.API.Data;
using RepositorySpec.API.Models;
using RepositorySpec.API.Repository.Interfaces;
using System.Collections;

namespace RepositorySpec.API.Repository.Classes
{
    public class UnitOfWork( StoreContext context ) : IUnitOfWork
    {
        private readonly StoreContext _context = context;
        //private Dictionary<string , Repository<BaseEntity>> _repositories;
        private Hashtable _repositories;

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
        }

        public IRepository<TEntity> GenericRepository<TEntity>() where TEntity : BaseEntity
        {
            if (_repositories is null)
                //_repositories = new Dictionary<string , Repository<BaseEntity>>();
                _repositories = new Hashtable();

            var key = typeof(TEntity).Name;
            if (!_repositories.ContainsKey(key))
            {
                var repo = new Repository<TEntity>(_context) /*as Repository<BaseEntity>*/;
                _repositories.Add(key , repo);
            }
            return _repositories [key] as IRepository<TEntity>;
        }
    }
}
