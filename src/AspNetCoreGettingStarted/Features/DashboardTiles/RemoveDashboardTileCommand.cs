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
    public class RemoveDashboardTileCommand
    {
        public class Request : BaseRequest, IRequest<Response>
        {
            public int Id { get; set; }
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
                var dashboardTile = await _context.DashboardTiles.SingleAsync(x=>x.DashboardTileId == request.Id && x.Tenant.TenantId == request.TenantId);
                dashboardTile.IsDeleted = true;
                await _context.SaveChangesAsync();
                return new Response();
            }

            private readonly IAspNetCoreGettingStartedContext _context;
        }
    }
}
