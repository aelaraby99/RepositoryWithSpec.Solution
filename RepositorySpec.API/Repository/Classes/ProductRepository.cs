using RepositorySpec.API.Data;
using RepositorySpec.API.Models;
using RepositorySpec.API.Repository.Interfaces;

namespace RepositorySpec.API.Repository.Classes
{
    public class ProductRepository( StoreContext context )
        : Repository<Product>(context), IProductRepository
    {
        //public async Task<Product?> GetByNameAsync( string name )
        //{
        //   return  await _context.Set<Product>().Where(p => p.Name == name).FirstOrDefaultAsync();
        //}
    }
}
