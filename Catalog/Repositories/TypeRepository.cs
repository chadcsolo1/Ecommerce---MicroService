using Catalog.Entities;
using MongoDB.Driver;

namespace Catalog.Repositories
{
    public class TypeRepository : ITypeRepository
    {
        private readonly IMongoCollection<ProductType> _typeCollection;
        public TypeRepository(IConfiguration config)
        {
            var client = new MongoClient(config["DatabaseSettings:ConnectionString"]);
            var database = client.GetDatabase(config["DatabaseSettings:DatabaseName"]);
            _typeCollection = database.GetCollection<ProductType>(config["DatabaseSettings:TypeCollectionName"]);
        }
        public async Task<IEnumerable<ProductType>> GetAllTypes()
        {
            return await _typeCollection.Find(type => true).ToListAsync();
        }

        public async Task<ProductType> GetByIdAsync(string id)
        {
            return await _typeCollection.Find(type => type.Id == id).FirstOrDefaultAsync();
        }
    }
}
