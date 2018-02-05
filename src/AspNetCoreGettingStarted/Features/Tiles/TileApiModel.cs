using AspNetCoreGettingStarted.Model;

namespace AspNetCoreGettingStarted.Features.Tiles
{
    public class TileApiModel
    {        
        public int TileId { get; set; }
        public int? TenantId { get; set; }
        public string Name { get; set; }

        public static TModel FromTile<TModel>(Tile tile) where
            TModel : TileApiModel, new()
        {
            var model = new TModel();
            model.TileId = tile.TileId;
            model.Name = tile.Name;
            return model;
        }

        public static TileApiModel FromTile(Tile tile)
            => FromTile<TileApiModel>(tile);

    }
}
