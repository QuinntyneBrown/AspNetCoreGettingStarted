using AspNetCoreGettingStarted.Features.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCoreGettingStarted.Configuration
{
    internal static class Options
    {
        internal static void LoadConfigurationOptions(IServiceCollection services, IConfiguration configuration)
        {            
            services.Configure<AuthenticationSettings>(configuration.GetSection("Authentication"));
            services.Configure<SeedDataSettings>(configuration.GetSection("SeedData"));
        }
    }
}