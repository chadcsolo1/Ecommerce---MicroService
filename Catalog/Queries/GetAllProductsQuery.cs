using Catalog.Repositories;
using Catalog.Responses;
using Catalog.Specifications;
using MediatR;

namespace Catalog.Queries
{
    public record GetAllProductsQuery(CatalogSpecParams specParams) : IRequest<Pagination<ProductResponse>>
    {
    }
}
