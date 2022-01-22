using System;

namespace PriceCalculator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            float discountAmount, taxAmount, netPrice;
            Store myStore = new Store();
            Calculator calculator = new Calculator();
            Console.WriteLine("please enter the Tax Rate in term of %");
            float taxRate = float.Parse(Console.ReadLine());
            Console.WriteLine("please enter the Discount Rate in term of %");
            float discountRate = float.Parse(Console.ReadLine());
            foreach (Product product in myStore.Products)
            {
               taxAmount = calculator.CalculateTax(product.ProductPrice, discountRate);
               discountAmount = calculator.CalculateTax(product.ProductPrice, taxRate);
               netPrice = calculator.CalculateNetPrice(product.ProductPrice, taxRate, discountRate);
               Formatter.PrintToConsole(discountRate, taxAmount, discountAmount, netPrice, product);
            }
            Console.ReadLine();
        }
    }
}
