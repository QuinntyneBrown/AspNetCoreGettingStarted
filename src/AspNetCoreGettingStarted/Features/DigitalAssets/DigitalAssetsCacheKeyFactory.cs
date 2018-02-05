using System;

namespace AspNetCoreGettingStarted.Features.DigitalAssets
{
    public class DigitalAssetsCacheKeyFactory
    {
        public static string Get(Guid tenantId) => $"[DigitalAssets] Get {tenantId}";
        public static string GetById(Guid tenantId, int id) => $"[DigitalAssets] GetById {tenantId}-{id}";
        public static string GetByUniqueId(Guid tenantId, Guid digitalAssetId) => $"[DigitalAssets] GetByUniqueId {tenantId}-{digitalAssetId}";
    }
}
