using Basket.Entities;
using Basket.Queries;
using Basket.Repositories;
using Basket.Responses;
using MediatR;

namespace Basket.Handlers
{
    public class GetBasketByUserNameHandler : IRequestHandler<GetBasketByUserNameQuery, ShoppingCartResponse>
    {
        private readonly IBasketRepository _basketRepository;

        public GetBasketByUserNameHandler(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }
        public async Task<ShoppingCartResponse> Handle(GetBasketByUserNameQuery request, CancellationToken cancellationToken)
        {
            //Get Shopping Cart
            var basket = await _basketRepository.GetBasketAsync(request.UserName);
            
            if (basket == null)
            {
                return new ShoppingCartResponse(request.UserName)
                {
                    
                    Items = new List<ShoppingCartItemResponse>()
                };
            }
            return ShoppingCart.ToResponse();
        }
    }
}
