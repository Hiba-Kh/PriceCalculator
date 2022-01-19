using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceCalculator
{
    public enum Currency
    {
        USD
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
        public int UPC { get; set; }
        public Price ProductPrice { get; set; }
        
    }
}
