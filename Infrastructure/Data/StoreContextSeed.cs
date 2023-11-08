using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Infrastructure.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context)
        {
            if (!context.ProductBrands.Any())
            {

                using var transaction = context.Database.BeginTransaction();
                var brandsData = File.ReadAllText("../Infrastructure/Data/SeedData/brands.json");
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData); ;

                foreach (var item in brands)
                {
                    context.ProductBrands.Add(item);
                }
                context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT ProductBrands ON");
                await context.SaveChangesAsync();
                context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT ProductBrands OFF");
                transaction.Commit();

            }

            if (!context.ProductTypes.Any())
            {
                using var transaction = context.Database.BeginTransaction();
                var typesdata = File.ReadAllText("../Infrastructure/Data/SeedData/types.json");
                var types = JsonSerializer.Deserialize<List<ProductType>>(typesdata);

                foreach (var type in types)
                {
                    context.ProductTypes.Add(type);
                }
                context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT ProductTypes ON");
                await context.SaveChangesAsync();
                context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT ProductTypes OFF");
                transaction.Commit();
            }

            if (!context.Products.Any())
            {
                var productdata = File.ReadAllText("../Infrastructure/Data/SeedData/products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(productdata);

                foreach (var pro in products)
                {
                    context.Products.Add(pro);
                }

                await context.SaveChangesAsync();
            }
        }
    }
    
}
