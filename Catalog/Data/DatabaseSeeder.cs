using Catalog.Entities;
using MongoDB.Driver;
using System.Text.Json;

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

            //brandCollection database is empty then seed data else get all data from database
            if ((await brandCollection.CountDocumentsAsync(p => true)) == 0)
            {
                //Read all data from brands.json, deserialize and insert to brandCollection database
                var brandData = await File.ReadAllTextAsync(Path.Combine(seedBasePath, "brands.json"));
                brandList = JsonSerializer.Deserialize<List<ProductBrand>>(brandData);
                await brandCollection.InsertManyAsync(brandList);
            } else
            {
                brandList = await brandCollection.Find(p => true).ToListAsync();
            }

            // Seed Types
            List<ProductType> typeList = new();

            //typeCollection database is empty then seed data else get all data from database
            if ((await typeCollection.CountDocumentsAsync(p => true)) == 0)
            {
                //Read all data from types.json, deserialize and insert to typeCollection database
                var typeData = await File.ReadAllTextAsync(Path.Combine(seedBasePath, "types.json"));
                typeList = JsonSerializer.Deserialize<List<ProductType>>(typeData);
                await typeCollection.InsertManyAsync(typeList);
            } else
            {
                typeList = await typeCollection.Find(p => true).ToListAsync();
            }

            // Seed products
            List<Product> productList = new();  

            //productCollection database is empty then seed data else get all data from database
            if ((await productCollection.CountDocumentsAsync(p => true)) == 0)
            {
                //Read all data from products.json, deserialize and insert to productCollection database
                var productData = await File.ReadAllTextAsync(Path.Combine(seedBasePath, "products.json"));
                productList = JsonSerializer.Deserialize<List<Product>>(productData);
                foreach (var product in productList)
                {
                    //Reset Id to have Mongo generate new Id
                    product.Id = null;
                    if (product.CreatedDate == default)
                    {
                        product.CreatedDate = DateTimeOffset.UtcNow;
                    }

                }
                await productCollection.InsertManyAsync(productList);
            } else
            {
                productList = await productCollection.Find(p => true).ToListAsync();
            }
            
        }
    }
}
