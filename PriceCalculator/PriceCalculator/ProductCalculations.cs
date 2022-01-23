using System;

namespace PriceCalculator
{
    public class ProductCalculationsResult
    {
        public float TaxAmount { get; set; }
        public float UPCDiscountAmount { get; set; }
        public float DiscountAmount { get; set; }
        public float NetPrice { get; set; }

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
        
        public float CalculateDiscount(Price price, float rate)
        {
             return Calculator.DoCalculation(price, rate);
        }
        public float CalculateTax(Price price)
        {
             return Calculator.DoCalculation(price, TaxRate);
        }
        public ProductCalculationsResult DoProductCalculations(Product product)
        {
            var result = new ProductCalculationsResult();
            result.TaxAmount = CalculateTax(product.ProductPrice);
            result.DiscountAmount = CalculateDiscount(product.ProductPrice, DiscountRate);
            float netPrice = product.ProductPrice.value;
            
            if (product.UPC == UPCForDiscount)
            {
                result.UPCDiscountAmount = CalculateDiscount(product.ProductPrice, UPCDiscountRate);
                netPrice -= result.UPCDiscountAmount;
            }
            netPrice += result.TaxAmount;
            netPrice -= result.DiscountAmount;
            result.NetPrice = (float)Math.Round(netPrice, product.ProductPrice.precision);
            return result;
        }
        public ProductCalculationsResult DoPrecedableCalculations(Product product)
        {
            var result = new ProductCalculationsResult();
            float netPrice = product.ProductPrice.value;
            Price price = new Price(netPrice, product.ProductPrice.currency, product.ProductPrice.precision);

            if (product.UPC == UPCForDiscount)
            {
                result.UPCDiscountAmount = CalculateDiscount(product.ProductPrice, UPCDiscountRate);
                netPrice -= result.UPCDiscountAmount;
                result.TaxAmount = CalculateTax(price);
                result.DiscountAmount = CalculateDiscount(price, DiscountRate);
                netPrice += result.TaxAmount;
                netPrice -= result.DiscountAmount;
                result.NetPrice = (float)Math.Round(netPrice, product.ProductPrice.precision);
            }
            else result = DoProductCalculations(product);
            return result;
        }
    }
}