using System;
namespace AspNetCoreGettingStarted.Model
{
    public class Tenant
    {
        public Guid TenantId { get; set; }
        public string Name { get; set; }
    }
}
