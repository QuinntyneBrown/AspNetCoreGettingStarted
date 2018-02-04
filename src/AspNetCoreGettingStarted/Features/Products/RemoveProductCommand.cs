using AspNetCoreGettingStarted.Data;
using AspNetCoreGettingStarted.Features.Core;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreGettingStarted.Features.Products
{
    public class RemoveProductCommand
    {
        public class Request : BaseAuthenticatedRequest, IRequest<Response>
        {
            public int Id { get; set; }
        }

        public class Response { }

        public class RemoveProductHandler : IRequestHandler<Request, Response>
        {
            public RemoveProductHandler(IAspNetCoreGettingStartedContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var product = await _context.Products
                    .Include(x => x.Tenant)
                    .SingleAsync(x => x.ProductId == request.Id && x.Tenant.TenantId == request.TenantId);

                _context.Products.Remove(product);
                await _context.SaveChangesAsync(cancellationToken);

                return new Response();
            }

            private readonly IAspNetCoreGettingStartedContext _context;
            private readonly ICache _cache;
        }
    }
}
