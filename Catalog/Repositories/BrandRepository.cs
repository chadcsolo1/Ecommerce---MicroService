using Catalog.Entities;
using MongoDB.Driver;

namespace Catalog.Repositories
{
    public class BrandRepository : IBrandRepository
    {
        private readonly IMongoCollection<ProductBrand> _brandCollection;
        public BrandRepository(IConfiguration config)
        {
            var client = new MongoClient(config["DatabaseSettings:ConnectionString"]);
            var database = client.GetDatabase(config["DatabaseSettings:DatabaseName"]);
            _brandCollection = database.GetCollection<ProductBrand>(config["DatabaseSettings:BrandCollectionName"]);
        }
        public async Task<IEnumerable<ProductBrand>> GetAllBrands()
        {
            return await _brandCollection.Find(brand => true).ToListAsync();
        }

        public async Task<ProductBrand> GetByIdAsync(string id)
        {
            return await _brandCollection.Find(brand => brand.Id == id).FirstOrDefaultAsync();
        }
    }
}
