using EventBus.Messages.Events;
using Newtonsoft.Json;
using Order.Commands;
using Order.Constants;
using Order.DTOs;
using Order.Entities;
using System.Text.Json.Serialization;

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

        public static CheckoutOrderCommand ToCommand(this CreateOrderDto dto)
        {
            return new CheckoutOrderCommand
            {
                UserName = dto.UserName,
                TotalPrice = dto.TotalPrice,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Address = dto.Address,
                Country = dto.Country,
                State = dto.State,
                ZipCode = dto.ZipCode,
                CardNumber = dto.CardNumber,
                Expiration = dto.Expiration,
                Cvv = dto.Cvv,
                PaymentMethod = dto.PaymentMethod
            };

        }

        public static UpdateOrderCommand ToUpdateCommand(this OrderingDto dto)
        {
            return new UpdateOrderCommand
            {
                Id = dto.Id,
                UserName = dto.UserName,
                TotalPrice = dto.TotalPrice,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Address = dto.Address,
                Country = dto.Country,
                State = dto.State,
                ZipCode = dto.ZipCode,
                CardNumber = dto.CardNumber,
                Expiration = dto.Expiration,
                Cvv = dto.Cvv,
                PaymentMethod = dto.PaymentMethod
            };
        }

        public static CheckoutOrderCommand ToCheckOrderCommand(this BasketCheckoutEvent message)
        {
            return new CheckoutOrderCommand
            {
                UserName = message.UserName!,
                TotalPrice = message.TotalPrice ?? 0,
                FirstName = message.FirstName!,
                LastName = message.LastName!,
                Email = message.Email!,
                Address = message.Address!,
                Country = message.Country!,
                State = message.State!,
                ZipCode = message.ZipCode!,
                CardName = message.CardName!,
                CardNumber = message.CardNumber!,
                Expiration = message.CardExpiration!,
                PaymentMethod = message.PaymentMethod ?? 0
            };
        }

        public static OutboxMessage ToOutboxMessage(OrderEntity order) 
        {
            return new OutboxMessage
            {
                CorrelationId = Guid.NewGuid().ToString(),
                Type = OutboxMessageTypes.OrderCreated.ToString(),
                OccurredOn = DateTime.UtcNow,
                Content = JsonConvert.SerializeObject(new
                {
                    order.Id,
                    order.UserName,
                    order.TotalPrice,
                    order.FirstName,
                    order.LastName,
                    order.Email,
                    order.Address,
                    order.Country,
                    order.State,
                    order.ZipCode,
                    //PCI Sensitive - in a production application we would not serialize the payment information
                    order.CardName,
                    order.CardNumber,
                    order.Expiration,
                    order.Cvv,
                    order.PaymentMethod,
                    order.Status
                })
            };
        }


    }


}
