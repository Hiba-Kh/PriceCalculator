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

            //float f = 10.123456F;
            //float fc = (float)Math.Round(f * 100f) / 100f;
            //MessageBox.Show(fc.ToString());

            return (float)Math.Round(number, precision);///100f;
        }
    }
}
