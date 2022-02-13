using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceCalculator
{
    public interface IFormatter
    {
        Dictionary<string, string> Format(ProductCalculationsResult result, Product product);
    }
}
