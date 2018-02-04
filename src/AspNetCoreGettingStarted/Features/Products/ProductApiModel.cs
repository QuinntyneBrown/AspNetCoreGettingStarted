using AspNetCoreGettingStarted.Model;

namespace AspNetCoreGettingStarted.Features.Products
{
    public class ProductApiModel
    {        
        public int? ProductId { get; set; }
        public string Name { get; set; }
        
        public static TModel FromProduct<TModel>(Product product) where
            TModel : ProductApiModel, new()
        {
            if (product == null) return null;

            var model = new TModel();
            model.ProductId = product.ProductId;
            model.Name = product.Name;
            return model;
        }

        public static ProductApiModel FromProduct(Product product)
            => FromProduct<ProductApiModel>(product);
    }
}
