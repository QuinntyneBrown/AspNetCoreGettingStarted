using MediatR;
using AspNetCoreGettingStarted.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using AspNetCoreGettingStarted.Model;
using static AspNetCoreGettingStarted.Features.DigitalAssets.Constants;
using AspNetCoreGettingStarted.Features.Core;
using System.Threading;

namespace AspNetCoreGettingStarted.Features.DigitalAssets
{
    public class GetDigitalAssetsQuery
    {
        public class Request : BaseAuthenticatedRequest, IRequest<Response> { }

        public class Response
        {
            public ICollection<DigitalAssetApiModel> DigitalAssets { get; set; } = new HashSet<DigitalAssetApiModel>();
        }

        public class GetDigitalAssetsHandler : IRequestHandler<Request, Response>
        {
            public GetDigitalAssetsHandler(IAspNetCoreGettingStartedContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var digitalAssets = await _cache.FromCacheOrServiceAsync<List<DigitalAsset>>(() => _context
                .DigitalAssets
                .Include(x => x.Tenant)
                .Where(x => x.Tenant.TenantId == request.TenantId)
                .ToListAsync(), DigitalAssetsCacheKeyFactory.Get(request.TenantId));

                return new Response()
                {
                    DigitalAssets = digitalAssets.Select(x => DigitalAssetApiModel.FromDigitalAsset(x)).ToList()
                };
            }

            private readonly IAspNetCoreGettingStartedContext _context;
            private readonly ICache _cache;
        }
    }
}