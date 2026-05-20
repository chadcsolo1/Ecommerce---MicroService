using EventBus.Messages.Events;
using MassTransit;
using Order.Entities;
using Order.Repositories;

namespace Order.EventBusConsumer
{
    public class PaymentFailedConsumer : IConsumer<PaymentFailedEvent>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger<PaymentFailedConsumer> _logger;
        public PaymentFailedConsumer(IOrderRepository orderRepository, ILogger<PaymentFailedConsumer> logger)
        {
            _orderRepository = orderRepository;
            _logger = logger;
        }
        public async Task Consume(ConsumeContext<PaymentFailedEvent> context)
        {
            var order = await _orderRepository.GetByIdAsync(context.Message.OrderId);

            if (order == null)
            {
                _logger.LogError($"Order with id {context.Message.OrderId} not found.");
                return;
            }

            order.Status = OrderStatus.Failed;
            await _orderRepository.UpdateAsync(order);
            _logger.LogInformation("Payment failed for OrderId: {OrderId}, Reason: {Reason}", context.Message.OrderId, context.Message.Reason);
        }
    }
}
