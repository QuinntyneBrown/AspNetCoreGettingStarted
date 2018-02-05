using AspNetCoreGettingStarted.Data;
using AspNetCoreGettingStarted.Features.Core;
using AspNetCoreGettingStarted.Model;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.EntityFrameworkCore;


namespace AspNetCoreGettingStarted.Features.Products
{
    public class AddOrUpdateProductCommand
    {
        public class Request : BaseAuthenticatedRequest, IRequest<Response>
        {
            public ProductApiModel Product { get; set; }
        }

        public class Response { }

        public class AddOrUpdateProductHandler : IRequestHandler<Request, Response>
        {
            public AddOrUpdateProductHandler(IAspNetCoreGettingStartedContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {                
                var entity = await _context.Products
                    .Include(x => x.Tenant)
                    .SingleOrDefaultAsync(x => x.ProductId == request.Product.ProductId && x.Tenant.TenantId == request.TenantId);

                if (entity == null) {
                    var tenant = await _context.Tenants.SingleAsync(x => x.TenantId == request.TenantId);

                    _context.Products.Add(entity = new Product() {
                        Tenant = tenant
                    });
                }

                entity.Name = request.Product.Name;
                
                await _context.SaveChangesAsync(cancellationToken);

                return new Response();
            }

            private readonly IAspNetCoreGettingStartedContext _context;
            private readonly ICache _cache;
        }
    }
}