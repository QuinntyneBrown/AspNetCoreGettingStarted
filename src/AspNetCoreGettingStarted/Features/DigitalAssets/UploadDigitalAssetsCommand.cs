using AspNetCoreGettingStarted.Data;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading;
using System.Threading.Tasks;
using AspNetCoreGettingStarted.Features.DigitalAssets.UploadHandlers;
using System.IO;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.AspNetCore.Http.Features;
using AspNetCoreGettingStarted.Features.Core;

namespace AspNetCoreGettingStarted.Features.DigitalAssets
{
    public class UploadDigitalAssetsCommand
    {
        public class Request: BaseAuthenticatedRequest, IRequest<Response> { }

        public class Response {

        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private static readonly FormOptions _defaultFormOptions = new FormOptions();
            private readonly IHttpContextAccessor _httpContextAccessor;
            public Handler(IAspNetCoreGettingStartedContext context, IHttpContextAccessor httpContextAccessor)
            {
                _httpContextAccessor = httpContextAccessor;
                _context = context;
            }
            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                if (!MultipartRequestHelper.IsMultipartContentType(_httpContext.Request.ContentType))
                {
                    throw new Exception($"Expected a multipart request, but got {_httpContext.Request.ContentType}");
                }

                var boundary = MultipartRequestHelper.GetBoundary(
                    MediaTypeHeaderValue.Parse(_httpContext.Request.ContentType),
                    _defaultFormOptions.MultipartBoundaryLengthLimit);
                var reader = new MultipartReader(boundary, _httpContext.Request.Body);

                var section = await reader.ReadNextSectionAsync();
                while (section != null)
                {
                    ContentDispositionHeaderValue contentDisposition;
                    var hasContentDispositionHeader = ContentDispositionHeaderValue.TryParse(section.ContentDisposition, out contentDisposition);

                    if (hasContentDispositionHeader)
                    {
                        if (MultipartRequestHelper.HasFileContentDisposition(contentDisposition))
                        {
                            using (var targetStream = new MemoryStream())
                            {
                                await section.Body.CopyToAsync(targetStream);
                                var bytes = StreamHelper.ReadToEnd(targetStream);
                                //TODO: Save Bytes, filename, mimetype, etc..
                            }
                        }
                    }

                    section = await reader.ReadNextSectionAsync();
                }
                
                return new Response();
            }

            private HttpContext _httpContext {  get { return _httpContextAccessor.HttpContext; } }

            private IAspNetCoreGettingStartedContext _context;
        }
    }
}
