using DotNetCoreGettingStarted.Data;
using DotNetCoreGettingStarted.Features.Core;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace DotNetCoreGettingStarted.Features.Products
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
            public Handler(IDataContext dataContext, ICache cache)
            {
                _context = dataContext;
                _cache = cache;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken){
                var products = await _cache.FromCacheOrServiceAsync(() => _context.Products
                    .Include(x => x.Category)
                    .Include(x => x.Tenant)
                    .Where(x => x.Tenant.TenantId == request.TenantUniqueId)
                    .Select(x => ProductApiModel.From(x))
                    .ToListAsync(), "Products");

                return new Response() { Products = products };
            } 
            
            private IDataContext _context;
            private ICache _cache;
        }
    }
}