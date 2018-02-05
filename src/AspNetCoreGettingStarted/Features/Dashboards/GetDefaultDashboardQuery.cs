using MediatR;
using AspNetCoreGettingStarted.Data;
using AspNetCoreGettingStarted.Features.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace AspNetCoreGettingStarted.Features.Dashboards
{
    public class GetDefaultDashboardQuery
    {
        public class Request : BaseRequest, IRequest<Response> { }

        public class Response
        {
            public DashboardApiModel Dashboard { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            public Handler(IAspNetCoreGettingStartedContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var dashboard = await _context.Dashboards
                    .Include(x => x.Tenant)
                    .Include(x => x.DashboardTiles)
                    .Include("DashboardTiles.Tile")
                    .Where(x => x.Tenant.TenantId == request.TenantId)
                    .FirstOrDefaultAsync();

                return new Response()
                {
                    Dashboard = dashboard != null
                    ? DashboardApiModel.FromDashboard(dashboard) : null
                };                
            }

            private readonly IAspNetCoreGettingStartedContext _context;
            private readonly ICache _cache;
        }
    }
}
