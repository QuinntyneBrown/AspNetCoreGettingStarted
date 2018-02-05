using MediatR;
using AspNetCoreGettingStarted.Data;
using AspNetCoreGettingStarted.Features.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace AspNetCoreGettingStarted.Features.DashboardTiles
{
    public class GetDashboardTileByIdQuery
    {
        public class Request : BaseRequest, IRequest<Response> { 
            public int Id { get; set; }            
        }

        public class Response
        {
            public DashboardTileApiModel DashboardTile { get; set; } 
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
                    DashboardTile = DashboardTileApiModel.FromDashboardTile(await _context.DashboardTiles
                    .Include(x => x.Tenant)				
					.SingleAsync(x=>x.DashboardTileId == request.Id &&  x.Tenant.TenantId == request.TenantId))
                };
            }

            private readonly IAspNetCoreGettingStartedContext _context;
            private readonly ICache _cache;
        }

    }

}
