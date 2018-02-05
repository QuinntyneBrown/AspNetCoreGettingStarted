using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNetCoreGettingStarted.Model
{
    public class DashboardTile
    {
        public int DashboardTileId { get; set; }

        public string Name { get; set; }        

        public int Width { get; set; } = 1;

        public int Height { get; set; } = 1;

        public int Top { get; set; } = 1;

        public int Left { get; set; } = 1;

        public DateTime CreatedOn { get; set; }
        
		public DateTime LastModifiedOn { get; set; }
        
		public string CreatedBy { get; set; }
        
		public string LastModifiedBy { get; set; }
        
		public bool IsDeleted { get; set; }

        public virtual Tenant Tenant { get; set; }

        public virtual Dashboard Dashboard { get; set; }

        public virtual Tile Tile { get; set; }
    }
}
