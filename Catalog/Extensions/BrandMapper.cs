using Catalog.Entities;
using Catalog.Responses;

namespace Catalog.Extensions
{
    public static class BrandMapper
    {
        public static BrandResponse ToResponse(this ProductBrand brand)
        {
            return new BrandResponse
            {
                Id = brand.Id,
                Name = brand.Name
            };
        }

        public static IList<BrandResponse> ToRepsonseList(this IEnumerable<ProductBrand> brands)
        {
            return brands.Select(brand => brand.ToResponse()).ToList();
        }
    }
}
