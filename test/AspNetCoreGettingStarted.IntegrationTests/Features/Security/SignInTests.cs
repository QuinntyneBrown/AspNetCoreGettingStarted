using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace AspNetCoreGettingStarted.IntegrationTests.Features.Security
{
    public class SignInTests
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public SignInTests()
        {
            _server = new TestServer(new WebHostBuilder().UseStartup<Startup>());

            _client = _server.CreateClient();
        }
    }
}
