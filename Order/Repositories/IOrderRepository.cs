using Order.Entities;

namespace Order.Repositories
{
    public interface IOrderRepository : IAsyncRepository<OrderEntity>
    {
        Task<IEnumerable<OrderEntity>> GetOrderByUserNameAsync(string userName);
        Task AddOutboxMessageAsync(OutboxMessage outboxMessage);
    }
}
