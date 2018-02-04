using AspNetCoreGettingStarted.Features.Core;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCoreGettingStarted
{
    public class AspNetCoreGettingStartedMediator : IMediator
    {
        private IMediator _inner;
        private IHttpContextAccessor _httpContextAccessor;

        public AspNetCoreGettingStartedMediator(MultiInstanceFactory multiInstanceFactory, SingleInstanceFactory singleInstanceFactory, IHttpContextAccessor httpContextAccessor)
        {
            _inner = new Mediator(singleInstanceFactory, multiInstanceFactory);
            _httpContextAccessor = httpContextAccessor;
        }
        public Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default(CancellationToken)) where TNotification : INotification
        {
            return _inner.Publish(notification, cancellationToken);
        }

        public Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (request.GetType().IsSubclassOf(typeof(BaseRequest)))
                (request as BaseRequest).TenantId = new Guid(_httpContextAccessor.HttpContext.Request.GetHeaderValue("Tenant"));

            if (request.GetType().IsSubclassOf(typeof(BaseAuthenticatedRequest)))
                (request as BaseAuthenticatedRequest).Username = _httpContextAccessor.HttpContext.User.Identity.Name;

            return _inner.Send(request, cancellationToken);
        }

        public Task Send(IRequest request, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (request.GetType().IsSubclassOf(typeof(BaseRequest)))
                (request as BaseRequest).TenantId = new Guid(_httpContextAccessor.HttpContext.Request.GetHeaderValue("Tenant"));

            if (request.GetType().IsSubclassOf(typeof(BaseAuthenticatedRequest)))
                (request as BaseAuthenticatedRequest).Username = _httpContextAccessor.HttpContext.User.Identity.Name;

            return _inner.Send(request, cancellationToken);
        }
    }
}
