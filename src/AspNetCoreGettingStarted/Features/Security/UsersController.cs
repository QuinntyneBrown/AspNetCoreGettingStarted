using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AspNetCoreGettingStarted.Features.Security
{
    [Route("api/users")]
    public class UsersController: Controller
    {
        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [Route("current")]
        [HttpGet]
        public async Task<IActionResult> Current()
            => Ok(await _mediator.Send(new GetCurrentUserQuery.Request()));

        [AllowAnonymous]
        [Route("signin")]
        [HttpPost]
        public async Task<IActionResult> SignIn([FromBody]SignIn.Request request)
            => Ok(await _mediator.Send(request));

        private readonly IMediator _mediator;
    }
}
