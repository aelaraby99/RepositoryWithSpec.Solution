using Microsoft.EntityFrameworkCore;
using RepositorySpec.API.Models;
using System.Reflection;

namespace RepositorySpec.API.Data
{
    public class StoreContext : DbContext
    {
        public StoreContext( DbContextOptions<StoreContext> options ) : base(options)
        {
        }
        protected override void OnModelCreating( ModelBuilder modelBuilder )
        {
            base.OnModelCreating(modelBuilder); 
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductBrand> ProductBrands { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
    }
}
