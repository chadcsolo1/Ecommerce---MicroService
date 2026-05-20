using EventBus.Messages.Events;
using MassTransit;
using Order.Entities;
using Order.Repositories;

namespace Order.EventBusConsumer
{
    public class PaymentCompletedConsumer : IConsumer<PaymentCompletedEvent>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger<PaymentCompletedConsumer> _logger;
        public PaymentCompletedConsumer(IOrderRepository orderRepository, ILogger<PaymentCompletedConsumer> logger)
        {
            _orderRepository = orderRepository;
            _logger = logger;
        }
        public async Task Consume(ConsumeContext<PaymentCompletedEvent> context)
        {
            var order = await _orderRepository.GetByIdAsync(context.Message.OrderId);

            if (order == null)
            {
                _logger.LogWarning("Order not found for Id: {OrderId}", context.Message.OrderId);
                return;
            }

            order.Status = OrderStatus.Paid;
            await _orderRepository.UpdateAsync(order);
            _logger.LogInformation("Order with Id: {OrderId} has been updated to Paid status", context.Message.OrderId);
        }
    }
}
