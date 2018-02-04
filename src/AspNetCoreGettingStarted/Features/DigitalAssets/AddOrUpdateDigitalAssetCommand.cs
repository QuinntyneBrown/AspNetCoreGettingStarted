using MediatR;
using AspNetCoreGettingStarted.Data;
using AspNetCoreGettingStarted.Model;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AspNetCoreGettingStarted.Features.Core;
using System.Threading;

//https://docs.microsoft.com/en-us/aspnet/core/mvc/models/file-uploads

namespace AspNetCoreGettingStarted.Features.DigitalAssets
{
    public class AddOrUpdateDigitalAssetCommand
    {
        public class Request : IRequest<Response>
        {
            public DigitalAssetApiModel DigitalAsset { get; set; }
        }

        public class Response { }

        public class AddOrUpdateDigitalAssetHandler : IRequestHandler<Request, Response>
        {
            public AddOrUpdateDigitalAssetHandler(IAspNetCoreGettingStartedContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var entity = await _context.DigitalAssets
                    .SingleOrDefaultAsync(x => x.DigitalAssetId == request.DigitalAsset.DigitalAssetId && x.IsDeleted == false);
                if (entity == null) _context.DigitalAssets.Add(entity = new DigitalAsset());
                entity.Name = request.DigitalAsset.Name;
                entity.Folder = request.DigitalAsset.Folder;
                await _context.SaveChangesAsync();

                return new Response() { };
            }

            private readonly IAspNetCoreGettingStartedContext _context;
            private readonly ICache _cache;
        }
    }
}
