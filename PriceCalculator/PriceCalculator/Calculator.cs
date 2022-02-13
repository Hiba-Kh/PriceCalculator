using System;

namespace PriceCalculator
{
    public static class Calculator
    {
        public static float DoCalculation(Money price, float rate, int precision = 4)
        {
            return (float)Math.Round((rate / 100) * price.Amount, precision);
        }
    }
}
