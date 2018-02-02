using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace AspNetCoreGettingStarted.Features.Core
{
    public static class HttpRequestExtensions
    {        
        public static string GetHeaderValue(this HttpRequest request, string name)
        {
            StringValues values;
            var found = request.Headers.TryGetValue(name, out values);
            if (found)
            {
                return values.FirstOrDefault();
            }

            return null;
        }
    }
}
