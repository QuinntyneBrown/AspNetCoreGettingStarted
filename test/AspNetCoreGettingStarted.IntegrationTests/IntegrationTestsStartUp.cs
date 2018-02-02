using AspNetCoreGettingStarted.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCoreGettingStarted.IntegrationTests
{
    public class IntegrationTestsStartUp : Startup
    {
        public IntegrationTestsStartUp(IConfiguration configuration)
            : base(configuration) { }

        public override void AddDataStores(IServiceCollection services)
        {
            base.AddDataStores(services);
            services.AddScoped<IAspNetCoreGettingStartedContext, MockAspNetCoreGettingStartedContext>();
        }
    }
}
