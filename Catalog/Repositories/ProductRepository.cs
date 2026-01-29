using Catalog.Entities;
using Catalog.Specifications;
using MongoDB.Bson;
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

        public async Task<ProductBrand> GetBrandByBrandIdAsync(string brandId)
        {
            return await _brandCollection.Find(b => b.Id == brandId).FirstOrDefaultAsync();
        }

        public async Task<Product> GetProductById(string productId)
        {
            return await _productCollection.Find(p => p.Id == productId).FirstOrDefaultAsync();
        }

        public async Task<Pagination<Product>> GetProductsAsync(CatalogSpecParams specParams)
        {
            var queryBuilder = Builders<Product>.Filter;
            var filter = queryBuilder.Empty;

            if (!string.IsNullOrEmpty(specParams.Sort))
            {
                filter &= queryBuilder.Where(product => product.Name.ToLower().Contains(specParams.Sort.ToLower()));
            }

            if (!string.IsNullOrEmpty(specParams.BrandId))
            {
                filter &= queryBuilder.Eq(product => product.Brand.Id, specParams.BrandId);
            }

            if (!string.IsNullOrEmpty(specParams.TypeId))
            {
                filter &= queryBuilder.Eq(product => product.Type.Id, specParams.TypeId);
            }

            var totalItems = await _productCollection.CountDocumentsAsync(filter);
            var products = await ApplyDataFilters(specParams, filter);

            return new Pagination<Product>(
                
                specParams.PageIndex,
                specParams.PageSize,
                (int)totalItems,
                products
            );

        }

        public async Task<IEnumerable<Product>> GetProductsByBrand(string brand)
        {
            return await _productCollection.
                Find(p => p.Brand.Name.ToLower() == brand.ToLower())
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByName(string name)
        {
            var filter = Builders<Product>.Filter.Regex(product => product.Name, new BsonRegularExpression($".*{name}.*", "i"));
            return await _productCollection.Find(filter).ToListAsync();
        }

        public async Task<ProductType> GetTypeByTypeIdAsync(string typeId)
        {
            return await _typeCollection.Find(type => type.Id == typeId).FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            var result = await _productCollection
                .ReplaceOneAsync(p => p.Id == product.Id, product);

            return result.IsAcknowledged && result.ModifiedCount > 0;
        }


        private async Task<IReadOnlyCollection<Product>> ApplyDataFilters(CatalogSpecParams specParams, FilterDefinition<Product> filter)
        {
            var sortDefinition = Builders<Product>.Sort.Ascending("Name");

            if (!string.IsNullOrEmpty(specParams.Sort))
            {
                sortDefinition = specParams.Sort switch
                {
                    "priceAsc" => Builders<Product>.Sort.Ascending(p => p.Price),
                    "priceDesc" => Builders<Product>.Sort.Descending(p => p.Price),
                    "nameDesc" => Builders<Product>.Sort.Descending(p => p.Name),
                    _ => Builders<Product>.Sort.Ascending(p => p.Name),
                };
            }

            return await _productCollection
                .Find(filter)
                .Sort(sortDefinition)
                .Skip(specParams.PageSize * (specParams.PageIndex - 1))
                .Limit(specParams.PageSize)
                .ToListAsync();
        }
    }
}
