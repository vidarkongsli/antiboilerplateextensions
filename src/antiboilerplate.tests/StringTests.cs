using Antiboilerplate.String;
using FluentAssertions;
using Xunit;

namespace Antiboilerplate.Tests
{
    public class StringTests
    {
        [Fact]
        public void ShouldBeAbleToConvertBackAndForth()
        {
            const string testString = "Møøse";

            testString
                .ToUtf8Base64()
                .FromUtf8Base64()
                .Should().Be(testString);
        }
    }
}
