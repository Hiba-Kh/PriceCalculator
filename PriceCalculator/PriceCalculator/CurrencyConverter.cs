using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceCalculator
{
    public class CurrencyConverter
    {
        private const float USDToGBPRate = 0.8770370f; //0.73957104f; //;
        private const float USDToEURRate = 0.88572619f;
        private const float USDToJPYRate = 114.30216f;
        public static float ConvertPriceValueToGBP(float priceValue, int precision)
        {
            return (float)Math.Round(priceValue * USDToGBPRate, precision);
        }
        public static float ConvertPriceValueToJPY(float priceValue, int precision)
        {
            return (float)Math.Round(priceValue * USDToJPYRate, precision);
        }
        public static float ConvertPriceValueToEUR(float priceValue, int precision)
        {
            return (float)Math.Round(priceValue * USDToEURRate, precision);
        }


        
    }
}

