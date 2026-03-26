using Basket.Commands;
using Basket.Mappers;
using Basket.Queries;
using MassTransit;
using MediatR;

namespace Basket.Handlers
{
    public class CheckoutBasketHandler : IRequestHandler<BasketCheckoutCommand, Unit>
    {
        private readonly IMediator _mediator;
        private readonly IPublishEndpoint _publishEnpoint;
        private readonly ILogger<CheckoutBasketHandler> _logger;

        public CheckoutBasketHandler(IMediator mediator, IPublishEndpoint publishEndpoint, ILogger<CheckoutBasketHandler> logger)
        {
            _mediator = mediator;
            _publishEnpoint = publishEndpoint;
            _logger = logger;
        }
        public async Task<Unit> Handle(BasketCheckoutCommand request, CancellationToken cancellationToken)
        {
            var dto = request.Dto;
            var basketResponse = await _mediator.Send(new GetBasketByUserNameQuery(dto.Username), cancellationToken);

            if (basketResponse is null || !basketResponse.Items.Any())
            {
                throw new InvalidOperationException("Basket not found or empty.");
            }

            //To entity maps a ShoppingCartResponse model into a ShoppingCart model
            var basket = basketResponse.ToEntity();
            //Takes the BasketCheckoutDto and the ShoppingCart object created above and converts this into a BasketCheckoutEvent object
            var evt = dto.ToBasketCheckoutEvent(basket);
            _logger.LogInformation("Publishing BasketCheckoutEvent for {User}", basket.UserName);
            //Publish event
            await _publishEnpoint.Publish(evt, cancellationToken);
            //delete the basket
            await _mediator.Send(new DeleteBasketByUserNameCommand(dto.Username), cancellationToken);
            return Unit.Value;
        }

    }
}
