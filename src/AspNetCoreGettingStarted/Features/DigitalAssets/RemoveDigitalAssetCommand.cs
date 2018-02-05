using MediatR;
using AspNetCoreGettingStarted.Data;
using System.Threading.Tasks;
using AspNetCoreGettingStarted.Features.Core;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System;

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
                var digitalAsset = await _context.DigitalAssets
                    .Include(x => x.Tenant)
                    .SingleAsync(x => x.DigitalAssetId == request.Id && x.Tenant.TenantId == request.TenantId);

                digitalAsset.IsDeleted = true;

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
