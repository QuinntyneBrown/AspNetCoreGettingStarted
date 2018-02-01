using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace DotNetCoreGettingStarted.Features.Core
{
    public class EventHub: Hub
    {
        public Task Send(string message)
        {
            return Clients.All.InvokeAsync("Send", message);
        }
    }
}
