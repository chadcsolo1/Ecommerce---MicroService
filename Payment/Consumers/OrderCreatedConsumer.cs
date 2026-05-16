using EventBus.Messages.Events;
using MassTransit;

namespace Payment.Consumers
{
    public class OrderCreatedConsumer : IConsumer<OrderCreatedEvent>
    {
        private readonly IPublishEndpoint _publishedEndpoint;
        private readonly ILogger<OrderCreatedEvent> _logger;
        public OrderCreatedConsumer(IPublishEndpoint publishedEndpoint, ILogger<OrderCreatedEvent> logger)
        {
            _publishedEndpoint = publishedEndpoint;
            _logger = logger;
        }
        public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
        {
            var message = context.Message;
            _logger.LogInformation("Processing payment for Order Id: {OrderId}", message.Id);

            //Simulate for Payment Processing
            await Task.Delay(1000);
            if (message.TotalPrice > 0)
            {
                //Simulate Success
                var completedEvent = new PaymentCompletedEvent
                {
                    OrderId = message.Id,
                    CorrelationId = message.CorrelationId ?? Guid.NewGuid()
                };
            }
        }
    }
}
