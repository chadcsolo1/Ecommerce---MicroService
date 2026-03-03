using Order.Entities;

namespace Order.Data
{
    public class OrderContextSeed
    {
        public static async Task SeedAsync(OrderContext orderContext, ILogger<OrderContextSeed> logger)
        {
            if (!orderContext.Orders.Any())
            {
                orderContext.Orders.AddRange(GetOrders());
                await orderContext.SaveChangesAsync();
                logger.LogInformation($"Ordering Database: {typeof(OrderContext).Name} seeded!!!");
            }
        }

        private static IEnumerable<OrderEntity> GetOrders()
        {
            return new List<OrderEntity>
            {
                new OrderEntity
                {
                    UserName = "RheaRhea",
                    FirstName = "Rhea",
                    LastName = "Solomon",
                    Email = "rhea_dog@dogmail.com",
                    Address = "123 Dog Street",
                    Country = "Dogland",
                    State = "Dogstate",
                    ZipCode = "12345",

                    CardName = "Rhea Solomon",
                    CardNumber = "1234 5678 9012 3456",
                    Expiration = "12/25",
                    Cvv = "123",
                    PaymentMethod = 1,
                    LastModifiedBy = "RheaRhea",
                    LastModifiedDate = DateTime.UtcNow
                }
            };
        }
    }
}
