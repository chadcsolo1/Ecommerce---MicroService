using MassTransit;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Order.Data;
using EventBus.Messages.Events;

namespace Order.Dispatcher
{
    public class OutboxMessageDispatcher : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<OutboxMessageDispatcher> _logger;

        public OutboxMessageDispatcher(IServiceProvider serviceProvider, ILogger<OutboxMessageDispatcher> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // This  method would contain logic to read outbox messages from the database
            // and publish them to the message bus, ensuring reliable message delivery.

            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _serviceProvider.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<OrderContext>();

                var publishEndpoint = scope.ServiceProvider.GetRequiredService<IPublishEndpoint>();

                // Add logic to process outbox messages using dbContext
                var pendingMessages = await dbContext.OutboxMessages
                    .Where(x => x.isProcessed == null)
                    .OrderBy(x => x.OccurredOn)
                    .Take(20)
                    .ToListAsync(stoppingToken);

                // Process each pending message
                foreach (var message in pendingMessages)
                {
                    // Add logic to publish the message to the message bus
                    // and mark it as processed in the database
                    try
                    {
                        //var dynamicData = JsonConvert.DeserializeObject<dynamic>(message.Content);
                        var orderCreatedEvent = JsonConvert.DeserializeObject<OrderCreatedEvent>(message.Content);
                        await publishEndpoint.Publish(orderCreatedEvent);

                        message.ProcessedOn = DateTime.UtcNow;
                        _logger.LogInformation("Published outbox message {MessageId}", message.Id);
                    }catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error publishing outbox message {MessageId}", message.Id);
                    }
                }

                await dbContext.SaveChangesAsync(stoppingToken);
                await Task.Delay(5000, stoppingToken);
            }
            
        }
    }
}
