using Catalog.DTOs;
using Catalog.Extensions;
using Catalog.Queries;
using Catalog.Specifications;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CatalogController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CatalogController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetAllProducts")]
        public async Task<ActionResult<IList<ProductDto>>> GetCatalogItems([FromQuery] CatalogSpecParams specParams)
        {
            var query = new GetAllProductsQuery(specParams);
            var results = await _mediator.Send(query);
            if (results == null)
            {
                return NotFound();
            }
            return Ok(results);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProductById(string id)
        {
            var query = new GetProductByIdQuery(id);
            var result = await _mediator.Send(query);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet("productName/{productName}")]
        public async Task<ActionResult<IList<ProductDto>>> GetProductByName(string productName)
        {
            var query = new GetProductByNameQuery(productName);
            var results = await _mediator.Send(query);
            if (results == null || !results.Any())
            {
                return NotFound();
            }

            var dtoList = results.Select(product => product.ToDto()).ToList();
            return Ok(dtoList);
        }


    }
}
