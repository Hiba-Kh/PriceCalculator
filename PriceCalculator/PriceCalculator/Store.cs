using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceCalculator
{
    public enum CostType
    {
        Percentage,
        Absolute
    }
    public class Cost
    {
        public string Description { get; set; }
        public CostType Type { get; set; }
        public float Value { get; set; }
    }
    public class Store
    {
        public List<Product> Products { get; } = new List<Product>()
        {
            new Product() { Name = "The Little Prince", UPC = "12345", ProductPrice = new Price(20.25f, Currency.USD, 2 )},
            new Product() { Name = "Dependency Injection", UPC = "6789", ProductPrice = new Price(100.0f, Currency.USD, 2 )}
        };
        public List<Cost> AdditionalCosts { get; } = new List<Cost>()
        {
            new Cost() {Type = CostType.Percentage, Description = "Packaging", Value = 1},
            new Cost() {Type = CostType.Absolute, Description = "Transport", Value = 0 }//2.2f},
        };
          
    }
}
//only data
