using DotNetCoreGettingStarted.Data;
using DotNetCoreGettingStarted.Features.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DotNetCoreGettingStarted.Features.Products
{
    public class GetProductsQuery
    {
        public class Request: IRequest<Response> { }

        public class Response
        {
            public ICollection<dynamic> Products = new HashSet<dynamic>();
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            public Handler(IDataContext dataContext, ICache cache)
            {
                _dataContext = dataContext;
                _cache = cache;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var products = await _dataContext.Products
                    .Include(x => x.Category)
                    .ToListAsync();

                return await _cache.FromCacheOrServiceAsync(() => Task.FromResult(new Response()), "Products");                
            }

            private IDataContext _dataContext;
            private ICache _cache;
        }
    }
}
