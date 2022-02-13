using System;
using System.Linq;
namespace PriceCalculator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            float discountRate, taxRate, upcDiscountRate;
            int currencyChoice;
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
            Console.WriteLine("please enter the upc Discount Rate in term of %");
            upcDiscountRate = float.Parse(Console.ReadLine());
            var productCalculator = new ProductCalculator(taxRate, discountRate, upcDiscountRate, "12345", 6, AmountType.Percentage);
            productCalculator.CalculationPrecision = 4;
            productCalculator.ResultPrecision = 2;
            productCalculator.RequestedCurrency = (Currency)currencyChoice;
            ProductCalculationsResult productCalculationsResult;
            Store myStore = new Store();
            Printer printer = new ConsolePrinter();

            foreach (Product product in myStore.Products)
            {
                productCalculationsResult = productCalculator.CalculateProductPrice(product, myStore.AdditionalCosts, true);
                printer.Print(productCalculationsResult, product);
            }
            Console.ReadLine();
        }
    }
}
