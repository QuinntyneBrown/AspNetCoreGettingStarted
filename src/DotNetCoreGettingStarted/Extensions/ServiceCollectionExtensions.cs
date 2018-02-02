using DotNetCoreGettingStarted.Data;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace DotNetCoreGettingStarted
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDataStores(this IServiceCollection services)
        {
            services.AddScoped<IDataContext, DataContext>();
            return services;
        }

    }
}