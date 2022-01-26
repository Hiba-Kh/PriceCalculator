﻿using System;

namespace PriceCalculator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            float discountRate, taxRate, UPCDiscountRate;
            string UPC;

            Console.WriteLine("please enter the Tax Rate in term of %");
            taxRate = float.Parse(Console.ReadLine());
            Console.WriteLine("please enter the Discount Rate in term of %");
            discountRate = float.Parse(Console.ReadLine());
            Console.WriteLine("please enter the UPC Discount Rate in term of %");
            UPCDiscountRate = float.Parse(Console.ReadLine());
            Console.WriteLine("please enter the UPC for the discount");
            UPC = Console.ReadLine();

            var productCalculations = new ProductCalculations(taxRate, discountRate, UPCDiscountRate, UPC);
            ProductCalculationsResult productCalculationsResult;
            Store myStore = new Store();
 
            foreach (Product product in myStore.Products)
            {
                productCalculationsResult = productCalculations.DoProductCalculations(product, myStore.AdditionalCosts);
                if (productCalculationsResult.DiscountAmount == 0)
                    Formatter.PrintResultWithoutDiscount(productCalculationsResult, product);
                else
                    Formatter.PrintResultWithDiscount(productCalculationsResult, product);
            }

            Console.ReadLine();
        }
    }
}
