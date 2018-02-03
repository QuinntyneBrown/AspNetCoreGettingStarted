using AspNetCoreGettingStarted.Features.Security;
using AspNetCoreGettingStarted.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreGettingStarted.Data.DbInitializers
{
    public class UserDbInitializer
    {
        public static void Seed(AspNetCoreGettingStartedContext context, IEncryptionService encryptionService)
        {
            if (!context.Users.Any()) {
                Tenant tenant = context.Tenants
                    .Single(x => x.TenantId == new Guid("bad9a182-ede0-418d-9588-2d89cfd555bd"));

                var user = new User()
                {
                    UserName = "quinntynebrown@gmail.com",
                    Tenant = tenant,
                    Password = encryptionService.TransformPassword("P@ssw0rd")
                };
                
                context.Users.Add(user);
            }
            
            context.SaveChanges();
        }
    }
}
