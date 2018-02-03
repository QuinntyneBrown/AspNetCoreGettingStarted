using AspNetCoreGettingStarted.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCoreGettingStarted.Features.Security
{
    public class GetClaimsForUserQuery
    {
        public class Request : IRequest<Response>
        {
            public string Username { get; set; }
            public Guid TenantUniqueId { get; set; }
        }

        public class Response
        {
            public ICollection<Claim> Claims { get; set; }
        }

        public class GetClaimsForUserHandler : IRequestHandler<Request, Response>
        {
            public GetClaimsForUserHandler(IAspNetCoreGettingStartedContext context)
            {
                _context = context;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var claims = new List<Claim>();

                var user = await _context.Users
                    .Include(x => x.Tenant)
                    .SingleAsync(x => x.UserName == request.Username);

                claims.Add(new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name", request.Username));
                
                return new Response()
                {
                    Claims = claims
                };
            }

            private readonly IAspNetCoreGettingStartedContext _context;
        }
    }
}
