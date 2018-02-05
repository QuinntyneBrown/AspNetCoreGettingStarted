using MediatR;
using AspNetCoreGettingStarted.Data;
using AspNetCoreGettingStarted.Features.Core;
using System;
using System.Threading.Tasks;
using System.Threading;

namespace AspNetCoreGettingStarted.Features.Tenants
{
    public class VerifyTenantCommand
    {
        public class Request : IRequest
        {
            public Guid TenantId { get; set; }
        }

        public class Response { }

        public class Handler : IRequestHandler<Request>
        {
            public Handler(IAspNetCoreGettingStartedContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            Task IRequestHandler<Request>.Handle(Request request, CancellationToken cancellationToken)
            {
                if (request.TenantId != new Guid("bad9a182-ede0-418d-9588-2d89cfd555bd"))
                    throw new Exception("Invalid Request");

                return Task.CompletedTask;
            }

            private readonly IAspNetCoreGettingStartedContext _context;
            private readonly ICache _cache;
        }
    }
}