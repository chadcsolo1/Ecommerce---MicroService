using Catalog.Commands;
using Catalog.DTOs;
using Catalog.Entities;
using Catalog.Repositories;
using Catalog.Responses;

namespace Catalog.Extensions
{
    public static class ProductMapper
    {
        // Map Product entity to ProductResponse DTO
        public static ProductResponse ToResponse(this Product product)
        {
            return new ProductResponse
            {
                Id = product.Id,
                Name = product.Name,
                Summary = product.Summary,
                Description = product.Description,
                ImageFile = product.ImageFile,
                Brand = product.Brand,
                Type = product.Type,
                Price = product.Price,
                CreatedDate = product.CreatedDate
            };
        }

        public static IEnumerable<ProductResponse> ToResponseList(this IEnumerable<Product> products)
        {
            return products.Select(product => product.ToResponse());
        }

        // Map a list of Product entities to a list of ProductResponse DTOs
        public static Pagination<ProductResponse> ToResponse(this Pagination<Product> pagination)
        {
            return new Pagination<ProductResponse>(
                pagination.PageIndex,
                pagination.PageSize,
                pagination.Count,
                pagination.Data.Select(product => product.ToResponse()).ToList()
            );
            //return products.Select(product => product.ToResponse());
        }

        public static Product ToEntity(this CreateProductCommand command, ProductBrand brand, ProductType type)
        {
            return new Product
            {
                Name = command.Name,
                Summary = command.Summary,
                Description = command.Description,
                ImageFile = command.ImageFile,
                Brand = brand,
                Type = type,
                Price = command.Price,
                CreatedDate = DateTime.UtcNow
            };
        }

        public static Product ToUpdateEntity(this UpdateProductCommand command, Product existingProduct, ProductBrand brand, ProductType type)
        {
            return new Product
            {
                Id = existingProduct.Id,
                Name = command.Name,
                Summary = command.Summary,
                Description = command.Description,
                ImageFile = command.ImageFile,
                Brand = brand,
                Type = type,
                Price = command.Price,
                CreatedDate = existingProduct.CreatedDate
            };
        }

        public static ProductDto ToDto(this ProductResponse product)
        {
            if (product == null) return null;

            return new ProductDto(
                product.Id,
                product.Name,
                product.Summary,
                product.Description,
                product.ImageFile,
                new BrandDto(product.Brand.Id, product.Brand.Name),
                new TypeDto(product.Type.Id, product.Type.Name),
                product.Price,
                product.CreatedDate
            );
        }

    }
}
