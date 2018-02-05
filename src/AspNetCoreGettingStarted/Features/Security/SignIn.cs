using AspNetCoreGettingStarted.Features.Core;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCoreGettingStarted.Features.Security
{
    public class SignIn
    {
        public class Request: BaseRequest, IRequest<Response> {
            public string UserName { get; set; }
            public string Password { get; set; }
        }

        public class Response {
            public string AccessToken { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            public Handler(IMediator mediator)
            {
                _mediator = mediator;
            }
            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {                
                if (await IsAuthenticated(request) == false)
                    throw new Exception("Invalid Username or Password!");

                var response = await _mediator.Send(new GetJwtTokenQuery.Request() { Username = request.UserName });

                return new Response() {
                    AccessToken = response.AccessToken
                };
            }

            private async Task<bool> IsAuthenticated(Request request) {
                var response = await _mediator.Send(new AuthenticateCommand.Request()
                {
                    Username = request.UserName,
                    Password = request.Password,
                    TenantId = request.TenantId
                });

                return response.IsAuthenticated;
            }

            private readonly IMediator _mediator;
        }
    }
}
