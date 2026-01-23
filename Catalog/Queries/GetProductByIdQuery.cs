using Catalog.Responses;
using MediatR;

namespace Catalog.Queries
{
    public record GetProductByIdQuery(string id) : IRequest<ProductResponse>{ }
}
