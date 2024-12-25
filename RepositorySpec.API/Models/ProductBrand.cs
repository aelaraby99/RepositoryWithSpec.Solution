using System.Text.Json.Serialization;

namespace RepositorySpec.API.Models
{
    public class ProductBrand : BaseEntity
    {
        public string Name { get; set; }
        //[JsonIgnore]
        public virtual ICollection<Product> Products { get; set; } = new HashSet<Product>();
    }
}
