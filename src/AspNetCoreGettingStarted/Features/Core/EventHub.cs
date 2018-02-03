using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Net;
using System.Threading.Tasks;

namespace AspNetCoreGettingStarted.Features.Core
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class EventHub: Hub
    {
        public Task Send(string message)
        {
            return Clients.All.InvokeAsync("Send", message);
        }
    }
}
