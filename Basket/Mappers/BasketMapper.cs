using Basket.Commands;
using Basket.DTOs;
using Basket.Entities;
using Basket.Responses;
using EventBus.Messages.Events;
using Microsoft.AspNetCore.Http.Features;

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

        public static ShoppingCart ToEntity(this ShoppingCartResponse response)
        {
            return new ShoppingCart(response.UserName)
            {
                Items = response.Items.Select(item => new CartItem
                {
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    Price = item.Price,
                    Quantity = item.Quantity
                }).ToList()
            };
        }

        public static BasketCheckoutEvent ToBasketCheckoutEvent(this BasketCheckoutDto dto, ShoppingCart basket)
        {
            return new BasketCheckoutEvent
            {
                UserName = dto.Username,
                TotalPrice = basket.Items.Sum(item => item.Price * item.Quantity),
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Address = dto.Address,
                Country = dto.Country,
                State = dto.State,
                ZipCode = dto.ZipCode,
                CardName = dto.Cardname,
                CardNumber = dto.CardNumber,
                CardExpiration = dto.Expiration,
                CVV = dto.Cvv,
                PaymentMethod = dto.PaymentMethod
                
            };
        }
    }
}
