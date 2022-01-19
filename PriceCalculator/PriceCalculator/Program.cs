using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceCalculator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool isEnd = false;
            string toContinue;
            Store myStore = new Store();
            String taxRate, discountRate;
            while (!isEnd)
            {
                Console.WriteLine("please enter the Tax Rate in term of %");
                taxRate = Console.ReadLine();
                Calculator.TaxRate = float.Parse(taxRate) / 100;
                Console.WriteLine("please enter the Discount Rate in term of %");
                discountRate = Console.ReadLine();
                Calculator.DiscountRate = float.Parse(discountRate) / 100;
                foreach (Product product in myStore.Products)
                {
                    Calculator.CalculateTax(product.ProductPrice);
                    Calculator.CalculateDiscount(product.ProductPrice);
                    Console.WriteLine($"Sample product: Book with name= {product.Name}, UPC= {product.UPC}, Price={product.ProductPrice.value}{product.ProductPrice.currency} ");
                    if (float.Parse(discountRate) == 0.0)
                    {
                        Console.WriteLine($"Tax = {taxRate}%, no discount");
                        Console.WriteLine($"price = {Calculator.CalculateNetPrice((product.ProductPrice))}");
                    }
                    else
                    {
                        Console.WriteLine($"Tax = {taxRate}%, discount = {discountRate}%");
                        Console.WriteLine($"price = {Calculator.CalculateNetPrice((product.ProductPrice))}");
                        Console.WriteLine($"{Calculator.DiscountAmount}{product.ProductPrice.currency} amount which was deduced");
                    }
                }
                Console.WriteLine("do you want to enter a new tax rate? y/n");
                toContinue = Console.ReadLine();
                if (toContinue.ToUpper() == "Y")
                    isEnd = false;
                else if (toContinue.ToUpper() == "N")
                    return;
            }
            Console.ReadLine();
        }
    }
}
