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
    public class GetTilesQuery
    {
        public class Request : BaseRequest, IRequest<Response> { }

        public class Response
        {
            public ICollection<TileApiModel> Tiles { get; set; } = new HashSet<TileApiModel>();
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
                var tiles = await _context.Tiles
                    .Include(x => x.Tenant)
                    .Where(x => x.Tenant.TenantId == request.TenantId )
                    .ToListAsync();

                return new Response()
                {
                    Tiles = tiles.Select(x => TileApiModel.FromTile(x)).ToList()
                };
            }

            private readonly IAspNetCoreGettingStartedContext _context;
            private readonly ICache _cache;
        }
    }
}
