using AspNetCoreGettingStarted.Data;
using AspNetCoreGettingStarted.Features.Core;
using AspNetCoreGettingStarted.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCoreGettingStarted.Features.Products
{
    public class GetProductByIdQuery
    {
        public class Request : BaseAuthenticatedRequest, IRequest<Response> { 
			public int Id { get; set; }
		}

        public class Response
        {
            public ProductApiModel Product { get; set; } 
		}

        public class GetProductByIdHandler : IRequestHandler<Request, Response>
        {
            public GetProductByIdHandler(IAspNetCoreGettingStartedContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                Product product = await _context.Products
                    .SingleAsync(x => x.Tenant.TenantId == request.TenantId);

                return new Response()
                {
                    Product = ProductApiModel.FromProduct(product)
                };
            }

            private readonly IAspNetCoreGettingStartedContext _context;
            private readonly ICache _cache;
        }
    }
}