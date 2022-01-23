using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceCalculator
{
    public class Formatter
    { 
        public static void PrintResult(ProductCalculations.ProductCalculationsResult result, Product product, float discountRate)
        {
            Console.WriteLine($"Sample product: Book with name= {product.Name}, UPC= {product.UPC}, Price={product.ProductPrice.value}{product.ProductPrice.currency} ");
            if (discountRate != 0.0)
            {
                Console.WriteLine($"{result.DiscountAmount + result.UPCDiscountAmount}{product.ProductPrice.currency} amount which was deduced");
                Console.WriteLine($"price = {result.NetPrice}");
            }

            else
                Console.WriteLine($"price = {result.NetPrice}");
        }
    }
}
