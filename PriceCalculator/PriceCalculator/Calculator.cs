﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceCalculator
{
    public class Calculator
    {
        public static float TaxRate { get; set; }
        public static float DiscountRate { get; set; }
        public static float TaxAmount { get; set; }
        public static float DiscountAmount { get; set; }
        public static float CalculateTax(Price price)
        {
            TaxAmount = TaxRate * price.value;
            return TaxAmount;
        }
        public static float CalculateDiscount(Price price)
        {
            DiscountAmount = DiscountRate * price.value;
            return DiscountAmount;
        }
        public static float CalculateNetPrice(Price price)
        {
            float netPrice = price.value;
            if (TaxAmount > 0)
                netPrice += TaxAmount;
            if (DiscountAmount > 0)
                netPrice -= DiscountAmount;
            return (float)Math.Round(netPrice, price.precision); ;
        }
    }
}