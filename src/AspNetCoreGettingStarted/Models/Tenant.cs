using System;
namespace AspNetCoreGettingStarted.Models
{
    public class Tenant
    {
        public Guid TenantId { get; set; }
        public string Name { get; set; }
    }
}
