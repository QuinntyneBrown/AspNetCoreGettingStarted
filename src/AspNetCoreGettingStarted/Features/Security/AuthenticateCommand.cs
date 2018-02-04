using AspNetCoreGettingStarted.Data;
using AspNetCoreGettingStarted.Features.Core;
using AspNetCoreGettingStarted.Model;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

//https://stackoverflow.com/questions/45805411/asp-net-core-2-0-authentication-middleware

namespace AspNetCoreGettingStarted.Features.Security
{
    public class AuthenticateCommand
    {
        public class Request: IRequest<Response> {
            public string Username { get; set; }
            public string Password { get; set; }
            public Guid TenantId { get; set; }
        }

        public class Response {
            public bool IsAuthenticated { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            public Handler(IEncryptionService encryptionService, IAspNetCoreGettingStartedContext context)
            {
                _encryptionService = encryptionService;
                _context = context;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var users = _context.Users.Include(x => x.Tenant).ToList();
                var user = await _context.Users
                    .Include(x => x.Tenant)
                    .SingleOrDefaultAsync(x => x.UserName.ToLower() == request.Username.ToLower() && x.Tenant.TenantId == request.TenantId);
                
                return new Response()
                {
                    IsAuthenticated = _encryptionService.TransformPassword(request.Password) == user.Password
                };                
            }
            
            protected readonly IAspNetCoreGettingStartedContext _context;
            protected readonly IEncryptionService _encryptionService;
        }
    }
}
