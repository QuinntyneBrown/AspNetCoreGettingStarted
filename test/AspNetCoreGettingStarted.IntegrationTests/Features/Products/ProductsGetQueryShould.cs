using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AspNetCoreGettingStarted.IntegrationTests.Features.Products
{
    public class ProductsGetQueryShould
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public ProductsGetQueryShould()
        {
            _server = new TestServer(new WebHostBuilder().UseStartup<IntegrationTestsStartUp>());

            _client = _server.CreateClient();
        }

        [Fact]
        public async Task ReturnProducts() {            
            _client.DefaultRequestHeaders.Add("Tenant", "cbe7af58-306c-439a-a44f-9fee5e80ce52");

            var response = await _client.GetAsync("/api/products");

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            Assert.Equal("{\"products\":[]}", responseString);
        }
    }
}
