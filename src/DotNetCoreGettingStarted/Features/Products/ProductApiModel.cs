using DotNetCoreGettingStarted.Models;

namespace DotNetCoreGettingStarted.Features.Products
{
    public class ProductApiModel
    {
        public int ProductId { get; set; }
        public string Name { get; set; }

        public static ProductApiModel From(Product entity) {
            var model = new ProductApiModel();
            model.ProductId = entity.ProductId;
            model.Name = entity.Name;
            return model;
        }
    }
}
