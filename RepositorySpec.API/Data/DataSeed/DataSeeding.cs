using Microsoft.EntityFrameworkCore;
using RepositorySpec.API.Models;
using System.Text.Json;

namespace RepositorySpec.API.Data.DataSeed
{
    public static class DataSeeding
    {
        public static async void SeedData( StoreContext context )
        {
            if (context.ProductBrands.Count() == 0)
            {
                var brandsData = System.IO.File.ReadAllText("Data/DataSeed/JsonData/brands.json");
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
                if (brands?.Count > 0)
                {
                    foreach (var item in brands)
                    {
                        context.ProductBrands.Add(item);
                    }
                    await context.SaveChangesAsync();
                }

            }

            if (context.ProductCategories.Count() == 0)
            {
                var categoriesData = System.IO.File.ReadAllText("Data/DataSeed/JsonData/categories.json");
                var categories = JsonSerializer.Deserialize<List<ProductCategory>>(categoriesData);
                if (categories?.Count > 0)
                {
                    foreach (var item in categories)
                    {
                        context.ProductCategories.Add(item);
                    }
                    await context.SaveChangesAsync();
                }
            }

            if (context.Products.Count() == 0)
            {
                var productsData = System.IO.File.ReadAllText("Data/DataSeed/JsonData/products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(productsData);
                if (products?.Count > 0)
                {
                    foreach (var item in products)
                    {
                        context.Products.Add(item);
                    }
                    await context.SaveChangesAsync();
                }

            }
        }
    }
}
