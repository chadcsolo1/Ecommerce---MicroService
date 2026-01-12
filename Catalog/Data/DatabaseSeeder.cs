using Catalog.Entities;
using MongoDB.Driver;

namespace Catalog.Data
{

    public class DatabaseSeeder
    {
        
        public static async Task SeedAsync(IConfiguration config)
        {
            var client = new MongoClient(config["DatabaseSettings:ConnectionString"]);
            var database = client.GetDatabase(config["DatabaseSettings:DatabaseName"]);
            var productCollection = database.GetCollection<Product>(config["DatabaseSettings:ProductCollectionName"]);
            var brandCollection = database.GetCollection<ProductBrand>(config["DatabaseSettings:BrandCollectionName"]);
            var typeCollection = database.GetCollection<ProductType>(config["DatabaseSettings:TypeCollectionName"]);

            var seedBasePath = Path.Combine("Data", "SeedData");

            // Seed Brands
            List<ProductBrand>  brandList = new();

            if (await brandCollection.CountDocumentsAsync(p => true) == 0)
            {
                //var brandData = await File.ReadAllTextAsync(seedBasePath, "brands.json");
            }
            
        }
    }
}
