using System;
using System.Linq;
using Antiboilerplate.Array;
using FluentAssertions;
using Xunit;

namespace Antiboilerplate.Tests;

public class ArrayTests
{
    [Fact]
    public void Example()
    {
        var (lastName, firstName, rest) =
            "Chaplin, Charlie Spencer".Split(new[] {' ', ','}, StringSplitOptions.RemoveEmptyEntries);

        lastName.Should().Be("Chaplin");
        firstName.Should().Be("Charlie");
        var middleNames = rest.ToArray();
        middleNames.Length.Should().Be(1);
        middleNames.First().Should().Be("Spencer");
    }

    [Fact]
    public void SingleDeconstruct()
    {
        var (first, rest) = "a b c d".Split(' ');
        first.Should().Be("a");
        rest.ToArray().Length.Should().Be(3);
    }

    [Fact]
    public void DoubleDeconstruct()
    {
        var (a, b, rest) = "a b c d".Split(' ');
        a.Should().Be("a");
        b.Should().Be("b");
        rest.ToArray().Length.Should().Be(2);
    }

    [Fact]
    public void TripleDeconstruct()
    {
        var (a, b, c, rest) = "a b c d".Split(' ');
        a.Should().Be("a");
        b.Should().Be("b");
        c.Should().Be("c");
        rest.ToArray().Length.Should().Be(1);
    }
}