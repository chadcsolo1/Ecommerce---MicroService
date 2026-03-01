using MediatR;
using Order.DTOs;

namespace Order.Queries
{
    public record GetOrderList(string UserName) : IRequest<List<OrderingDto>>;
}
