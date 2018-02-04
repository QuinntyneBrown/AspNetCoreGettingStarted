using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace AspNetCoreGettingStarted.Features.Core
{
    public class EventHub: Hub
    {
        private IHttpContextAccessor _httpContextAccessor;
        public EventHub(IHttpContextAccessor httpContextAccessor)
        {
            _httpContext = httpContextAccessor.HttpContext;
        }

        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }

        public Task Send(string message)
        {
            return Clients.All.InvokeAsync("Send", message);
        }

        private readonly HttpContext _httpContext;
    }
}
