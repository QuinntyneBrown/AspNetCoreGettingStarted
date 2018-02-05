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
    public class GetDashboardByIdQuery
    {
        public class Request : BaseRequest, IRequest<Response> { 
            public int DashboardId { get; set; }            
        }

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
                return new Response()
                {
                    Dashboard = DashboardApiModel.FromDashboard(await _context.Dashboards
                    .Include(x => x.Tenant)	
                    .Include(x => x.DashboardTiles)
                    .Include("DashboardTiles.Tile")
					.SingleAsync(x=>x.DashboardId == request.DashboardId &&  x.Tenant.TenantId == request.TenantId))
                };
            }

            private readonly IAspNetCoreGettingStartedContext _context;
            private readonly ICache _cache;
        }

    }

}
