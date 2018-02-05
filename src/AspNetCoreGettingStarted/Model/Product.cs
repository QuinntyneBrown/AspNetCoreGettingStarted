﻿namespace AspNetCoreGettingStarted.Model
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public int? CategoryId { get; set; }
        public Category Category { get; set; }
        public Tenant Tenant { get; set; }
    }
}
