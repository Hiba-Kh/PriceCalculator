using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceCalculator
{
    public class Formatter
    {
        public static void PrintToConsole(float discountRate, float taxAmount, float discountAmount, float netPrice, Product product)
        {
            Console.WriteLine($"Sample product: Book with name= {product.Name}, UPC= {product.UPC}, Price={product.ProductPrice.value}{product.ProductPrice.currency} ");
            if (discountRate != 0.0)
            {
                Console.WriteLine($"{discountAmount}{product.ProductPrice.currency} amount which was deduced");
                Console.WriteLine($"price = {netPrice}");
            }

            else
                Console.WriteLine($"price = {netPrice}");
        }
    }
}
