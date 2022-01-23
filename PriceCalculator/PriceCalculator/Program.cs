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
            Console.WriteLine("please enter the UPC Discount Rate in term of %");
            UPCDiscountRate = float.Parse(Console.ReadLine());
            Console.WriteLine("please enter the UPC for the discount");
            UPC = Console.ReadLine();

            var productCalculations = new ProductCalculations(taxRate, discountRate, UPCDiscountRate, UPC);
            ProductCalculationsResult result;
            Store myStore = new Store();
            foreach (Product product in myStore.Products)
            {
                result = productCalculations.DoPrecedableCalculations(product);
                if (result.DiscountAmount == 0)
                Formatter.PrintResultWithoutDiscount(result, product);
                else
                    Formatter.PrintResultWithDiscount(result, product);
            }
            Console.ReadLine();
        }
    }
}
