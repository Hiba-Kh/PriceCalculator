using System;
using System.Collections.Generic;

namespace PriceCalculator
{
    public class ProductCalculationsResult
    {
        public float TaxAmount { get; set; }
        public float UPCDiscountAmount { get; set; }
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

        public ProductCalculations(float taxRate, float discountRate, float upcDiscountRate, string upc)
         {
            Calculator = new Calculator();
            TaxRate = taxRate;
            DiscountRate = discountRate;
            UPCDiscountRate = upcDiscountRate;
            UPCForDiscount = upc;
         }

        public float CalculateRateAmount(Price price, float rate)
        {
            return Calculator.DoCalculation(price, rate);
        }

        public ProductCalculationsResult DoProductCalculations(Product product, List<Cost> AdditionalCosts)
        {
            var result = new ProductCalculationsResult();
            result.TaxAmount = CalculateRateAmount(product.ProductPrice, TaxRate);
            result.DiscountAmount = CalculateRateAmount(product.ProductPrice, DiscountRate);
            float netPrice = product.ProductPrice.value;

            if (product.UPC == UPCForDiscount)
            {
                result.UPCDiscountAmount = CalculateRateAmount(product.ProductPrice, UPCDiscountRate);
                netPrice -= result.UPCDiscountAmount;
            }
            netPrice += result.TaxAmount;
            netPrice -= result.DiscountAmount;
            float costValue;
            float netCost = 0.0f;
            foreach (Cost cost in AdditionalCosts)
            {
                costValue = DoCostsCalculations(product, cost);
                result.AdditionalCosts.Add(cost.Description, costValue);
                netCost += costValue;
            }
            if (netCost > 0.0f)
                result.HasAdditionalCosts = true;
            result.NetPrice = (float)Math.Round((netPrice + netCost), product.ProductPrice.precision);
            return result;
        }

        public ProductCalculationsResult DoPrecedableCalculations(Product product, List<Cost> additionalCosts)
        {
            var result = new ProductCalculationsResult();
            float netPrice = product.ProductPrice.value;
            Price price = new Price(netPrice, product.ProductPrice.currency, product.ProductPrice.precision);

            if (product.UPC == UPCForDiscount)
            {
                result.UPCDiscountAmount = CalculateRateAmount(product.ProductPrice, UPCDiscountRate);
                netPrice -= result.UPCDiscountAmount;
                result.TaxAmount = CalculateRateAmount(price, TaxRate);
                result.DiscountAmount = CalculateRateAmount(price, DiscountRate);
                netPrice += result.TaxAmount;
                netPrice -= result.DiscountAmount;
                result.NetPrice = (float)Math.Round(netPrice, product.ProductPrice.precision);
            }
            else result = DoProductCalculations(product, additionalCosts);
            return result;
        }
        public float DoCostsCalculations(Product product, Cost cost)
        {
            if (cost.Type == CostType.Absolute)
                return cost.Value;
            else if (cost.Type == CostType.Percentage)
                return Calculator.DoCalculation(product.ProductPrice, cost.Value);
            return 0.0f;
        }
       

    }
}