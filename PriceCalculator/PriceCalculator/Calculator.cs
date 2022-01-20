using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceCalculator
{
    public class Calculator
    {
        public  float TaxRate { get; set; }
        public float DiscountRate { get; set; }
        private float _discountAmount;
        private float _taxAmount;
        public  void CalculateTax(Price price)
        {
            _taxAmount = (float)Math.Round(TaxRate * price.value, price.precision);
        }
        public  void CalculateDiscount(Price price)
        {
            _discountAmount = (float)Math.Round(DiscountRate * price.value, price.precision); 
        }
        public  float CalculateNetPrice(Price price)
        {
            float netPrice = price.value;
            if (_taxAmount > 0)
                netPrice += _taxAmount;
            if (_discountAmount > 0)
                netPrice -= _discountAmount;
            return (float)Math.Round(netPrice, price.precision); 
        }
        public void DoCalculations(float taxRate, float discountRate)
        {
            Store myStore = new Store();
            TaxRate = taxRate/100;
            DiscountRate = discountRate/100;
            float netPrice;
            foreach (Product product in myStore.Products)
            {
                CalculateTax(product.ProductPrice);
                CalculateDiscount(product.ProductPrice);
                netPrice = CalculateNetPrice((product.ProductPrice));
                // TODO check with Karam how to send these info, in separate params or combine it in an object  
                Formatter.PrintToConsole(taxRate, discountRate, _taxAmount, _discountAmount, netPrice, product);
            }
        }
    }
}
