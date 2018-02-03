using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AspNetCoreGettingStarted.Features.Security
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class UserController: Controller
    {
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> SignIn([FromBody]AuthenticateCommand.Request request) {
            var authenticateResponse = await _mediator.Send(request);

            if (authenticateResponse.IsAuthenticated == false)
                return NotFound();

            return Ok(await _mediator.Send(new GetJwtTokenQuery.Request() { Username = request.Username }));
        }

        private readonly IMediator _mediator;
    }
}
