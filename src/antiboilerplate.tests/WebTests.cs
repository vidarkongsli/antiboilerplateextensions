using System;
using System.Linq;
using Antiboilerplate.String;
using antiboilerplate.Web;
using FluentAssertions;
using Xunit;

namespace Antiboilerplate.Tests;

public class WebTests
{
    [Fact]
    public void ShouldParseQueryStringLegacy()
    {
        const string testString = "/foo?a=10&b=20";
        var query = testString.ParseQuery();

        query.Keys.Count.Should().Be(2);
        query["a"].Should().Be("10");
        query["b"].Should().Be("20");
    }

    [Fact]
    public void ShouldParseQueryString()
    {
        const string testString = "/foo?a=10&b=20";
        var query = testString.ParseQueryString()
            .ToDictionary(q => q.Key, q => q.Value);

        query.Keys.Count.Should().Be(2);
        query["a"].Should().Be("10");
        query["b"].Should().Be("20");
    }

    [Fact]
    public void ShouldUrlDecodeLegacy()
    {
        const string testString = "/foo?a=https%3A%2F%2Fwww.vg.no%2F&b=20";
        var query = testString.ParseQuery();

        query["a"].Should().Be("https://www.vg.no/");
    }

    [Fact]
    public void ShouldUrlDecode()
    {
        const string testString = "/foo?a=https%3A%2F%2Fwww.vg.no%2F&b=20";
        var query = testString.ParseQueryString()
            .ToDictionary(q => q.Key, q => q.Value);

        query["a"].Should().Be("https://www.vg.no/");
    }

    [Fact]
    public void ShouldWorkWithoutQuestionMarkLegacy()
    {
        const string testString = "a=10&b=20";
        var query = testString.ParseQuery();

        query.Keys.Count.Should().Be(2);
        query["a"].Should().Be("10");
    }

    [Fact]
    public void ShouldWorkWithoutQuestionMark()
    {
        const string testString = "a=10&b=20";
        var query = testString.ParseQueryString()
            .ToDictionary(q => q.Key, q => q.Value);

        query.Keys.Count.Should().Be(2);
        query["a"].Should().Be("10");
    }

    [Fact]
    public void ShouldWorkWithUriClassLegacy()
    {
        var builder = new UriBuilder("https", "test.com", 80, "foo") { Query = "a=1&b=2" };
        var query = builder.Uri.ParseQuery();

        query.Keys.Count.Should().Be(2);
        query["a"].Should().Be("1");
        query["b"].Should().Be("2");
    }

    [Fact]
    public void ShouldWorkWithUriClass()
    {
        var builder = new UriBuilder("https", "test.com", 80, "foo") { Query = "a=1&b=2" };
        var query = builder.Uri.ParseQueryString().ToDictionary(q => q.Key, q => q.Value);

        query.Keys.Count.Should().Be(2);
        query["a"].Should().Be("1");
        query["b"].Should().Be("2");
    }

    [Fact]
    public void ShouldDecodeQueryKeys()
    {
        "vidar+kongsli=1".ParseQueryString().Single().Key.Should().Be("vidar kongsli");
    }

    [Fact]
    public void ShouldHandleNonUniqueQueryStringKeys()
    {
        var query = "a=1&b=2&a=3".ParseQueryString();
        query.Where(n => n.Key == "a").Should().HaveCount(2)
            .And.Contain(n => n.Value == "1")
            .And.Contain(n => n.Value == "3");
    }
}