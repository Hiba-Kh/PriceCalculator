using System;
using System.Collections.Generic;

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
        public int Precision { get; set; } = 4;
    }
    public class Store
    {
        public List<Product> Products { get; } = new List<Product>()
        {
            new Product() { Name = "The Little Prince", UPC = "12345", ProductPrice = new Price(20.25f, Currency.USD, 4 )},
            new Product() { Name = "Dependency Injection", UPC = "6789", ProductPrice = new Price(100.0f, Currency.USD, 4 )}
        };
        public List<Cost> AdditionalCosts { get; } = new List<Cost>()
        {
            new Cost() {Type = CostType.Percentage, Description = "Packaging", Value = 1},
            new Cost() {Type = CostType.Absolute, Description = "Transport", Value = 2.2f},
        };

        public Cost Cap { get; set; } = new Cost() { Type = CostType.Percentage, Value = 30};
    }
}
//only data
