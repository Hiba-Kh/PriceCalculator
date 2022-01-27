using System;

namespace PriceCalculator
{
    public class Calculator
    {
        public float DoCalculation(Price price, float rate)
        {
            return (float)Math.Round((rate / 100) * price.value, price.precision);
        }
        

    }
}
