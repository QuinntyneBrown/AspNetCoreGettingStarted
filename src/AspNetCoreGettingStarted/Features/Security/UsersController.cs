using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AspNetCoreGettingStarted.Features.Security
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class UsersController: Controller
    {
        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> SignIn([FromBody]SignIn.Request request)
            => Ok(await _mediator.Send(request));

        private readonly IMediator _mediator;
    }
}
