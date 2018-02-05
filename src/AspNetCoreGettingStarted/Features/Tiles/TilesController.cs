using MediatR;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreGettingStarted.Features.Tiles
{
    [Authorize]
    [Route("api/tiles")]
    public class TileController : Controller
    {
        private readonly IMediator _mediator;
        public TileController(IMediator mediator) {
            _mediator = mediator;
        }
        
        [Route("add")]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody]AddOrUpdateTileCommand.Request request) => Ok(await _mediator.Send(request));

        [Route("update")]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody]AddOrUpdateTileCommand.Request request) => Ok(await _mediator.Send(request));
        
        [Route("get")]
        [HttpGet]
        public async Task<IActionResult> Get() => Ok(await _mediator.Send(new GetTilesQuery.Request()));

        [Route("getById")]
        [HttpGet]
        public async Task<IActionResult> GetById(GetTileByIdQuery.Request request) => Ok(await _mediator.Send(request));

        [Route("remove")]
        [HttpDelete]
        public async Task<IActionResult> Remove(RemoveTileCommand.Request request) => Ok(await _mediator.Send(request));

    }
}
