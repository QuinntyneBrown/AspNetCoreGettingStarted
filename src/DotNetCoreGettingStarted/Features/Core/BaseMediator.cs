using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DotNetCoreGettingStarted.Features.Core
{
    public class BaseMediator : IMediator
    {
        public BaseMediator(SingleInstanceFactory single, MultiInstanceFactory multi, IHttpContextAccessor httpContextAccessor)
        {
            _httpContext = httpContextAccessor.HttpContext;
            _inner = new Mediator(single, multi);
        }

        public Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default(CancellationToken)) where TNotification : INotification
        {
            return _inner.Publish(notification);
        }

        public Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default(CancellationToken))
        {            
            if (request.GetType().IsSubclassOf(typeof(BaseRequest)))
                (request as BaseRequest).TenantUniqueId = new Guid(_httpContext.Request.GetHeaderValue("Tenant"));

            if (request.GetType().IsSubclassOf(typeof(BaseAuthenticatedRequest)))
                (request as BaseAuthenticatedRequest).Username = _httpContext.User.Identity.Name;

            return _inner.Send(request);
        }

        public Task Send(IRequest request, CancellationToken cancellationToken = default(CancellationToken))
        {
            return _inner.Send(request);
        }

        private readonly HttpContext _httpContext;
        private readonly IMediator _inner;
    }
}
