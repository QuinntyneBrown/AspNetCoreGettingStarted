using AspNetCoreGettingStarted.Data;
using AspNetCoreGettingStarted.Features.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCoreGettingStarted.Features.Products
{
    public class GetProductsQuery
    {
        public class Request: BaseRequest, IRequest<Response> { }

        public class Response
        {
            public ICollection<ProductApiModel> Products = new HashSet<ProductApiModel>();
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            public Handler(IAspNetCoreGettingStartedContext dataContext, ICache cache)
            {
                _context = dataContext;
                _cache = cache;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken){
                var products = await _cache.FromCacheOrServiceAsync(() => _context.Products
                    .Include(x => x.Category)
                    .Include(x => x.Tenant)
                    .Select(x => ProductApiModel.From(x))
                    .ToListAsync(), "Products");

                return new Response() { Products = products };
            } 
            
            private IAspNetCoreGettingStartedContext _context;
            private ICache _cache;
        }
    }
}