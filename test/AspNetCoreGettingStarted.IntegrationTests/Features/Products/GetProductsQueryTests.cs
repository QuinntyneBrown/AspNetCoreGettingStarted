using AspNetCoreGettingStarted.Features.Products;
using AspNetCoreGettingStarted.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using AspNetCoreGettingStarted.IntegrationTests.Extensions;
using AspNetCoreGettingStarted.IntegrationTests.Data;
using Microsoft.Extensions.Configuration;
using System.IO;
using AspNetCoreGettingStarted.Tests.Utilities;

namespace AspNetCoreGettingStarted.IntegrationTests.Features.Products
{
    public class GetProductsQueryTests
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public GetProductsQueryTests()
        {
            _server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>()
                .UseConfiguration(TestHelpers.GetAppSettings()));

            _client = _server.CreateClient();
        }

        [Fact]
        public async Task CanGetProducts()
        {
            _client.DefaultRequestHeaders.Add("Tenant", "bad9a182-ede0-418d-9588-2d89cfd555bd");

            var responseMessage = await _client.GetAsync("/api/products");

            responseMessage.EnsureSuccessStatusCode();

            var response = await responseMessage.Content.ReadAsAsync<GetProductsQuery.Response>();
            
            Assert.Equal(response.Products.Count, 0);
        }
    }
}