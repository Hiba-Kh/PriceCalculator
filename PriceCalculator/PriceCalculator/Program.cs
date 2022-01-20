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
            Calculator calculator = new Calculator();
            String taxRate, discountRate;
            while (!isEnd)
            {
                Console.WriteLine("please enter the Tax Rate in term of %");
                taxRate = Console.ReadLine();
                Console.WriteLine("please enter the Discount Rate in term of %");
                discountRate = Console.ReadLine();
                calculator.DoCalculations(float.Parse(taxRate), float.Parse(discountRate)); 
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
