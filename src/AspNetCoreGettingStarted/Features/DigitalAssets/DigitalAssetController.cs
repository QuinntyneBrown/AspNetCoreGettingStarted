using AspNetCoreGettingStarted.Features.DigitalAssets.UploadHandlers;
using MediatR;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Http;

namespace AspNetCoreGettingStarted.Features.DigitalAssets
{
    [Authorize]
    [Route("api/digitalassets")]
    public class DigitalAssetController : Controller
    {
        private readonly IMediator _mediator;
        public DigitalAssetController(IMediator mediator) {
            _mediator = mediator;
        }

        [Route("add")]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody]AddOrUpdateDigitalAssetCommand.Request request)
            => Ok(await _mediator.Send(request));

        [Route("update")]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody]AddOrUpdateDigitalAssetCommand.Request request)
            => Ok(await _mediator.Send(request));
        
        [Route("get")]
        [HttpGet]
        public async Task<IActionResult> Get()
            => Ok(await _mediator.Send(new GetDigitalAssetsQuery.Request()));

        [Route("getMostRecent")]
        [HttpGet]
        public async Task<IActionResult> GetMostRecent()
            => Ok(await _mediator.Send(new GetMostRecentDigitalAssetsQuery.Request()));

        [Route("getById")]
        [HttpGet]
        public async Task<IActionResult> GetById([FromQuery]GetDigitalAssetByIdQuery.Request request)
            => Ok(await _mediator.Send(request));

        [Route("remove")]
        [HttpDelete]
        public async Task<IActionResult> Remove([FromQuery]RemoveDigitalAssetCommand.Request request)
            => Ok(await _mediator.Send(request));

        [Route("serve")]
        [HttpGet]
        public async Task<HttpResponseMessage> Serve([FromQuery]GetDigitalAssetByUniqueIdQuery.Request request)
        {            
            var response = await _mediator.Send(request);
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new ByteArrayContent(response.DigitalAsset.Bytes);
            result.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(response.DigitalAsset.ContentType);
            return result;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload() {

            if (!MultipartRequestHelper.IsMultipartContentType(Request.ContentType))
            {
                return BadRequest($"Expected a multipart request, but got {Request.ContentType}");
            }

            var boundary = MultipartRequestHelper.GetBoundary(
                MediaTypeHeaderValue.Parse(Request.ContentType),
                _defaultFormOptions.MultipartBoundaryLengthLimit);
            var reader = new MultipartReader(boundary, HttpContext.Request.Body);

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

            return Ok();
        }

        private static readonly FormOptions _defaultFormOptions = new FormOptions();
    }
}