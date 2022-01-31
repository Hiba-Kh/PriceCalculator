using Xunit;
using FluentAssertions;
namespace PriceCalculator.Tests
{
    public class CalculatorShould
    {
        private readonly Calculator _sut;
        private readonly float _rate;

        public CalculatorShould()
        {
            _sut = new Calculator();
            _rate = 21;
        }
        [Fact]
        public void DoCalculation()
        {
            Price price = new Price()
            {
                value = 20.25f,
                currency = Currency.USD,
                precision = 2,
            };
            float result = _sut.DoCalculation(price, _rate);
            result.Should().Be(4.25f);
        }

        [Fact]
        public void ReturnResultWithPriceLikePrecision()
        {
            Price price = new Price()
            {
                value = 20.1f,
                currency = Currency.USD,
                precision = 2,
            };
            float result = _sut.DoCalculation(price, _rate);
            result.Should().NotBe(4.2f);
        }


    }
}