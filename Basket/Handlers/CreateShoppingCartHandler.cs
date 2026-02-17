using Basket.Commands;
using Basket.GrpcService;
using Basket.Mappers;
using Basket.Repositories;
using Basket.Responses;
using MediatR;

namespace Basket.Handlers
{
    public class CreateShoppingCartHandler : IRequestHandler<CreateShoppingCartCommand, ShoppingCartResponse>
    {
        private readonly IBasketRepository _basketRepository;
        private readonly DiscountGrpcService _discountGrpcService;

        public CreateShoppingCartHandler(IBasketRepository basketRepository, DiscountGrpcService discountGrpcService)
        {
            _basketRepository = basketRepository;
            _discountGrpcService = discountGrpcService;
        }
        public async Task<ShoppingCartResponse> Handle(CreateShoppingCartCommand request, CancellationToken cancellationToken)
        {
            //Apply discounts to each item in the shopping cart
            foreach (var item in request.Items)
            {
                var discount = await _discountGrpcService.GetDiscount(item.ProductName);
                item.Price -= discount.Amount;
            }
            //Convert command to domain entity
            var shoppingCart = request.ToEntity();

            //Save to Redis - Upsert operation
            var updatedCart = await _basketRepository.UpsertBasket(shoppingCart);

            //Convert back to response
            return updatedCart.ToResponse();
        }
    }
}
