using AspNetCoreGettingStarted.Features.Tiles;
using AspNetCoreGettingStarted.Model;

namespace AspNetCoreGettingStarted.Features.DashboardTiles
{
    public class DashboardTileApiModel
    {
        public int DashboardTileId { get; set; }
        public int? TenantId { get; set; }
        public string Name { get; set; }
        public string DashboardName { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Top { get; set; }
        public int Left { get; set; }
        public int? TileId { get; set; }
        public int? DashboardId { get; set; }
        public TileApiModel Tile { get; set; }
        public static TModel FromDashboardTile<TModel>(DashboardTile dashboardTile) where
            TModel : DashboardTileApiModel, new() => new TModel
            {
                DashboardTileId = dashboardTile.DashboardTileId,
                Name = dashboardTile.Name,
                Width = dashboardTile.Width,
                Height = dashboardTile.Height,
                Top = dashboardTile.Top,
                Left = dashboardTile.Left,
                Tile = TileApiModel.FromTile(dashboardTile.Tile)

            };

        public static DashboardTileApiModel FromDashboardTile(DashboardTile dashboardTile)
            => FromDashboardTile<DashboardTileApiModel>(dashboardTile);
    }
}
