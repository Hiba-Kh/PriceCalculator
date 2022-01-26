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
            Console.WriteLine($"Cost = {product.ProductPrice.value}");
            Console.WriteLine($"Tax = {result.TaxAmount}{product.ProductPrice.currency}");
            Console.WriteLine($"Discounts = {result.DiscountAmount + result.UPCDiscountAmount}{product.ProductPrice.currency}");
            if (result.HasAdditionalCosts)
              PrintAdditionalCost(result.AdditionalCosts, product.ProductPrice.currency);
            Console.WriteLine($"Total = {result.NetPrice}");
        }
        public static void PrintResultWithoutDiscount(ProductCalculationsResult result, Product product)
        {
            Console.WriteLine($"Sample product: Book with name= {product.Name}, UPC= {product.UPC}, Price={product.ProductPrice.value}{product.ProductPrice.currency} ");
            Console.WriteLine($"price = {result.NetPrice}");
        }
       
        public static void PrintAdditionalCost(Dictionary<string, float> AdditionalCosts, Currency curreny)
        {
            foreach (KeyValuePair<string, float> kvp in AdditionalCosts)
            {
                if (kvp.Value > 0)
                    Console.WriteLine($"{kvp.Key} = {kvp.Value}{curreny}");
            }
        }
    }
}
