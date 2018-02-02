using AspNetCoreGettingStarted.Data;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCoreGettingStarted
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDataStores(this IServiceCollection services)
        {
            services.AddScoped<IAspNetCoreGettingStartedContext, AspNetCoreGettingStartedContext>();
            return services;
        }

    }
}