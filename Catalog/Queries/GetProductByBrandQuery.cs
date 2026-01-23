using Catalog.Responses;
using MediatR;

namespace Catalog.Queries
{
    public record GetProductByBrandQuery(string brandName): IRequest<IList<ProductResponse>>;
}
