using Basket.Entities;
using Basket.Responses;

namespace Basket.Mappers
{
    public static class BasketMapper
    {
        public static ShoppingCartResponse ToResponse(this ShoppingCart shoppingCart)
        {
            return new ShoppingCartResponse(
                shoppingCart.UserName,
                shoppingCart.Items.Select(item => new ShoppingCartItemResponse(
                    item.ProductId,
                    item.ProductName,
                    item.Price,
                    item.Quantity,
                    item.ImageFile)).ToList()
            );
        }
    }
}
