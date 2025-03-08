using System.Collections.Generic;
using System.Linq;
using Antiboilerplate.String;
using FluentAssertions;
using Xunit;

namespace antiboilerplate.tests
{
    public class EnumerableTests
    {
        [Fact]
        public void SH()
        {
            var dictionary = new List<KeyValuePair<string, string>>
            {
                new("a", "1"),
                new("b", "2"),
                new("a", "3")
            }.ToDictionaryOfArrays();

            dictionary.Keys.Should().Contain("a");
            dictionary["a"].Should().HaveCount(2).And.Contain("1").And.Contain("3");
            dictionary.Keys.Should().Contain("b");
            dictionary["b"].Single().Should().Be("2");
        }
    }
}
