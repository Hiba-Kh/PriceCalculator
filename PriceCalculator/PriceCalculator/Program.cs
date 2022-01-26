using System;

namespace PriceCalculator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            float discountRate, taxRate, UPCDiscountRate;
            string UPC;
            // TODO: validate input
            Console.WriteLine("please enter the Tax Rate in term of %");
            taxRate = float.Parse(Console.ReadLine());
            Console.WriteLine("please enter the Discount Rate in term of %");
            discountRate = float.Parse(Console.ReadLine());
            Console.WriteLine("please enter the UPC Discount Rate in term of %");
            UPCDiscountRate = float.Parse(Console.ReadLine());
            Console.WriteLine("please enter the UPC for the discount");
            UPC = Console.ReadLine();
            Console.WriteLine("please select the method of how to apply the discount (1/2):");
            Console.WriteLine("1- Additive");
            Console.WriteLine("2- Multiplicative");
            int methodChoice = int.Parse(Console.ReadLine());
            bool isMultiplicative;
            if (methodChoice == 1)
                isMultiplicative = false;
            else if (methodChoice == 2)
                isMultiplicative = true;
            
            else return;
            var productCalculations = new ProductCalculations(taxRate, discountRate, UPCDiscountRate, UPC);
            ProductCalculationsResult productCalculationsResult;
            Store myStore = new Store();
 
            foreach (Product product in myStore.Products)
            {
                productCalculationsResult = productCalculations.DoProductCalculations(product, myStore.AdditionalCosts, isMultiplicative);
                if (productCalculationsResult.DiscountAmount == 0)
                    Formatter.PrintResultWithoutDiscount(productCalculationsResult, product);
                else
                    Formatter.PrintResultWithDiscount(productCalculationsResult, product);
            }

            Console.ReadLine();
        }
    }
}
