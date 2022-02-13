using System;
using Xunit;
using FluentAssertions;
using System.Collections.Generic;

namespace PriceCalculator.Tests
{
    public class ProductCalculatorShould
    {
        readonly ProductCalculator sut;
        readonly float discountRate, upcDiscountRate, taxRate;
        readonly Product product;
        public ProductCalculatorShould()
        {
            discountRate = 15;
            upcDiscountRate = 7;
            taxRate = 21;
            sut = new ProductCalculator(taxRate, discountRate, upcDiscountRate, "12345", 6, AmountType.Absolute);
            sut.ResultPrecision = 2;
            sut.CalculationPrecision = 4;
            sut.RequestedCurrency = Currency.USD;
            product = new Product() { Name = "The Little Prince", UPC = "12345", ProductPrice = new Money(20.25f, Currency.USD) };//, 4) };
        }

        [Fact]
        public void ComputeNetPriceWithTask()
        {
            var result = sut.CalculateProductPrice(product, new List<Expenses>(), false);
            result.TaxAmount.Should().Be(4.25F);
            result.DiscountAmount.Should().Be(4.45F);
            result.NetPrice.Should().Be(20.05F);
        }

        [Fact]
        public void ComputeAbsoluteAdditionalCosts()
        {
            List<Expenses>  additionalCosts = new List<Expenses>()
            {
                new Expenses() {Type = AmountType.Absolute, Description = "Transport", Money = new Money(2.2f, Currency.USD)},
            };
            var costResult = new Dictionary<string, float>
            {
                {"Transport", 2.2f}
            };
            var result = sut.CalculateProductPrice(product, additionalCosts, false);
            result.TaxAmount.Should().Be(4.25F);
            result.DiscountAmount.Should().Be(4.45F);
            result.NetPrice.Should().Be(22.25F);
            result.Expenses.Should().Equal(costResult);
        }

        [Fact]
        public void ComputePercentageAdditionalCosts()
        {
            List<Expenses> additionalCosts = new List<Expenses>()
            {
                new Expenses() {Type = AmountType.Percentage, Description = "Packaging", Money = new Money(1, Currency.USD)},
            };
            var result = sut.CalculateProductPrice(product, additionalCosts, false);
            var costResult = new Dictionary<string, float>
            {
                {"Packaging", 0.20f}
            };
            result.TaxAmount.Should().Be(4.25F);
            result.DiscountAmount.Should().Be(4.45F);
            result.NetPrice.Should().Be(20.25F);
            result.Expenses.Should().Equal(costResult);
        }
    }
}
