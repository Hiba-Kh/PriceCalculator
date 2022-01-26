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

    public struct Price
    {
        public float value;
        public Currency currency;
        public int precision;
        public Price(float value, Currency currency, int precision)
        {
            this.value = value;
            this.currency = currency;
            this.precision = precision;
        }
    }
  
    public class Product
    {
        public string Name { get; set; }
        public string UPC { get; set; }
        public Price ProductPrice { get; set; }   
    }
}
