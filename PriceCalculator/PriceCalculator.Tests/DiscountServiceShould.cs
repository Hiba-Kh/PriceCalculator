using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
namespace PriceCalculator.Tests
{
    public class DiscountServiceShould
    {
        public DiscountService sut;
        readonly float discountRate, upcDiscountRate;
        readonly Product product;
        public DiscountServiceShould()
        {
            discountRate = 15;
            upcDiscountRate = 7;
            sut = new DiscountService(discountRate, upcDiscountRate, "12345", 6, AmountType.Percentage);
            product = new Product() { Name = "The Little Prince", UPC = "12345", ProductPrice = new Money(20.25f, Currency.USD) };//, 4) };
        }
        [Fact]
        public void TakeCapWhenDiscountIsBigger()
        {
            sut = new DiscountService(discountRate, upcDiscountRate, "12345", 10, AmountType.Percentage);
            float result = sut.CalculateDiscount(product, false);
            result.Should().Be(2.025F);
            result.Should().BeLessThan(4.45f);
        }

        [Fact]
        public void TakeDiscountWhenCapIsBigger()
        {
            sut = new DiscountService(discountRate, upcDiscountRate, "12345", 30, AmountType.Percentage);
            float result = sut.CalculateDiscount(product, false);
            result.Should().Be(4.455F);
            result.Should().BeLessThan(6.075f);
        }

        [Fact]
        public void PerformCalculationsMlutiplicatively()
        {
            sut = new DiscountService(discountRate, upcDiscountRate, "12345", 30, AmountType.Percentage);
            float result = sut.CalculateDiscount(product, true);
            result.Should().Be(4.2424F);
        }
    }
}
