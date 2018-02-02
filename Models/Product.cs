using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreGettingStarted.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public Category Category { get; set; }
    }
}
