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
    }
}
