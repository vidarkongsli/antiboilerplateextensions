using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Xml.Linq;
using antiboilerplate.tests.TestData;
using Antiboilerplate.Resources;
using FluentAssertions;
using Xunit;

namespace Antiboilerplate.Tests;

public class EmbeddedResourceTests
{
    [Fact]
    public async Task ShouldBeAbleToReadJsonFile()
    {
        var data = await EmbeddedResource.ReadJson<ConfigurationTestData>();
        data.Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task ShouldBeAbleToReadAFile()
    {
        var data = await EmbeddedResource.Read("antiboilerplate.tests.TestData.ConfigurationTestData.json",
            Assembly.GetExecutingAssembly());
        data.Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task ShouldBeAbleToReadAndDeserializeJsonFile()
    {
        var config = await EmbeddedResource.ReadAndDeserializeJson<ConfigurationTestData, ConfigurationTestContents>();
        config.Exclude.Should().HaveCountGreaterThan(0);
    }

    [DataContract]
    public class ConfigurationTestContents
    {
        [DataMember(Name="exclude")]
        public ICollection<string> Exclude { get; set; }
    }

    [Fact]
    public async Task ShouldBeABleToReadAndParseXmlFile()
    {
        var data = await EmbeddedResource.ReadXml<XmlTestData>();
        XNamespace ns = "urn:oasis:names:tc:SAML:2.0:protocol";

        var format =
            (from el in data.Descendants()
                where el.Name == ns + "NameIDPolicy"
                select el.Attribute("Format")).FirstOrDefault();

        format.Should().NotBeNull();
    }
}