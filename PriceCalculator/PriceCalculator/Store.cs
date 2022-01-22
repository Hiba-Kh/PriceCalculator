using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceCalculator
{
    public class Store
    {
        public List<Product> Products { get; set; } = new List<Product>()
        {
            new Product() { Name = "The Little Prince", UPC = 12345, ProductPrice = new Price(20.25f, Currency.USD, 2 )},
            new Product() { Name = "Dependency Injection", UPC = 6789, ProductPrice = new Price(100.0f, Currency.USD, 2 )}
        };
    }
}
//only data