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

namespace AspNetCoreGettingStarted.Features.DashboardTiles
{
    public class AddOrUpdateDashboardTileCommand
    {
        public class Request : BaseAuthenticatedRequest, IRequest<Response>
        {
            public DashboardTileApiModel DashboardTile { get; set; }  
            public int UserId { get; set; }
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
                var entity = await _context.DashboardTiles
                    .Include(x => x.Tenant)
                    .Include(x => x.Dashboard)
                    .Include("Dashboard.DashboardTiles")
                    .SingleOrDefaultAsync(x => x.DashboardTileId == request.DashboardTile.DashboardTileId && x.Tenant.TenantId == request.TenantId);

                if (entity == null)
                {
                    var tenant = await _context.Tenants.SingleAsync(x => x.TenantId == request.TenantId);
                    var dashboard = _context.Dashboards
                        .Include(x => x.DashboardTiles)
                        .Single(x => x.DashboardId == request.DashboardTile.DashboardId.Value
                        && x.Tenant.TenantId == request.TenantId);

                    _context.DashboardTiles.Add(entity = new DashboardTile() { Tenant = tenant });

                    dashboard.DashboardTiles.Add(entity);
                }

                entity.Name = request.DashboardTile.Name;
                entity.Top = request.DashboardTile.Top;
                entity.Left = request.DashboardTile.Left;
                entity.Width = request.DashboardTile.Width;
                entity.Height = request.DashboardTile.Height;

                await _context.SaveChangesAsync(cancellationToken);
                
                return new Response();
            }

            private readonly IAspNetCoreGettingStartedContext _context;
        }
    }
}
