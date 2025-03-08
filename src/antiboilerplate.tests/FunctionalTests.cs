using System.Linq;
using Antiboilerplate.Functional;
using FluentAssertions;
using Xunit;

namespace Antiboilerplate.Tests;

public class FunctionalTests
{
    [Fact]
    public void MapShouldWork()
    {
        1.Map(y => $"x:{y}").Should().Be("x:1");
    }

    [Fact]
    public void MapShouldReturnDefaultWhenNull()
    {
        string test = null;
        // ReSharper disable once ExpressionIsAlwaysNull
        var testInteger = test.Map(_ => 1);
        testInteger.Should().Be(default(int));
    }

    [Fact]
    public void ThenShouldWork()
    {
        var called = false;
        1.Then(y => called = true).Should().Be(1);
        called.Should().BeTrue();
    }

    [Fact]
    public void ThenShouldNotDoAnythingWhenNull()
    {
        string s = null;
        var called = false;
        // ReSharper disable once ExpressionIsAlwaysNull
        s.Then(_ => called = true).Should().BeNull();
        called.Should().BeFalse();
    }

    [Fact]
    public void AsCollectionShouldWork()
    {
        var arr = "a".AsCollection();
        arr.Count.Should().Be(1);
        arr.First().Should().Be("a");
    }

    [Fact]
    public void AsCollectionShouldPassEmptyCollectionWhenGivenNull()
    {
        ((string)null).AsCollection().Count.Should().Be(0);
    }

    [Fact]
    public void EachShouldWork()
    {
        var arr = new []{1, 2, 3};
        var aggregate = 0;
        arr.Each(x => aggregate += x);
        aggregate.Should().Be(6);
    }

    [Fact]
    public void EachShouldNotCallbackWhenPassedNull()
    {
        var called = false;
        ((string[])null).Each(x => called = true);
        called.Should().BeFalse();
    }
}