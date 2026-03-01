using MediatR;
using Order.DTOs;
using Order.Mappers;
using Order.Queries;
using Order.Repositories;

namespace Order.Handler
{
    public class GetOrderListHandler : IRequestHandler<GetOrderList, List<OrderingDto>>
    {
        private readonly IOrderRepository _orderRepository;
        public GetOrderListHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<List<OrderingDto>> Handle(GetOrderList request, CancellationToken cancellationToken)
        {
            var orders = await _orderRepository.GetOrderByUserNameAsync(request.UserName);
            return orders.Select(order => order.ToDto()).ToList();
        }
    }
}
