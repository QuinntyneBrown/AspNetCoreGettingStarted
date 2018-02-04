using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreGettingStarted.Features.Products
{
    [Produces("application/json")]
    [Route("api/products")]
    [Authorize]
    public class ProductsController : Controller
    {
        public ProductsController(IMediator mediator) {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("get")]
        public async Task<ActionResult> Get() => Ok(await _mediator.Send(new GetProductsQuery.Request()));

        private readonly IMediator _mediator;
    }
}