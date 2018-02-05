using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreGettingStarted.Configuration
{
    internal static class Services
    {
        internal static void CreateIoCContainer(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IHttpContextAccessor, HttpContextAccessor>();

        }
    }
}
