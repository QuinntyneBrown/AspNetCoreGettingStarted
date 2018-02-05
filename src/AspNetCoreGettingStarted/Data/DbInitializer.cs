using AspNetCoreGettingStarted.Data.DbInitializers;
using AspNetCoreGettingStarted.Features.Security;
using System.Threading.Tasks;

namespace AspNetCoreGettingStarted.Data
{
    public class DbInitializer
    {
        public async static Task Initialize(AspNetCoreGettingStartedContext context, IEncryptionService encryptionService)
        {           
            context.Database.EnsureCreated();

            TenantDbInitializer.Seed(context);
            CategoryDbInitializer.Seed(context);
            UserDbInitializer.Seed(context, encryptionService);

            await Task.CompletedTask;
        }
    }
}
