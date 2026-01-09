using Catalog.Entities;
using Catalog.Specifications;

namespace Catalog.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Pagination<Product>> GetProductsAsync(CatalogSpecParams specParams);
        Task<IEnumerable<Product>> GetProductsByName(string name);
        Task<IEnumerable<Product>> GetProductsByBrand(string brand);
        Task<Product> GetProductById(string productId);
        Task<Product> CreateProduct(Product product);
        Task<bool> UpdateProduct(Product product);
        Task<bool> DeleteProduct(string productId);
        Task<ProductBrand> GetBrandByBrandIdAsync(string brandId);
        Task<ProductType> GetTypeByTypeIdAsync(string typeId);
    }
}
