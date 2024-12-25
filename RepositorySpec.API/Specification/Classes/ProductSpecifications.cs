using RepositorySpec.API.Models;
using System.Linq.Expressions;

namespace RepositorySpec.API.Specification.Classes
{
    public class ProductSpecifications : BaseSpecification<Product>
    {
        //specification.AddInclude(p => p.Brand);
        //specification.ApplyPagination(0 , 5);
        //specification.AddOrderBy(p => p.Price , true);
        public ProductSpecifications( int id ) : base(p => p.Id == id)
        {
            //This work only with one crieteria / GetById so the return will be only one item so no need for Paging or sorting or searching with any [The Item with the includes]
            AddIncludes();
        }
        public ProductSpecifications( string? SearchWord , string? Sort , int PageSize = 5 , int Page = 1 )
            : base(p =>
                (string.IsNullOrEmpty(SearchWord) || p.Name.ToLower().Contains(SearchWord.ToLower()))
            )
        {
            AddIncludes();
            ApplySorting(Sort);
            ApplyPagination(Page >= 1 ? Page : 1 , PageSize >= 1 ? PageSize : 5);
        }

        private void AddIncludes()
        {
            AddInclude(p => p.Brand);
            AddInclude(p => p.Category);
        }
        private void ApplySorting( string? Sort )
        {
            if (!string.IsNullOrEmpty(Sort))
            {
                switch (Sort)
                {
                    case "PriceDesc":
                        AddOrderBy(p => p.Price , true);
                        break;
                    case "Price":
                        AddOrderBy(p => p.Price);
                        break;
                    case "Name":
                        AddOrderBy(p => p.Name);
                        break;
                    case "NameDesc":
                        AddOrderBy(p => p.Name , true);
                        break;
                    default:
                        AddOrderBy(p => p.Id);
                        break;
                }
            }
        }
    }
}
