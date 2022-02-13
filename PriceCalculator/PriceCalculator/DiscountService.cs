using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceCalculator
{
    public class DiscountService
    {

        private readonly float DiscountRate;
        private readonly float UPCDiscountRate;
        private readonly string UPCForDiscount;
        private readonly float CapAmount;
        private readonly AmountType CapType;
        public DiscountService(float discountRate, float upcDiscountRate, string upcForDiscount, float capAmount, AmountType capType)
        {
            DiscountRate = discountRate;
            UPCDiscountRate = upcDiscountRate;
            UPCForDiscount = upcForDiscount;
            CapAmount = capAmount;
            CapType = capType;
        }
     
        public float CalculateDiscount(Product product, bool isMultiplicative)
        {
            float discountAmount;
            discountAmount = CalculateRateAmount(product.ProductPrice, DiscountRate);
            if (product.UPC == UPCForDiscount)
            {
                discountAmount += CalculateUPCDiscountIfExists(product, isMultiplicative, discountAmount);
            }
            float capAmount = CalculateCapAmount(product.ProductPrice);
            return Math.Min(capAmount, discountAmount);
        }
        private float CalculateRateAmount(Money price, float rate)
        {
            return Calculator.DoCalculation(price, rate);
        }
        private float CalculateUPCDiscountIfExists(Product product, bool isMultiplicative, float discountAmount)
        {
            Money priceAfterFirstDiscount = new Money(product.ProductPrice.Amount - discountAmount, product.ProductPrice.Currency);//, CalculationPrecision);
            if (isMultiplicative)
                return CalculateRateAmount(priceAfterFirstDiscount, UPCDiscountRate);
            else
                return CalculateRateAmount(product.ProductPrice, UPCDiscountRate);
        }

        private float CalculateCapAmount(Money price)
        {
            if (CapType == AmountType.Absolute)
                return CapAmount;
            else
                return CalculateRateAmount(price, CapAmount);
        }

    }
}
