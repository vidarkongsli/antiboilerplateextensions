using System;
using antiboilerplate.Web;
using FluentAssertions;
using Xunit;

namespace Antiboilerplate.Tests
{
    public class UrlTests
    {
        [Theory]
        [InlineData(Url.Scheme.Http, "www.iana.org", null, null, null, "http://www.iana.org/")]
        [InlineData(Url.Scheme.Http, "www.iana.org", null, "user", "pass", "http://user:pass@www.iana.org/")]
        [InlineData(Url.Scheme.Https, "www.iana.org", null, null, null, "https://www.iana.org/")]
        [InlineData(Url.Scheme.Http, "www.iana.org", "path", null, null, "http://www.iana.org/path")]
        public void NormalCases(Url.Scheme scheme, string host, string path,
            string username, string password, string expectedUrl)
        {
            var url = Url.Create().WithScheme(scheme).WithHost(host);
            if (path != null) url.WithPath(path);
            if (username != null) url.WithUsername(username);
            if (password != null) url.WithPassword(password);

            string result = url;
            result.Should().Be(expectedUrl);
        }

        [Fact]
        public void ShouldBeAbleToAddQueryParameters()
        {
            string url = Url.Create().WithHost("localhost")
                .WithQueryParameter("a", "1")
                .WithQueryParameter("b", "2")
                .WithQueryParameter("a b", "1 2");
            url.Should().EndWith("/?a=1&b=2&a+b=1+2");
        }

        [Fact]
        public void ShouldBeAbleToUseExistingQueryAndAddParametersToIt()
        {
            string url = Url.Create().WithHost("localhost")
                .WithQuery("a+b=1+2")
                .WithQueryParameter("c", "3");
            url.Should().EndWith("/?a+b=1+2&c=3");
        }

        [Fact]
        public void ShouldBeAbleToCreateWithFragment()
        {
            string url = Url.Create().WithHost("localhost").WithFragment("chapter1");
            url.Should().EndWith("#chapter1");
        }

        [Fact]
        public void ShouldBeAbleToUseForHostOverload()
        {
            Uri url = Url.CreateForHost("localhost");
            url.Host.Should().Be("localhost");
        }
    }
}
