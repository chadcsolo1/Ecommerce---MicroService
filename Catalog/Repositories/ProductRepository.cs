using Catalog.Entities;
using Catalog.Specifications;
using MongoDB.Driver;

namespace Catalog.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IMongoCollection<Product> _productCollection;
        private readonly IMongoCollection<ProductBrand> _brandCollection;
        private readonly IMongoCollection<ProductType> _typeCollection;
        public ProductRepository(IConfiguration config)
        {
            var client = new MongoClient(config["DatabaseSettings:ConnectionString"]);
            var database = client.GetDatabase(config["DatabaseSettings:DatabaseName"]);
            _productCollection = database.GetCollection<Product>(config["DatabaseSettings:ProductCollectionName"]);
            _brandCollection = database.GetCollection<ProductBrand>(config["DatabaseSettings:BrandCollectionName"]);
            _typeCollection = database.GetCollection<ProductType>(config["DatabaseSettings:TypeCollectionName"]);
        }
        public async Task<Product> CreateProduct(Product product)
        {
            await _productCollection.InsertOneAsync(product);
            return product;
        }

        public async Task<bool> DeleteProduct(string productId)
        {
            var result = await _productCollection.DeleteOneAsync(p => p.Id == productId);
            return result.IsAcknowledged && result.DeletedCount > 0;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _productCollection.Find(product => true).ToListAsync();
        }

        public Task<ProductBrand> GetBrandByBrandIdAsync(string brandId)
        {
            throw new NotImplementedException();
        }

        public Task<Product> GetProduct(string productId)
        {
            throw new NotImplementedException();
        }

        public Task<Pagination<Product>> GetProductsAsync(CatalogSpecParams specParams)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> GetProductsByBrand(string brand)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> GetProductsByName(string name)
        {
            throw new NotImplementedException();
        }

        public Task<ProductType> GetTypeByTypeIdAsync(string typeId)
        {
            throw new NotImplementedException();
        }

        public Task<Product> UpdateProduct(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
