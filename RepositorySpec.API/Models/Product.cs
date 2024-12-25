using System.ComponentModel.DataAnnotations.Schema;

namespace RepositorySpec.API.Models
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }

        /// <summary>
        /// Product Brand and Product are in a one-to-many relationship 
        /// </summary>
        [ForeignKey(nameof(Brand))]
        public int ProductBrandId { get; set; }
        [InverseProperty(nameof(ProductBrand.Products))]
        public ProductBrand? Brand { get; set; }
        /// <summary>
        /// Product Category and Product are in a one-to-many relationship.
        /// </summary>
        [ForeignKey(nameof(Category))]
        public int ProductCategoryId { get; set; }
        [InverseProperty(nameof(ProductCategory.Products))]
        public ProductCategory? Category { get; set; }
    }
}
