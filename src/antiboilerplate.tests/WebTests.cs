using System;
using antiboilerplate.Web;
using FluentAssertions;
using Xunit;

namespace Antiboilerplate.Tests
{
    public class WebTests
    {
        [Fact]
        public void ShouldParseQueryString()
        {
            const string testString = "/foo?a=10&b=20";
            var query = testString.ParseQuery();

            query.Keys.Count.Should().Be(2);
            query["a"].Should().Be("10");
            query["b"].Should().Be("20");
        }

        [Fact]
        public void ShouldUrlDecode()
        {
            const string testString = "/foo?a=https%3A%2F%2Fwww.vg.no%2F&b=20";
            var query = testString.ParseQuery();

            query["a"].Should().Be("https://www.vg.no/");
        }

        [Fact]
        public void ShouldWorkWithoutQuestionMark()
        {
            const string testString = "a=10&b=20";
            var query = testString.ParseQuery();

            query.Keys.Count.Should().Be(2);
            query["a"].Should().Be("10");
        }

        [Fact]
        public void ShouldWorkWithUriClass()
        {
            var builder = new UriBuilder("https", "test.com", 80, "foo") {Query = "a=1&b=2"};
            var query = builder.Uri.ParseQuery();

            query.Keys.Count.Should().Be(2);
            query["a"].Should().Be("1");
            query["b"].Should().Be("2");
        }
    }
}
