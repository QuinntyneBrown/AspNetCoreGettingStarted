﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace AspNetCoreGettingStarted.Features.Core
{
    public class EventHub: Hub
    {
        public EventHub()
        {

        }

        public Task Send(string message)
        {
            return Clients.All.InvokeAsync("Send", message);
        }
        
    }
}
