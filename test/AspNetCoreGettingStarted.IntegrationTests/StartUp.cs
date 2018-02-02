using AspNetCoreGettingStarted.Data;
using AspNetCoreGettingStarted.IntegrationTests.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCoreGettingStarted.IntegrationTests
{
    public class StartUp : AspNetCoreGettingStarted.Startup
    {
        public StartUp(IConfiguration configuration)
            : base(configuration) { }

        public override void AddDataStores(IServiceCollection services)
        {
            base.AddDataStores(services);
            services.AddScoped<IAspNetCoreGettingStartedContext, MockAspNetCoreGettingStartedContext>();
        }
    }
}
