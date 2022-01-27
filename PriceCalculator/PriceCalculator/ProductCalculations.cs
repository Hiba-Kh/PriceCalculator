using System;
using System.Collections.Generic;
using System.Linq;

namespace PriceCalculator
{
    public class ProductCalculationsResult
    {
        public float TaxAmount { get; set; }
        public float DiscountAmount { get; set; }
        public float NetPrice { get; set; }
        public Dictionary<string, float> AdditionalCosts { get; set; } = new Dictionary<string, float>();
        public bool HasAdditionalCosts { get; set; }
       

    }
    public class ProductCalculations
    {
        public float TaxRate { get; set; }
        public float DiscountRate { get; set; }
        private Calculator Calculator { get; set; }
        public float UPCDiscountRate { get; set; }
        public string UPCForDiscount { get; set; }
        public int ResultPrecision { get; set; }
        public ProductCalculations(float taxRate, float discountRate, float upcDiscountRate, string upc)
         {
            Calculator = new Calculator();
            TaxRate = taxRate;
            DiscountRate = discountRate;
            UPCDiscountRate = upcDiscountRate;
            UPCForDiscount = upc;
         }

        private float CalculateRateAmount(Price price, float rate)
        {
            return Calculator.DoCalculation(price, rate);
        }

        public ProductCalculationsResult DoProductCalculations(Product product, List<Cost> AdditionalCosts, bool isMultiplicative, Cost cap = null, Currency requestedCurrency = Currency.USD)
        {
            if (requestedCurrency != Currency.USD)
            {
                float newPriceValue = ConvertPriceValueToRequestedCurrency(product.ProductPrice.value, requestedCurrency, product.ProductPrice.precision);
                product.ProductPrice = new Price(newPriceValue, requestedCurrency, product.ProductPrice.precision);
            }
            var result = new ProductCalculationsResult
            {
                TaxAmount = (float)CalculateRateAmount(product.ProductPrice, TaxRate),
                DiscountAmount = CalculateRateAmount(product.ProductPrice, DiscountRate)
            };
            float netPrice = product.ProductPrice.value;
            if (product.UPC == UPCForDiscount)
                result.DiscountAmount += CalculateUPCDiscountIfExists(product, isMultiplicative, result.DiscountAmount);
            float capAmount = CalculateCapAmount(cap, product.ProductPrice);
            if (result.DiscountAmount > capAmount)
                result.DiscountAmount = capAmount;
            netPrice += result.TaxAmount;
            netPrice -= result.DiscountAmount;
            float costValue;
            float netCost = 0.0f;
            foreach (Cost cost in AdditionalCosts)
            {
                costValue = DoCostsCalculations(product, cost, requestedCurrency);
                result.AdditionalCosts.Add(cost.Description, costValue);
                netCost += costValue;
            }
            if (netCost > 0.0f)
                result.HasAdditionalCosts = true;
            result.NetPrice = (float)Math.Round((netPrice + netCost), product.ProductPrice.precision);

            ConvertCalculationsResultToLowerPrecision(result, ResultPrecision);
            return result;
        }

        private void ConvertCalculationsResultToLowerPrecision(ProductCalculationsResult result, int precision)
        {
            result.NetPrice = result.NetPrice.RoundToPrecision(precision);
            result.TaxAmount = result.TaxAmount.RoundToPrecision(precision);
            result.DiscountAmount = result.DiscountAmount.RoundToPrecision(precision);
            for (int i = result.AdditionalCosts.Count - 1; i >= 0; i--)
            {
                var cost = result.AdditionalCosts.ElementAt(i);
                result.AdditionalCosts[cost.Key] = cost.Value.RoundToPrecision(precision);
            }
        }

        private float CalculateUPCDiscountIfExists(Product product, bool isMultiplicative, float discountAmount)
        {
            Price priceAfterFirstDiscount = new Price(product.ProductPrice.value - discountAmount, product.ProductPrice.currency, product.ProductPrice.precision);
            if (isMultiplicative)
                return CalculateRateAmount(priceAfterFirstDiscount, UPCDiscountRate);
            else
                return CalculateRateAmount(product.ProductPrice, UPCDiscountRate);
        }

        private float ConvertPriceValueToRequestedCurrency(float priceValue, Currency requestedCurrency, int precision)
        {
            if (requestedCurrency == Currency.GBP)
                return CurrencyConverter.ConvertPriceValueToGBP(priceValue, precision);
            else if (requestedCurrency == Currency.EUR)
                return CurrencyConverter.ConvertPriceValueToEUR(priceValue, precision);
            else if (requestedCurrency == Currency.JPY)
                return CurrencyConverter.ConvertPriceValueToJPY(priceValue, precision);
            return 0;
        }

        private float CalculateCapAmount(Cost cap, Price price)
        {
            if (cap.Type == CostType.Absolute)
                return cap.Value;
            else
                return CalculateRateAmount(price, cap.Value);
        }

        public ProductCalculationsResult DoPrecedableCalculations(Product product, List<Cost> additionalCosts)
        {
            var result = new ProductCalculationsResult();
            float netPrice = product.ProductPrice.value;
            Price price = new Price(netPrice, product.ProductPrice.currency, product.ProductPrice.precision);

            if (product.UPC == UPCForDiscount)
            {
                result.DiscountAmount = CalculateRateAmount(product.ProductPrice, UPCDiscountRate);
                result.TaxAmount = CalculateRateAmount(price, TaxRate);
                result.DiscountAmount += CalculateRateAmount(price, DiscountRate);
                netPrice += result.TaxAmount;
                netPrice -= result.DiscountAmount;
                result.NetPrice = (float)Math.Round(netPrice, product.ProductPrice.precision);
            }
            else result = DoProductCalculations(product, additionalCosts, false);
            return result;
        }
        private float DoCostsCalculations(Product product, Cost cost, Currency requestedCurrency)
        {
            if (cost.Type == CostType.Absolute)
            {

                if (requestedCurrency != Currency.USD)
                {
                    return ConvertPriceValueToRequestedCurrency(cost.Value, requestedCurrency, cost.Precision);
                }
                return cost.Value;
            }
            else if (cost.Type == CostType.Percentage)
                return Calculator.DoCalculation(product.ProductPrice, cost.Value);
            return 0.0f;
        }

      
       
       

    }
}