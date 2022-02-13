using System;
using System.Collections.Generic;

namespace PriceCalculator
{
    public enum AmountType
    {
        Percentage,
        Absolute
    }
   
    public struct Expenses
    {
        public Money Money { get; set; }
        public AmountType Type { get; set; }
        public string Description { get; set; }
    }

    public class Store
    {
        public List<Product> Products { get; } = new List<Product>()
        {
            new Product() { Name = "The Little Prince", UPC = "12345", ProductPrice = new Money(20.25f, Currency.USD) },
            new Product() { Name = "Dependency Injection", UPC = "6789", ProductPrice = new Money(100.0f, Currency.USD) }
        };
        public List<Expenses> AdditionalCosts { get; } = new List<Expenses>()
        {
            new Expenses() {Type = AmountType.Percentage, Description = "Packaging", Money = new Money(0, Currency.USD)},
            new Expenses() {Type = AmountType.Percentage, Description = "Transport", Money = new Money(3f, Currency.USD)},
        };

    }
}
//only data
