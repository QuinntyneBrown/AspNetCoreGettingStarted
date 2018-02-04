using MediatR;
using AspNetCoreGettingStarted.Data;
using AspNetCoreGettingStarted.Features.Core;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace AspNetCoreGettingStarted.Features.DigitalAssets
{
    public class GetDigitalAssetByIdQuery
    {
        public class Request : BaseAuthenticatedRequest, IRequest<Response> { 
			public int Id { get; set; }
		}

        public class Response
        {
            public DigitalAssetApiModel DigitalAsset { get; set; } 
		}

        public class GetDigitalAssetByIdHandler : IRequestHandler<Request, Response>
        {
            public GetDigitalAssetByIdHandler(IAspNetCoreGettingStartedContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var digitalAsset = await _context.DigitalAssets
                    .Include(x => x.Tenant)
                    .SingleAsync(x => x.DigitalAssetId == request.Id && x.Tenant.TenantId == request.TenantId);

                return new Response()
                {
                    DigitalAsset = DigitalAssetApiModel.FromDigitalAsset(digitalAsset)
                };
            }

            private readonly IAspNetCoreGettingStartedContext _context;
            private readonly ICache _cache;
        }
    }
}
