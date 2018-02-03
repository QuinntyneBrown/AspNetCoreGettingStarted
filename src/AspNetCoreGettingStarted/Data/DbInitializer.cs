using AspNetCoreGettingStarted.Data.DbInitializers;
using AspNetCoreGettingStarted.Features.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreGettingStarted.Data
{
    public class DbInitializer
    {
        public async static Task Initialize(AspNetCoreGettingStartedContext context, IEncryptionService encryptionService)
        {           
            context.Database.EnsureCreated();

            TenantDbInitializer.Seed(context);
            UserDbInitializer.Seed(context, encryptionService);

            await Task.CompletedTask;
        }
    }
}
