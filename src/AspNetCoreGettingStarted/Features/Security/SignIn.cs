using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCoreGettingStarted.Features.Security
{
    public class SignIn
    {
        public class Request: IRequest<Response> {
            public string UserName { get; set; }
            public string Password { get; set; }
            public Guid TenantUniqueId { get; set; }
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
                var authenticateResponse = await _mediator.Send(new AuthenticateCommand.Request() {
                    Username = request.UserName,
                    Password = request.Password,
                    TenantUniqueId = request.TenantUniqueId
                });

                if (authenticateResponse.IsAuthenticated == false)
                    throw new Exception("Invalid Username or Password!");

                var tokenResponse = await _mediator.Send(new GetJwtTokenQuery.Request() { Username = request.UserName });

                return new Response() {
                    AccessToken = tokenResponse.AccessToken
                };
            }

            private readonly IMediator _mediator;
        }
    }
}
