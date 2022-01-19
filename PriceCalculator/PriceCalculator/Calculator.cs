using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceCalculator
{
    public class Calculator
    {
        public static float TaxRate { get; set; }
        public static float CalculateTax(Price price)
        {
            float taxAmount = TaxRate * price.value;
           return (float)Math.Round(price.value + taxAmount, price.precision);
        }
    }
}
