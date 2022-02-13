using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceCalculator
{
    public static class FloatExtensions
    {
        public static float RoundToPrecision(this float number, int precision)
        {
            return (float)Math.Round(number, precision);
        }
    }
}
