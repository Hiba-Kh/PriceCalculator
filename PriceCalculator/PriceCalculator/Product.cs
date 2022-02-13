using System;
namespace PriceCalculator
{
    public enum Currency
    {
        USD = 1,
        GBP,
        JPY,
        EUR
    }

    public struct Money
    {
        public float Amount;
        public Currency Currency;
        public Money(float amount, Currency currency)
        {
            Amount = amount;
            Currency = currency;
        }
    }
  
    public class Product
    {
        public string Name { get; set; }
        public string UPC { get; set; }
        public Money ProductPrice { get; set; }   
    }
}
