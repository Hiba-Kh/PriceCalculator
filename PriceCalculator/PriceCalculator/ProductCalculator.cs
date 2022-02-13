using System;
using System.Collections.Generic;
using System.Linq;

namespace PriceCalculator
{
    public class ProductCalculationsResult
    {
        public float TaxAmount { get; set; }
        public float DiscountAmount { get; set; }
        public float NetPrice { get; set; }
        public Currency Currency { get; set; }
        public Dictionary<string, float> Expenses { get; set; } = new Dictionary<string, float>();
    }
    public class ProductCalculator
    {
        private readonly float TaxRate;
        private readonly DiscountService DiscountService;
        private readonly CurrencyConverterService CurrencyService;
        public int ResultPrecision { get; set; }
        public int CalculationPrecision { get; set; }
        public Currency RequestedCurrency { get; set; }
        public ProductCalculator(float taxRate, float discountRate, float upcDiscountRate, string upc, float cap, AmountType type)
        {
            TaxRate = taxRate;
            DiscountService = new DiscountService(discountRate, upcDiscountRate, upc, cap, type);
            CurrencyService = new CurrencyConverterService();
        }

        private float CalculateTaxAmount(Money price, float rate)
        {
            return Calculator.DoCalculation(price, rate, CalculationPrecision);
        }

        public ProductCalculationsResult CalculateProductPrice(Product product, List<Expenses> expenses, bool isMultiplicative)
        {
            var result = new ProductCalculationsResult();
            result.TaxAmount = CalculateTaxAmount(product.ProductPrice, TaxRate);
            result.DiscountAmount = DiscountService.CalculateDiscount(product, isMultiplicative);
            result.Expenses = CalculateProductExpenses(product, expenses);
            float expensesAmount = result.Expenses.Select(e => e.Value).Sum();
            result.NetPrice = product.ProductPrice.Amount + result.TaxAmount - result.DiscountAmount + expensesAmount;
            ConvertCalculationsResultPrecision(result);
            ConvertResultToRequestedCurrency(result, product);
            return result;
        }

        private void ConvertResultToRequestedCurrency(ProductCalculationsResult result, Product product)
        {
            if (RequestedCurrency != Currency.USD)
            {
                result.Currency = RequestedCurrency;
                result.NetPrice = CurrencyService.ConvertCurrency(result.NetPrice, Currency.USD, RequestedCurrency);
                result.TaxAmount = CurrencyService.ConvertCurrency(result.TaxAmount, Currency.USD, RequestedCurrency);
                result.DiscountAmount = CurrencyService.ConvertCurrency(result.DiscountAmount, Currency.USD, RequestedCurrency);
                result.Expenses = (Dictionary<string, float>)result.Expenses.ToDictionary(kvp => kvp.Key, kvp => CurrencyService.ConvertCurrency(kvp.Value, Currency.USD, RequestedCurrency));
            }
        }

        private void ConvertCalculationsResultPrecision(ProductCalculationsResult result)
        {
            result.NetPrice = result.NetPrice.RoundToPrecision(ResultPrecision);
            result.TaxAmount = result.TaxAmount.RoundToPrecision(ResultPrecision);
            result.DiscountAmount = result.DiscountAmount.RoundToPrecision(ResultPrecision);
            result.Expenses = (Dictionary<string, float>)result.Expenses.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.RoundToPrecision(ResultPrecision));

        }

        private Dictionary<string, float> CalculateProductExpenses(Product product, List<Expenses> expenses)
        {
            var result = expenses.ToDictionary(kvp => kvp.Description, kvp => CalculateExpenseAmount(product, kvp));
            return result;

        }
        private float CalculateExpenseAmount(Product product, Expenses expenses)
        {
            if (expenses.Type == AmountType.Absolute)
            {
                return expenses.Money.Amount;
            }
            else if (expenses.Type == AmountType.Percentage)
                return Calculator.DoCalculation(product.ProductPrice, expenses.Money.Amount, CalculationPrecision);
            return 0.0f;
        }
    }
}

