using Catalog.Entities;
using Catalog.Responses;

namespace Catalog.Extensions
{
    public static class TypeMapper
    {
        public static TypeResponse ToResponse(this ProductType type)
        {
            return new TypeResponse
            {
                Id = type.Id,
                Name = type.Name
            };
        }
        public static IList<TypeResponse> ToResponseList(this IEnumerable<ProductType> types)
        {
            return types.Select(type => type.ToResponse()).ToList();
        }
    }
}
