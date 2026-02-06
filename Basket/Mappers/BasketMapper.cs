using Basket.Commands;
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

        public static ShoppingCart ToEntity(this CreateShoppingCartCommand command)
        {
            var shoppingCart = new ShoppingCart
            {
                UserName = command.UserName,
                Items = command.Items.Select(item => new CartItem
                {
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    Price = item.Price,
                    Quantity = item.Quantity,
                    ImageFile = item.ImageFile
                }).ToList()
            };
            return shoppingCart;
        }
    }
}
