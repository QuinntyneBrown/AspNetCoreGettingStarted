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
            _client.DefaultRequestHeaders.Add("Tenant", "cbe7af58-306c-439a-a44f-9fee5e80ce52");

            var responseMessage = await _client.GetAsync("/api/products");

            responseMessage.EnsureSuccessStatusCode();

            var response = await responseMessage.Content.ReadAsAsync<GetProductsQuery.Response>();
            
            Assert.Equal(response.Products.Count, 0);
        }
    }
}