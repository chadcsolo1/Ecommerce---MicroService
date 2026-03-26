using Basket.Commands;
using Basket.DTOs;
using Basket.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Basket.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BasketController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{userName}")]
        public async Task<ActionResult<ShoppingCartDto>> GetBasket(string userName)
        {
            //Query
            var query = new GetBasketByUserNameQuery(userName);

            //Send the query to the Mediator - Implementing CQRS Pattern
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<ShoppingCartDto>> CreateOrUpdateBasket([FromBody] CreateShoppingCartCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("{userName}")]
        public async Task<IActionResult> DeleteBasket(string userName)
        {
            var command = new DeleteBasketByUserNameCommand(userName);
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> Checkout([FromBody] BasketCheckoutDto dto) 
        {
            await _mediator.Send(new BasketCheckoutCommand(dto));
            return Accepted();
        }
    }
}
