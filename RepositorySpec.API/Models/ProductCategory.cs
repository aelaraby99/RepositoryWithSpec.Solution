using System.Text.Json.Serialization;

namespace RepositorySpec.API.Models
{
    public class ProductCategory : BaseEntity
    {
        public string Name { get; set; }
        //[JsonIgnore]
        public virtual ICollection<Product> Products { get; set; } = new HashSet<Product>();
    }
}
