using System;

namespace PriceCalculator
{
    public class ProductCalculations
    {
        public float TaxRate { get; set; }
        public float DiscountRate { get; set; }
        private Calculator Calculator { get; set; }
        public float UPCDiscountRate { get; set; }
        public string UPCForDiscount { get; set; }
        public ProductCalculationsResult Result { get; set; }

        public class ProductCalculationsResult
        { 
            public float TaxAmount { get; set; }
            public float UPCDiscountAmount { get; set; }
            public float DiscountAmount { get; set; }
            public float NetPrice { get; set; }

        }

        public ProductCalculations(float taxRate, float discountRate, float upcDiscountRate, string upc)
         {
            Calculator = new Calculator();
            TaxRate = taxRate;
            DiscountRate = discountRate;
            UPCDiscountRate = upcDiscountRate;
            UPCForDiscount = upc;
         }
        
        public void CalculateDiscount(Price price)
        {
            Result.DiscountAmount = Calculator.DoCalculation(price, DiscountRate);
        }
        public void CalculateTax(Price price)
        {
            Result.TaxAmount = Calculator.DoCalculation(price, TaxRate);
        }
        public void CalculateUPCDiscount(Price price)
        {
            Result.UPCDiscountAmount = Calculator.DoCalculation(price, UPCDiscountRate);
        }
        public void DoProductCalculations(Product product)
        {
            Result = new ProductCalculationsResult();
            float netPrice = product.ProductPrice.value;
            CalculateTax(product.ProductPrice);
            CalculateDiscount(product.ProductPrice);

            if (product.UPC == UPCForDiscount)
            {
                CalculateUPCDiscount(product.ProductPrice);
                netPrice -= Result.UPCDiscountAmount;
            }
            netPrice += Result.TaxAmount;
            netPrice -= Result.DiscountAmount;
            Result.NetPrice = (float)Math.Round(netPrice, product.ProductPrice.precision);
        }
    }
}