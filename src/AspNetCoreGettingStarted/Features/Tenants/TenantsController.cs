using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AspNetCoreGettingStarted.Features.Tenants
{    
    [Route("api/tenants")]
    public class TenantController : Controller
    {
        public TenantController(IMediator mediator){
            _medidator = mediator;
        }

        [AllowAnonymous]
        [Route("verify")]
        public async Task<IActionResult> Verify([FromBody]VerifyTenantCommand.Request request) {
            await _medidator.Send(request);
            return Ok();
        } 

        private readonly IMediator _medidator;
    }
}
