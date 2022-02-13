using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceCalculator
{
    public abstract class Printer
    {
        public IFormatter Formatter { get; set; }
        public abstract void OutputDate(Dictionary<string, string> outputDictionary);

        public void Print(ProductCalculationsResult calculationsResult, Product product)
        {
            var output = Formatter.Format(calculationsResult, product);
            OutputDate(output);
        }
    }
}
