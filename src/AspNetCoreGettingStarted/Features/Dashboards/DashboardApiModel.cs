using AspNetCoreGettingStarted.Features.DashboardTiles;
using AspNetCoreGettingStarted.Model;
using System.Collections.Generic;
using System.Linq;

namespace AspNetCoreGettingStarted.Features.Dashboards
{
    public class DashboardApiModel
    {        
        public int DashboardId { get; set; }        
        public string Name { get; set; }
        public ICollection<DashboardTileApiModel> DashboardTiles { get; set; } = new HashSet<DashboardTileApiModel>();

        public static TModel FromDashboard<TModel>(Dashboard dashboard) where
            TModel : DashboardApiModel, new()
        {
            var model = new TModel();
            model.DashboardId = dashboard.DashboardId;
            model.Name = dashboard.Name;
            model.DashboardTiles = dashboard.DashboardTiles.Select(x => DashboardTileApiModel.FromDashboardTile(x)).ToList();
            return model;
        }

        public static DashboardApiModel FromDashboard(Dashboard dashboard)
            => FromDashboard<DashboardApiModel>(dashboard);
    }
}
