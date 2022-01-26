using System;

namespace PriceCalculator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            float discountRate, taxRate;
            int currencyChoice;
            // TODO: validate input
            Console.WriteLine("please select in what currency you want the price:");
            Console.WriteLine("1- USD");
            Console.WriteLine("2- GBP");
            Console.WriteLine("3- JPY");
            Console.WriteLine("4- EUR");
            currencyChoice = int.Parse(Console.ReadLine());
            Console.WriteLine("please enter the Tax Rate in term of %");
            taxRate = float.Parse(Console.ReadLine());
            Console.WriteLine("please enter the Discount Rate in term of %");
            discountRate = float.Parse(Console.ReadLine());
            
            var productCalculations = new ProductCalculations(taxRate, discountRate, 0, "");
            ProductCalculationsResult productCalculationsResult;
            Store myStore = new Store();

            foreach (Product product in myStore.Products)
            {
                productCalculationsResult = productCalculations.DoProductCalculations(product, myStore.AdditionalCosts, false, myStore.Cap, (Currency)currencyChoice);
                if (productCalculationsResult.DiscountAmount == 0)
                    Formatter.PrintResultWithoutDiscount(productCalculationsResult, product);
                else
                    Formatter.PrintResultWithDiscount(productCalculationsResult, product);
            }
            Console.ReadLine();
        }
    }
}
