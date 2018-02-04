using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace AspNetCoreGettingStarted.Features.Core
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class EventHub: Hub
    {
        public EventHub() { }

        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }

        public Task Send(string message)
        {
            return Clients.All.InvokeAsync("Send", message);
        }        
    }
}
