using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNetCoreGettingStarted.Features.Products
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<ActionResult> Get() => Ok(await _mediator.Send(new GetProductsQuery.Request()));
        
        private IMediator _mediator;
    }
}