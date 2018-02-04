using MediatR;
using AspNetCoreGettingStarted.Data;
using AspNetCoreGettingStarted.Features.Core;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System;
using System.IdentityModel.Tokens;
using AspNetCoreGettingStarted.Features.Security;
using AspNetCoreGettingStarted.Model;
using System.Threading;

namespace AspNetCoreGettingStarted.Features.DigitalAssets
{
    public class GetDigitalAssetByUniqueIdQuery
    {
        public class Request : IRequest<Response>
        {
            public string UniqueId { get; set; }
            public string OAuthToken { get; set; }
            public Guid TenantId { get; set; }
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
                if(string.IsNullOrEmpty(request.OAuthToken))
                    return new Response()
                    {
                        DigitalAsset = DigitalAssetApiModel.FromDigitalAsset(await _cache.FromCacheOrServiceAsync<DigitalAsset>(() => _context
                        .DigitalAssets
                        .Include(x => x.Tenant)
                        .SingleAsync(x => x.UniqueId.ToString() == request.UniqueId && x.IsSecure == false),DigitalAssetsCacheKeyFactory.GetByUniqueId(request.TenantId,request.UniqueId)))
                    };

                return new Response()
                {
                    DigitalAsset = DigitalAssetApiModel.FromDigitalAsset(await _context
                    .DigitalAssets
                    .Include(x => x.Tenant)
                    .SingleAsync(x => x.UniqueId.ToString() == request.UniqueId && x.Tenant.TenantId == request.TenantId))
                };
            }

            private readonly AspNetCoreGettingStartedContext _context;
            private readonly ICache _cache;
        }
    }
}
