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
                                        .WithUrl($"{_server.Url}/events?token=eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6InF1aW5udHluZWJyb3duQGdtYWlsLmNvbSIsInN1YiI6InF1aW5udHluZWJyb3duQGdtYWlsLmNvbSIsImp0aSI6IjNlOGU1NjAzLTQzYzktNDU1ZS04MjNlLTI5MDI2ZGIxOTc3NCIsImlhdCI6MTUxNzcwMjQ2OCwibmJmIjoxNTE3NzAyNDY4LCJleHAiOjE1MTgzMDcyNjgsImlzcyI6ImxvY2FsaG9zdCIsImF1ZCI6ImFsbCJ9.hJhH4FtkQfRmaQLRMlntkmE8cvqNxvJFyRvt9G_H-KM")
                                        .Build();

            string message = "Message";

            connection.On<string>("Send", (result) => Assert.Equal(result, message));

            await connection.StartAsync().OrTimeout();

            await connection.InvokeAsync("Send",message).OrTimeout();

        }
    }
}
