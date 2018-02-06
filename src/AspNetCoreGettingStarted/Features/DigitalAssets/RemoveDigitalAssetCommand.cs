using MediatR;
using AspNetCoreGettingStarted.Data;
using System.Threading.Tasks;
using AspNetCoreGettingStarted.Features.Core;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System;
using AspNetCoreGettingStarted.Model;
using System.Linq;

namespace AspNetCoreGettingStarted.Features.DigitalAssets
{
    public class RemoveDigitalAssetCommand
    {
        public class Request : BaseAuthenticatedRequest, IRequest<Response>
        {
            public Guid Id { get; set; }
        }

        public class Response { }

        public class Handler : IRequestHandler<Request, Response>
        {
            public Handler(IAspNetCoreGettingStartedContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                DigitalAsset digitalAsset = default(DigitalAsset);

                var s = _context.DigitalAssets.ToList();

                try
                {
                    digitalAsset = await _context.DigitalAssets
                        .Include(x => x.Tenant)
                        .SingleAsync(x => x.DigitalAssetId == request.Id && x.Tenant.TenantId == request.TenantId);
                }
                catch(Exception e)
                {
                    throw e;
                }
                _context.DigitalAssets.Remove(digitalAsset);

                await _context.SaveChangesAsync(cancellationToken);

                _cache.Remove(DigitalAssetsCacheKeyFactory.Get(request.TenantId));
                _cache.Remove(DigitalAssetsCacheKeyFactory.GetByUniqueId(request.TenantId,digitalAsset.DigitalAssetId));

                return new Response();
            }

            private readonly IAspNetCoreGettingStartedContext _context;
            private readonly ICache _cache;
        }
    }
}
