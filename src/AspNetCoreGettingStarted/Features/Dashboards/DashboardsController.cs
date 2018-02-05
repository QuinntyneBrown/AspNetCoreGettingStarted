using MediatR;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AspNetCoreGettingStarted.Features.Core;

namespace AspNetCoreGettingStarted.Features.Dashboards
{
    [Authorize]
    [Route("api/dashboards")]
    public class DashboardController : Controller
    {
        private readonly IMediator _mediator;

        public DashboardController(IMediator mediator) {
            _mediator = mediator;
        }

        [Route("add")]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody]AddOrUpdateDashboardCommand.Request request) => Ok(await _mediator.Send(request));

        [Route("update")]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody]AddOrUpdateDashboardCommand.Request request) => Ok(await _mediator.Send(request));
        
        [Route("get")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Get() => Ok(await _mediator.Send(new GetDashboardsQuery.Request()));

        [Route("getById")]
        [HttpGet]
        public async Task<IActionResult> GetById(GetDashboardByIdQuery.Request request) => Ok(await _mediator.Send(request));

        [Route("getDefault")]
        [HttpGet]
        public async Task<IActionResult> GetDefault() => Ok(await _mediator.Send(new GetDefaultDashboardQuery.Request()));
        
        [Route("remove")]
        [HttpDelete]
        public async Task<IActionResult> Remove(RemoveDashboardCommand.Request request) => Ok(await _mediator.Send(request));

    }
}
