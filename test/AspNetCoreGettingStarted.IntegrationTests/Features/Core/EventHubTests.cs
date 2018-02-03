using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Xunit;
using AspNetCoreGettingStarted.IntegrationTests.Extensions;

namespace AspNetCoreGettingStarted.IntegrationTests.Features.Core
{
    public class EventHubTests
    {
        private readonly ServerFixture<Startup> _server;
        private readonly HttpClient _client;

        public EventHubTests()
        {
            _server = new ServerFixture<Startup>();

            _client = new HttpClient();
        }

        [Fact]
        public async Task CanSend()
        {            
            var connection = new HubConnectionBuilder()
                                        .WithUrl($"{_server.Url}/events")
                                        .Build();

            string message = "Message";

            connection.On<string>("Send", (result) => Assert.Equal(result, message));

            await connection.StartAsync().OrTimeout();

            await connection.InvokeAsync("Send",message).OrTimeout();

        }
    }
}
