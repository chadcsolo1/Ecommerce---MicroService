using MediatR;
using Order.Commands;
using Order.Entities;
using Order.Exceptions;
using Order.Repositories;

namespace Order.Handler
{
    public class DeleteOrderHandler : IRequestHandler<DeleteOrderCommand, Unit>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger<CheckoutOrderHandler> _logger;
        public DeleteOrderHandler(IOrderRepository orderRepository, ILogger<CheckoutOrderHandler> logger)
        {
            _orderRepository = orderRepository;
            _logger = logger;
        }
        public async Task<Unit> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var orderToDelete = await _orderRepository.GetByIdAsync(request.Id);

            if (orderToDelete == null)
            {
                throw new OrderNotFoundException(nameof(OrderEntity), request.Id);
            }

            await _orderRepository.DeleteAsync(orderToDelete);
            _logger.LogInformation($"Order with Id {request.Id} has been successfully deleted.");
            return Unit.Value;
        }
    }
}
