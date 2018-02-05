using MediatR;
using AspNetCoreGettingStarted.Data;
using AspNetCoreGettingStarted.Features.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace AspNetCoreGettingStarted.Features.Tiles
{
    public class GetTileByIdQuery
    {
        public class Request : BaseRequest, IRequest<Response> { 
            public int Id { get; set; }            
        }

        public class Response
        {
            public TileApiModel Tile { get; set; } 
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
                    Tile = TileApiModel.FromTile(await _context.Tiles
                    .Include(x => x.Tenant)				
					.SingleAsync(x=>x.TileId == request.Id &&  x.Tenant.TenantId == request.TenantId))
                };
            }

            private readonly IAspNetCoreGettingStartedContext _context;
            private readonly ICache _cache;
        }

    }

}
