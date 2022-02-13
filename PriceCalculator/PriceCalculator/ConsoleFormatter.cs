using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceCalculator
{
    public class ConsoleFormatter : IFormatter
    {
        public Dictionary<string,string> Format(ProductCalculationsResult calculationsResult, Product product)
        {
            var result = new Dictionary<string, string>()
            {
                {"Cost = ", $"{product.ProductPrice.Amount}"},
                {"Tax = ", $"{calculationsResult.TaxAmount}{calculationsResult.Currency}"},
                {"Discounts = ", $"{calculationsResult.DiscountAmount}{calculationsResult.Currency}"},
                
            };

            if (calculationsResult.Expenses.Count > 0)
            {
                var expensesFormat = calculationsResult.Expenses.Where(kvp => kvp.Value > 0).ToDictionary(kvp => kvp.Key, kvp => $" = {kvp.Value}{calculationsResult.Currency}");
                foreach(var expenseFormat in expensesFormat)
                {
                    result.Add(expenseFormat.Key, expenseFormat.Value);
                }
            }
            result.Add("Total = ", $"{calculationsResult.NetPrice}{calculationsResult.Currency}");
            return result;
        }
    }
}
