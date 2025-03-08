using Antiboilerplate.String;
using FluentAssertions;
using Xunit;

namespace Antiboilerplate.Tests;

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

    [Fact]
    public void ShouldReportIsNullOrEmptyCorrectly()
    {
        const string nullString = null;
        nullString.IsNullOrEmpty().Should().BeTrue("A null string should report true");
        string.Empty.IsNullOrEmpty().Should().BeTrue("An empty string should report true");
        "Not empty".IsNullOrEmpty().Should().BeFalse();
    }

    [Fact]
    public void ShouldReportHasTextCorrectly()
    {
        const string nullString = null;
        nullString.HasText().Should().BeFalse("A null string should report false");
        string.Empty.HasText().Should().BeFalse("An empty string should report false");
        "Not empty".HasText().Should().BeTrue();
    }

    [Fact]
    public void ShouldReportIsNullOrWhitespaceCorrectly()
    {
        const string nullString = null;
        nullString.IsNullOrWhitespace().Should().BeTrue("A null string should report true");
        string.Empty.IsNullOrWhitespace().Should().BeTrue("An empty string should report true");
        "\t".IsNullOrWhitespace().Should().BeTrue("A tab is whitespace");
        "Not empty".IsNullOrWhitespace().Should().BeFalse();
    }

    [Fact]
    public void ShouldMergeStrings() => new[]{"1", "2"}
        .AggregateWithDelimiter(", ").Should().Be("1, 2");

    [Fact]
    public void ShouldMergeEmptyEnumerable() => System.Array.Empty<string>()
        .AggregateWithDelimiter(", ").Should().Be(string.Empty);
}