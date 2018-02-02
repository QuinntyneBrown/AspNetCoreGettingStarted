using AspNetCoreGettingStarted.Features.Products;
using AspNetCoreGettingStarted.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using AspNetCoreGettingStarted.IntegrationTests.Extensions;
using AspNetCoreGettingStarted.IntegrationTests.Data;

namespace AspNetCoreGettingStarted.IntegrationTests.Features.Products
{
    public class GetProductsQueryTests
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public GetProductsQueryTests()
        {
            _server = new TestServer(new WebHostBuilder().UseStartup<StartUp>());

            _client = _server.CreateClient();
        }

        [Fact]
        public async Task CanGetProducts()
        {
            using (var context = new MockAspNetCoreGettingStartedContext())
            {
                context.Products.Add(new Product { ProductId = 1, Name = "" });                
                context.SaveChanges();
            }

            _client.DefaultRequestHeaders.Add("Tenant", "cbe7af58-306c-439a-a44f-9fee5e80ce52");

            var responseMessage = await _client.GetAsync("/api/products");

            responseMessage.EnsureSuccessStatusCode();

            var response = await responseMessage.Content.ReadAsAsync<GetProductsQuery.Response>();
            
            Assert.Equal(response.Products.Count, 1);
        }
    }
}