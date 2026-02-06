using Basket.Entities;

namespace Basket.Repositories
{
    public interface IBasketRepository
    {
        Task<ShoppingCart> GetBasketAsync(string userName);
        Task<ShoppingCart> UpsertBasket(ShoppingCart shoppingCart);
        Task DeleteBasket(string userName);
    }
}
