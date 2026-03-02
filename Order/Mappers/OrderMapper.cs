using Order.Commands;
using Order.DTOs;
using Order.Entities;

namespace Order.Mappers
{
    public static class OrderMapper
    {
        public static OrderingDto ToDto(this OrderEntity order) =>
            new(order.Id!, order.UserName!, order.TotalPrice ?? 0, order.FirstName!, order.LastName!,
                order.Email!, order.Address!, order.State!, order.Country!, order.ZipCode!, order.CardName!,
                order.CardNumber!, order.Cvv!, order.PaymentMethod ?? 0);

        public static OrderEntity ToEntity(this CheckoutOrderCommand command)
        {
            return new OrderEntity
            {
                UserName = command.UserName,
                TotalPrice = command.TotalPrice,
                FirstName = command.FirstName,
                LastName = command.LastName,
                Email = command.Email,
                Address = command.Address,
                Country = command.Country,
                State = command.State,
                ZipCode = command.ZipCode,
                CardName = command.CardName,
                CardNumber = command.CardNumber,
                Expiration = command.Expiration,
                Cvv = command.Cvv,
                PaymentMethod = command.PaymentMethod
            };

        }

        public static void MapUpdate(this OrderEntity orderToUpdate, UpdateOrderCommand request)
        {
            orderToUpdate.UserName = request.UserName;
            orderToUpdate.TotalPrice = request.TotalPrice;
            orderToUpdate.FirstName = request.FirstName;
            orderToUpdate.LastName = request.LastName;
            orderToUpdate.Email = request.Email;
            orderToUpdate.Address = request.Address;
            orderToUpdate.Country = request.Country;
            orderToUpdate.State = request.State;
            orderToUpdate.ZipCode = request.ZipCode;
            orderToUpdate.CardName = request.CardName;
            orderToUpdate.CardNumber = request.CardNumber;
            orderToUpdate.Expiration = request.Expiration;
            orderToUpdate.Cvv = request.Cvv;
            orderToUpdate.PaymentMethod = request.PaymentMethod;
        }


    }


}
