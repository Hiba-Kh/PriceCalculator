using Xunit;
using FluentAssertions;
namespace PriceCalculator.Tests
{
    public class FloatExtensionsShould
    {
        [Fact]
        public void ReturnNumberWithRequestedPrecision()
        {
            float sut = 2.4318f;
            float result = sut.RoundToPrecision(1);
            result.Should().Be(2.4f);
        }
    }
}
