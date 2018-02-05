namespace AspNetCoreGettingStarted.Model
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public Tenant Tenant { get; set; }
    }
}
