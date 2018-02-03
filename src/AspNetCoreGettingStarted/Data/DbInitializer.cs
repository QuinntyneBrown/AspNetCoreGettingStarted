using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreGettingStarted.Data
{
    public class DbInitializer
    {
        public async static Task Initialize(AspNetCoreGettingStartedContext context)
        {

            throw new Exception();

            context.Database.EnsureCreated();

            if (context.Users.Any())
            {
                return;
            }

            await context.SaveChangesAsync();
        }
    }
}
