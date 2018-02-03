using AspNetCoreGettingStarted.Features.Security;
using AspNetCoreGettingStarted.IntegrationTests.Extensions;
using AspNetCoreGettingStarted.Tests.Utilities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AspNetCoreGettingStarted.IntegrationTests.Features.Security
{
    public class SignInTests
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public SignInTests()
        {
            _server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>()
                .UseConfiguration(TestHelpers.GetAppSettings()));

            _client = _server.CreateClient();
        }

        [Fact]
        public async Task CanSignIn()
        {
            
            _client.DefaultRequestHeaders.Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));

            
            var content = new StringContent(JsonConvert.SerializeObject(new SignIn.Request() {
                UserName = "quinntynebrown@gmail.com",
                Password = "P@ssw0rd",
                TenantUniqueId = new Guid("bad9a182-ede0-418d-9588-2d89cfd555bd")
            }), Encoding.UTF8,"application/json");
            
            var responseMessage = await _client.PostAsync("/api/users/signin",content);

            responseMessage.EnsureSuccessStatusCode();

            var response = await responseMessage.Content.ReadAsAsync<SignIn.Response>();

            Assert.False(string.IsNullOrEmpty(response.AccessToken));
        }
    }
}
