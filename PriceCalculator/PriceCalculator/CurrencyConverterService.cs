using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceCalculator
{
    public class CurrencyConverterService
    {
        struct CurrenciesRate
        {
            public Currency Source;
            public Currency Destination;
            public float Rate;

            public CurrenciesRate(Currency source, Currency destination, float ratio)
            {
                Source = source;
                Destination = destination;
                Rate = ratio;
            }
        }

        private readonly Dictionary<(Currency, Currency), float> currenciesRates = new List<CurrenciesRate>()
        {
            new CurrenciesRate(Currency.USD, Currency.GBP, 0.74f),
            new CurrenciesRate(Currency.USD, Currency.EUR, 0.88f),
            new CurrenciesRate(Currency.USD, Currency.JPY, 115.90f),
            new CurrenciesRate(Currency.EUR, Currency.JPY, 115.90f)
        }.ToDictionary(kvp =>( kvp.Source, kvp.Destination), kvp => kvp.Rate);

        public float ConvertCurrency(float money, Currency source, Currency destination)
        {
            float currencyRate = 0;
            if (currenciesRates.ContainsKey((source, destination)))
            {
                currencyRate = currenciesRates[(source, destination)];
            }

            return money* currencyRate;
        }
    }
}
