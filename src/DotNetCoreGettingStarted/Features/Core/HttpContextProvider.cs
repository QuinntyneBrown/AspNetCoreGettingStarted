using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreGettingStarted.Features.Core
{
    public class HttpContextProvider
    {
        public HttpContext HttpContext { get; set; }
            
    }
}
