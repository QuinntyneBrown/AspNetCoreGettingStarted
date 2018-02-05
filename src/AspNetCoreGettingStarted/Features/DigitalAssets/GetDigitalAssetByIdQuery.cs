using AspNetCoreGettingStarted.Data;
using AspNetCoreGettingStarted.Features.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System;
using System.Threading;

namespace AspNetCoreGettingStarted.Features.DigitalAssets
{
    public class GetDigitalAssetByIdQuery
    {
        public class Request : BaseAuthenticatedRequest, IRequest<Response>
        {
            public Guid DigitalAssetId { get; set; }
        }

        public class Response
        {
            public DigitalAssetApiModel DigitalAsset { get; set; }
        }

        public class GetDigitalAssetByUniqueIdHandler : IRequestHandler<Request, Response>
        {
            public GetDigitalAssetByUniqueIdHandler(AspNetCoreGettingStartedContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                return new Response()
                {
                    DigitalAsset = DigitalAssetApiModel.FromDigitalAsset(await _cache.FromCacheOrServiceAsync(() => _context
                    .DigitalAssets
                    .Include(x => x.Tenant)
                    .SingleAsync(x => x.DigitalAssetId == request.DigitalAssetId && x.Tenant.TenantId == request.TenantId),DigitalAssetsCacheKeyFactory.GetByUniqueId(request.TenantId,request.DigitalAssetId)))
                };
            }

            private readonly AspNetCoreGettingStartedContext _context;
            private readonly ICache _cache;
        }
    }
}
