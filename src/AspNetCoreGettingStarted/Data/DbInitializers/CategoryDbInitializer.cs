using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreGettingStarted.Data.DbInitializers
{
    public class CategoryDbInitializer
    {
        public static void Seed(AspNetCoreGettingStartedContext context)
        {


            context.SaveChanges();
        }
    }
}
