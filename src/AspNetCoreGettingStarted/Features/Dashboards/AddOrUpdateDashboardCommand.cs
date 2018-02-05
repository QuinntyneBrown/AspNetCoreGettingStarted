using MediatR;
using AspNetCoreGettingStarted.Data;
using AspNetCoreGettingStarted.Model;
using AspNetCoreGettingStarted.Features.Core;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace AspNetCoreGettingStarted.Features.Dashboards
{
    public class AddOrUpdateDashboardCommand
    {
        public class Request : BaseRequest, IRequest<Response>
        {
            public DashboardApiModel Dashboard { get; set; }            
        }

        public class Response { }

        public class Handler : IRequestHandler<Request, Response>
        {
            public Handler(IAspNetCoreGettingStartedContext context)
            {
                _context = context;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var entity = await _context.Dashboards
                    .Include(x => x.Tenant)
                    .SingleOrDefaultAsync(x => x.DashboardId == request.Dashboard.DashboardId && x.Tenant.TenantId == request.TenantId);
                
                if (entity == null) {
                    var tenant = await _context.Tenants.SingleAsync(x => x.TenantId == request.TenantId);
                    _context.Dashboards.Add(entity = new Dashboard() { Tenant = tenant });
                }

                entity.Name = request.Dashboard.Name;
                
                await _context.SaveChangesAsync();

                return new Response();
            }
            
            private readonly IAspNetCoreGettingStartedContext _context;
        }
    }
}
