using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceCalculator
{
    public class Formatter
    { 
        public static void PrintResultWithDiscount(ProductCalculationsResult result, Product product)
        {
            Console.WriteLine($"Sample product: Book with name= {product.Name}, UPC= {product.UPC}, Price={product.ProductPrice.value}{product.ProductPrice.currency} ");
            Console.WriteLine($"price = {result.NetPrice}");
            Console.WriteLine($"{result.DiscountAmount + result.UPCDiscountAmount}{product.ProductPrice.currency} amount which was deduced");
        }
        public static void PrintResultWithoutDiscount(ProductCalculationsResult result, Product product)
        {
            Console.WriteLine($"Sample product: Book with name= {product.Name}, UPC= {product.UPC}, Price={product.ProductPrice.value}{product.ProductPrice.currency} ");
            Console.WriteLine($"price = {result.NetPrice}");
        }
    }
}
