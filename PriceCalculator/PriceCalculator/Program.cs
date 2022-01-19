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
            float priceAfterTax;
            Store myStore = new Store();
            while (!isEnd)
            {
                Console.WriteLine("please enter the tax rate in term of %");
                String taxRate = Console.ReadLine();
                Calculator.TaxRate = float.Parse(taxRate) / 100;
                
                foreach (Product product in myStore.Products)
                {
                    priceAfterTax = Calculator.CalculateTax(product.ProductPrice);
                    Console.WriteLine($"Sample product: Book with name= {product.Name}, UPC= {product.UPC}, Price={product.ProductPrice.value}$ ");
                    Console.WriteLine($"Product price reported as {product.ProductPrice.value}$ before tax and {priceAfterTax}$ after {taxRate}% tax  ");    
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
