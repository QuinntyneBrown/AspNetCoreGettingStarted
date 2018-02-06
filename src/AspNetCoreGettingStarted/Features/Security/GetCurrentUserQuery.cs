using MediatR;
using AspNetCoreGettingStarted.Data;
using AspNetCoreGettingStarted.Features.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace AspNetCoreGettingStarted.Features.Security
{
    public class GetCurrentUserQuery
    {
        public class Request : BaseAuthenticatedRequest, IRequest<Response> { }

        public class Response
        {
            public UserApiModel User { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            public Handler(IAspNetCoreGettingStartedContext context, ICache cache, IHttpContextAccessor httpContextAccessor)
            {
                _context = context;
                _cache = cache;
                _httpContextAccessor = httpContextAccessor;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var user = await _context.Users
                    .Include(x => x.Tenant)
                    .SingleAsync(x => x.Tenant.TenantId == request.TenantId && x.UserName == request.Username);

                return new Response() { User = UserApiModel.FromUser(user) };
            }

            private readonly IAspNetCoreGettingStartedContext _context;
            private readonly ICache _cache;
            private readonly IHttpContextAccessor _httpContextAccessor;
        }
    }
}
