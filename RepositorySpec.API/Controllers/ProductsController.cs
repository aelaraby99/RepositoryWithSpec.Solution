using Microsoft.AspNetCore.Mvc;
using RepositorySpec.API.Models;
using RepositorySpec.API.Repository.Interfaces;

namespace RepositorySpec.API.Controllers
{
    public class ProductsController
        : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        ///private readonly IGenericRepository<Product> _productsRepo;
        ///private readonly IGenericRepository<ProductBrand> _productBrandsRepo;
        ///private readonly IGenericRepository<ProductCategory> _productCategoriesRepo;
        ///public ProductsController( IGenericRepository<Product> productsRepo , IGenericRepository<ProductBrand> productBrandsRepo , IGenericRepository<ProductCategory> productCategoriesRepo )
        ///{
        ///    _productsRepo = productsRepo;
        ///    _productBrandsRepo = productBrandsRepo;
        ///    _productCategoriesRepo = productCategoriesRepo;
        ///}
        #region Spec
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Product>>> GetProducts( string? Search , int Page , int PageSize , string? SortingBy )
        //{

        //    ///BaseSpecification<Product> specification = new BaseSpecification<Product>();
        //    ///specification = new BaseSpecification<Product>(p => p.Id > 5);
        //    ///specification.AddInclude(p => p.Brand);
        //    ///specification.ApplyPagination(0 , 5);
        //    ///specification.AddOrderBy(p => p.Price , true);

        //    //var specification = new ProductSpecifications(Search , SortingBy , PageSize , Page);
        //    //var products = await _unitOfWork.GenericRepository<Product>().GetAllWithSpecAsync(specification);
        //    var products = await _unitOfWork.GenericRepository<Product>().GetAllAsync();
        //    return Ok(products);
        //}
        #endregion
        public ProductsController( IUnitOfWork unitOfWork )
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _unitOfWork.GenericRepository<Product>().GetAllAsync();
            return Ok(products);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Product?>> GetProduct( int id )
        {
            ///var spec = new ProductSpecifications(id);
            ///var product = await _unitOfWork.GenericRepository<Product>().GetByIdWithSpecAsync(spec);
            ///return Ok(NotFound());
            var product = await _unitOfWork.GenericRepository<Product>().GetByIdAsync(id);
            if (product is null)
                return NotFound($"Product with ID {id} not found.");
            return (product);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct( int id )
        {
            var product = await _unitOfWork.GenericRepository<Product>().GetByIdAsync(id);
            if (product == null)
            {
                return NotFound($"Product with ID {id} not found.");
            }
            await _unitOfWork.GenericRepository<Product>().DeleteByIdAsync(id);
            await _unitOfWork.CompleteAsync();
            return Ok();
        }
        [HttpDelete]
        public async Task<ActionResult> DeleteProduct([FromBody] Product product )
        {
            if (product == null)
            {
                return BadRequest("Product cannot be null.");
            }
            var productToDelete = await _unitOfWork.GenericRepository<Product>().GetByIdAsync(product.Id);
            if (productToDelete is null)
                return NotFound($"Product with ID {product.Id} not found.");

            await _unitOfWork.GenericRepository<Product>().DeleteAsync(productToDelete);
            await _unitOfWork.CompleteAsync();
            return Ok();
        }
        [HttpPost]
        public async Task<ActionResult> AddProduct( Product product )
        {
            if (product is null)
                return BadRequest("Product cannot be null");
            await _unitOfWork.GenericRepository<Product>().AddAsync(product);
            await _unitOfWork.CompleteAsync();
            return Ok();
        }
        [HttpPut]
        public async Task<ActionResult> UpdateProduct( Product product )
        {
            if (product is null)
                return BadRequest("Product cannot be null");
            var productToUpdate = await _unitOfWork.GenericRepository<Product>().GetByIdAsync(product.Id);
            if (productToUpdate is null)
                return NotFound("Product not found");
            try
            {
                await _unitOfWork.GenericRepository<Product>().UpdateAsync(product);
                await _unitOfWork.CompleteAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500 , $"An error occurred while updating the product: {ex.Message}");
            }

        }
    }
}
