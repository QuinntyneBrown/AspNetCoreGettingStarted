using MediatR;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AspNetCoreGettingStarted.Features.Core;

namespace AspNetCoreGettingStarted.Features.DashboardTiles
{
    [Authorize]
    [Route("api/dashboardtiles")]
    public class DashboardTileController : Controller
    {
        private readonly IMediator _mediator;
        public DashboardTileController(IMediator mediator) {
            _mediator = mediator;
        }
        
        [Route("add")]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody]AddOrUpdateDashboardTileCommand.Request request) => Ok(await _mediator.Send(request));

        [Route("update")]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody]AddOrUpdateDashboardTileCommand.Request request) => Ok(await _mediator.Send(request));
        
        [Route("get")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Get() => Ok(await _mediator.Send(new GetDashboardTilesQuery.Request()));

        [Route("getById")]
        [HttpGet]
        public async Task<IActionResult> GetById(GetDashboardTileByIdQuery.Request request) => Ok(await _mediator.Send(request));

        [Route("remove")]
        [HttpDelete]
        public async Task<IActionResult> Remove(RemoveDashboardTileCommand.Request request) => Ok(await _mediator.Send(request));
    }
}
