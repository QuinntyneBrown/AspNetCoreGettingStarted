using AspNetCoreGettingStarted.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreGettingStarted.Data.DbInitializers
{
    public class TenantDbInitializer
    {
        public static void Seed(AspNetCoreGettingStartedContext context) {
            if (!context.Tenants.Any())
            {
                context.Tenants.Add(new Tenant()
                {
                    Name = "Default",
                    TenantId = new Guid("bad9a182-ede0-418d-9588-2d89cfd555bd")
                });
            }

            context.SaveChanges();
        }
    }
}
