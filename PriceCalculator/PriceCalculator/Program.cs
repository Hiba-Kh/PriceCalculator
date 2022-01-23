using System;

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
            Console.WriteLine("please enter the UPC for the discount");
            UPC = Console.ReadLine();
            Console.WriteLine("please enter the UPC Discount Rate in term of %");
            UPCDiscountRate = float.Parse(Console.ReadLine());

            ProductCalculations productCalculations = new ProductCalculations(taxRate, discountRate, UPCDiscountRate, UPC);
            Store myStore = new Store();
            foreach (Product product in myStore.Products)
            {
                productCalculations.DoProductCalculations(product);
                Formatter.PrintResult(productCalculations.Result, product, discountRate);
            }
            Console.ReadLine();
        }
    }
}
