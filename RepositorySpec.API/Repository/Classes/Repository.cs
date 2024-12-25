using Microsoft.EntityFrameworkCore;
using RepositorySpec.API.Data;
using RepositorySpec.API.Models;
using RepositorySpec.API.Repository.Interfaces;
using RepositorySpec.API.Specification.Classes;
using RepositorySpec.API.Specification.Interfaces;

namespace RepositorySpec.API.Repository.Classes
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly StoreContext _context;
        public Repository( StoreContext context )
        {
            _context = context;
        }
        public async Task AddAsync( T entity )
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public Task DeleteAsync( T entity )
        {
            _context.Remove(entity);
            return Task.CompletedTask;
        }

        public async Task DeleteByIdAsync( int? Id )
        {
            var entity = await GetByIdAsync(Id);
            if (entity != null)
            {
                await DeleteAsync(entity);
            }
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            ///if (typeof(T) == typeof(Product))
            ///{ // Open for extension, closed for modification
            ///    return await _context.Set<Product>().Include(p => p.Brand).Include(p => p.Category).ToListAsync() as IReadOnlyList<T>;
            ///}
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T?> GetByIdAsync( int? Id )
        {
            return await _context.Set<T>().FindAsync(Id);
        }

        public Task UpdateAsync( T entity )
        {
            _context.Set<T>().Update(entity);
            return Task.CompletedTask;
        }

        #region Specs
        public async Task<IEnumerable<T>> GetAllWithSpecAsync( ISpecification<T> spec )
        {
            return await SpecifiedQueryBuilder<T>.BuildQuery(_context.Set<T>().AsQueryable() , spec).ToListAsync();
        }

        public async Task<T?> GetByIdWithSpecAsync( ISpecification<T> spec )
        {
            return await SpecifiedQueryBuilder<T>.BuildQuery(_context.Set<T>().AsQueryable() , spec).FirstOrDefaultAsync();
        }
        #endregion
    }

}
