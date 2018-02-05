using MediatR;
using AspNetCoreGettingStarted.Data;
using AspNetCoreGettingStarted.Model;
using AspNetCoreGettingStarted.Features.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace AspNetCoreGettingStarted.Features.Dashboards
{
    public class RemoveDashboardCommand
    {
        public class Request : BaseAuthenticatedRequest, IRequest<Response>
        {
            public int DashboardId { get; set; }
        }

        public class Response { }

        public class Handler : IRequestHandler<Request, Response>
        {
            public Handler(IAspNetCoreGettingStartedContext context)
            {
                _context = context;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var dashboard = await _context.Dashboards
                    .Include(x => x.Tenant)
                    .SingleAsync(x=>x.DashboardId == request.DashboardId && x.Tenant.TenantId == request.TenantId);

                _context.Dashboards.Remove(dashboard);
                await _context.SaveChangesAsync(cancellationToken);
                return new Response();
            }

            private readonly IAspNetCoreGettingStartedContext _context;
        }
    }
}
