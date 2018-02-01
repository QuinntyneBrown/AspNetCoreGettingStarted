using DotNetCoreGettingStarted.Data;
using MediatR;
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
            public Handler(IDataContext dataContext)
            {
                _dataContext = dataContext;
            }

            public Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                return Task.FromResult(new Response());
            }

            private IDataContext _dataContext;
        }
    }
}
