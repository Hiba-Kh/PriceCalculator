using System;

namespace PriceCalculator
{
    public class CalculationConfigs
    {

    }
    public class Calculator
    {
     
        public  float CalculateTax(Price price, float taxRate)
        {
            return (float)Math.Round((taxRate/100) * price.value, price.precision);
        }
        public  float CalculateDiscount(Price price, float discountRate)
        {
            return (float)Math.Round((discountRate/100) * price.value, price.precision); 
        }
        public float CalculateNetPrice(Price price, float taxRate, float discountRate)
        {
            float netPrice = price.value;
            float taxAmount = CalculateTax(price, taxRate);
            float discountAmount = CalculateDiscount(price, discountRate);
            if (taxAmount > 0)
                netPrice += taxAmount;
            if (discountAmount > 0)
                netPrice -= discountAmount;
            return (float)Math.Round(netPrice, price.precision);
        }

    }
}
