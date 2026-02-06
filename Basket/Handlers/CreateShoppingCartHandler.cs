using Basket.Commands;
using Basket.Mappers;
using Basket.Repositories;
using Basket.Responses;
using MediatR;

namespace Basket.Handlers
{
    public class CreateShoppingCartHandler : IRequestHandler<CreateShoppingCartCommand, ShoppingCartResponse>
    {
        private readonly IBasketRepository _basketRepository;

        public CreateShoppingCartHandler(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }
        public async Task<ShoppingCartResponse> Handle(CreateShoppingCartCommand request, CancellationToken cancellationToken)
        {
            //Convert command to domain entity
            var shoppingCart = request.ToEntity();

            //Save to Redis - Upsert operation
            var updatedCart = await _basketRepository.UpsertBasket(shoppingCart);

            //Convert back to response
            return updatedCart.ToResponse();
        }
    }
}
