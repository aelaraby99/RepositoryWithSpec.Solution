using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RepositorySpec.API.Models;

namespace RepositorySpec.API.Data.Configurations
{
    public class ProductConfigurations : IEntityTypeConfiguration<Product>
    {
        public void Configure( EntityTypeBuilder<Product> builder )
        {
            builder.HasKey(p => p.Id);
           
            builder.HasOne(p => p.Brand)
                .WithMany(b => b.Products)
                .HasForeignKey(p => p.ProductBrandId);
           
            builder.HasOne(p => p.Category)
                .WithMany(C => C.Products)
                .HasForeignKey(p => p.ProductCategoryId);
        }
    }
}
