using System;
using Xunit;
using FluentAssertions;
using System.Collections.Generic;

namespace PriceCalculator.Tests
{
    public class ProductCalculationsShould
    {
        readonly ProductCalculations sut;
        readonly float discountRate, upcDiscountRate, taxRate;
        readonly Product product;
        Cost cap;
        public ProductCalculationsShould()
        {
            discountRate = 15;
            upcDiscountRate = 7;
            taxRate = 21;
            sut = new ProductCalculations(taxRate, discountRate, upcDiscountRate, "12345");
            sut.ResultPrecision = 2;
            product = new Product() { Name = "The Little Prince", UPC = "12345", ProductPrice = new Price(20.25f, Currency.USD, 4) };
            cap = new Cost() { Type = CostType.Percentage, Value = 30 };
        }

        [Fact]
        public void ComputeNetPriceWithTaskAndDiscount()
        {
            var result = sut.DoProductCalculations(product, new List<Cost>(), false, cap); 
            result.TaxAmount.Should().Be(4.25F);
            result.DiscountAmount.Should().Be(4.45F);
            result.NetPrice.Should().Be(20.05F);
        }
        
        [Fact]
        public void TakeCapWhenDiscountIsBigger()
        {
            cap.Value = 10;
            var result = sut.DoProductCalculations(product, new List<Cost>(), false, cap);
            result.TaxAmount.Should().Be(4.25F);
            result.DiscountAmount.Should().Be(2.03F);
            result.DiscountAmount.Should().BeLessThan(4.45f);
            result.NetPrice.Should().Be(22.48F);
        }

        [Fact]
        public void TakeDiscountWhenCapIsBigger()
        {
            cap.Value = 30;
            var result = sut.DoProductCalculations(product, new List<Cost>(), false, cap);
            result.TaxAmount.Should().Be(4.25F);
            result.DiscountAmount.Should().Be(4.45F);
            result.DiscountAmount.Should().BeLessThan(6.075f);
            result.NetPrice.Should().Be(20.05F);
        }

        [Fact]
        public void PerformCalculationsMlutiplicatively()
        {
            var result = sut.DoProductCalculations(product, new List<Cost> () , true, cap);
            result.TaxAmount.Should().Be(4.25F);
            result.DiscountAmount.Should().Be(4.24F);
            result.NetPrice.Should().Be(20.26F);
        }

        [Fact]
        public void ComputeAbsoluteAdditionalCosts()
        {
            List<Cost>  additionalCosts = new List<Cost>()
            {
                new Cost() {Type = CostType.Absolute, Description = "Transport", Value = 2.2f},
            };
            var costResult = new Dictionary<string, float>
            {
                {"Transport", 2.2f}
            };
            var result = sut.DoProductCalculations(product, additionalCosts, false, cap);
            result.TaxAmount.Should().Be(4.25F);
            result.DiscountAmount.Should().Be(4.45F);
            result.NetPrice.Should().Be(22.25F);
            result.AdditionalCosts.Should().Equal(costResult);
        }

        [Fact]
        public void ComputePercentageAdditionalCosts()
        {
            List<Cost> additionalCosts = new List<Cost>()
            {
                new Cost() {Type = CostType.Percentage, Description = "Packaging", Value = 1},
            };
            var result = sut.DoProductCalculations(product, additionalCosts, false, cap);
            var costResult = new Dictionary<string, float>
            {
                {"Packaging", 0.20f}
            };
            result.TaxAmount.Should().Be(4.25F);
            result.DiscountAmount.Should().Be(4.45F);
            result.NetPrice.Should().Be(20.25F);
            result.AdditionalCosts.Should().Equal(costResult);
        }
    }
}
