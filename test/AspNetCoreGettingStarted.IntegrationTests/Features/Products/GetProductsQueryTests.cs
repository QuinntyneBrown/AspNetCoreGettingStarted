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
using System.Net.Http.Headers;

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

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6InF1aW5udHluZWJyb3duQGdtYWlsLmNvbSIsInN1YiI6InF1aW5udHluZWJyb3duQGdtYWlsLmNvbSIsImp0aSI6ImQ0NGExMTlkLTRiYjgtNGFmMS05ZTgyLWNiZmYxMTUyZGY0YiIsImlhdCI6MTUxNzY5OTQ5OCwibmJmIjoxNTE3Njk5NDk4LCJleHAiOjE1MTgzMDQyOTgsImlzcyI6ImxvY2FsaG9zdCIsImF1ZCI6ImFsbCJ9._LUVa08nKLif2qFvYqKCJrI9ARZk1eVgi_D6RWb2UC0");

            var responseMessage = await _client.GetAsync("/api/products/get");

            responseMessage.EnsureSuccessStatusCode();

            var response = await responseMessage.Content.ReadAsAsync<GetProductsQuery.Response>();
            
            Assert.Equal(response.Products.Count, 0);
        }
    }
}