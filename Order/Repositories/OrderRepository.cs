using Microsoft.EntityFrameworkCore;
using Order.Data;
using Order.Entities;

namespace Order.Repositories
{
    public class OrderRepository : RepositoryBase<OrderEntity>, IOrderRepository
    {
        public OrderRepository(OrderContext dbContext): base(dbContext) { }


        public async Task<IEnumerable<OrderEntity>> GetOrderByUserNameAsync(string userName)
        {
            var orderList = await _dbContext.Orders
                .AsNoTracking() //For better performance
                .Where(x => x.UserName == userName)
                .ToListAsync();
            return orderList;
        }


        public async Task AddOutboxMessageAsync(OutboxMessage outboxMessage)
        {
            await _dbContext.OutboxMessages.AddAsync(outboxMessage);
            await _dbContext.SaveChangesAsync();
        }

    }
}
