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

namespace AspNetCoreGettingStarted.Features.Tiles
{
    public class AddOrUpdateTileCommand
    {
        public class Request : BaseRequest, IRequest<Response>
        {
            public TileApiModel Tile { get; set; }            
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
                var entity = await _context.Tiles
                    .Include(x => x.Tenant)
                    .SingleOrDefaultAsync(x => x.TileId == request.Tile.TileId && x.Tenant.TenantId == request.TenantId);
                
                if (entity == null) {
                    var tenant = await _context.Tenants.SingleAsync(x => x.TenantId == request.TenantId);
                    _context.Tiles.Add(entity = new Tile() { Tenant = tenant });
                }

                entity.Name = request.Tile.Name;
                
                await _context.SaveChangesAsync();

                return new Response();
            }

            private readonly IAspNetCoreGettingStartedContext _context;
        }
    }
}
