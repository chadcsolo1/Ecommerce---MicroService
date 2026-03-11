using MediatR;
using Microsoft.AspNetCore.Mvc;
using Order.Commands;
using Order.DTOs;
using Order.Mappers;
using Order.Queries;

namespace Order.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<OrderController> _logger;
        public OrderController(IMediator mediator, ILogger<OrderController> logger)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet("{userName}", Name = "GetOrdersByUserName")]
        public async Task<ActionResult<IEnumerable<OrderingDto>>> GetOrdersByUserName([FromRoute] string userName)
        {
            //GetOrderList is our Query to retrieve orders.
            var query = new GetOrderList(userName);
            //Calling _mediator.Send(query) will sends the request to the GetOrderListHandler<GetOrderList, List<OrderingDto> handler
            //to process the query and return the list of orders for the specified user.
            var orders = await _mediator.Send(query);
            _logger.LogInformation("Orders retrieved successfully for user: {UserName}", userName);
            return Ok(orders);
        }

        //Testing purpose
        [HttpPost(Name = "CheckoutOrder")]
        public async Task<ActionResult<int>> CheckoutOrder([FromBody] CreateOrderDto createOrderDto)
        {
            var command = createOrderDto.ToCommand();
            var result = await _mediator.Send(command);
            _logger.LogInformation($"Order created successfully with Id: {result}");
            return Ok(result);
        }

        [HttpPut(Name = "UpdateOrder")]
        public async Task<IActionResult> UpdateOrder([FromBody] OrderingDto orderingDto)
        {
            var command = orderingDto.ToUpdateCommand();
            await _mediator.Send(command);
            _logger.LogInformation($"Order with Id: {orderingDto.Id} updated successfully.");
            return NoContent();
        }

        [HttpDelete("{id}", Name = "DeleteOrder")]
        public async Task<IActionResult> DeleteOrder([FromRoute] int id)
        {
            var command = new DeleteOrderCommand { Id = id };
            await _mediator.Send(command);
            _logger.LogInformation($"Order with Id: {id} deleted successfully.");
            return NoContent();
        }

    }
}
