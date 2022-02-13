using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceCalculator
{
    public class ConsolePrinter : Printer
    {
        public ConsolePrinter()
        {
            Formatter = new ConsoleFormatter();
        }
        public override void OutputDate(Dictionary<string, string> outputDictionary)
        {
            foreach (KeyValuePair<string, string> kvp in outputDictionary)
                Console.WriteLine(kvp.Key + kvp.Value);
        }
    }
}
