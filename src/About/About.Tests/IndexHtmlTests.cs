using Blazorise;
using FluentAssertions;
using HtmlAgilityPack;
using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace About.Tests
{
    public class IndexHtmlTests
    {
        public const string IndexLocation = @"..\..\..\..\About\wwwroot\index.html";

        [Fact]

        public async Task StyleSheetsInHead()
        {
            var expectedStyleSheets = new[]
            {
                "https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css",
                "https://use.fontawesome.com/releases/v5.12.0/css/all.css",
                "_content/Blazorise.Bootstrap/blazorise.bootstrap.css",
                "_content/Blazorise/blazorise.css",
                "_content/BlazorPro.Spinkit/spinkit.min.css",
                "css/site.css",
                "css/bootstrap/bootstrap.min.css"
            };

            var doc = new HtmlDocument();
            doc.Load(IndexLocation);

            var head = doc.DocumentNode.Descendants("head").ToArray();
            head.Count().Should().Be(1);
            var styleSheets = head.Single().Descendants("link").Where(o=>o.GetAttributeValue("rel", "") == "stylesheet").ToList();

            styleSheets.ForEach(o=>expectedStyleSheets.Any(p=>p == o.GetAttributeValue("href", "")).Should().BeTrue());

            styleSheets.Count().Should().Be(expectedStyleSheets.Length);
        }

        [Fact]
        public async Task ScriptsInBody()
        {
            var expectedScripts = new[]
            {
                "_content/Blazorise.Bootstrap/blazorise.bootstrap.js",
                "_content/Blazorise/blazorise.js",
                "https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/js/bootstrap.min.js",
                "https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.7/umd/popper.min.js",
                "https://code.jquery.com/jquery-3.3.1.slim.min.js",
                "_framework/blazor.webassembly.js"
            };

            var doc = new HtmlDocument();
            doc.Load(IndexLocation);

            var head = doc.DocumentNode.Descendants("body").ToArray();
            head.Count().Should().Be(1);
            var links = head.Single().Descendants("script").ToList();


            links.ForEach(o => expectedScripts.Any(p => p == o.GetAttributeValue("src", "")).Should().BeTrue());

            links.Count().Should().Be(expectedScripts.Length);
        }

        [Fact]
        public async Task Title()
        {
            var doc = new HtmlDocument();
            doc.Load(IndexLocation);
            var title = doc.DocumentNode.Descendants("title").ToArray();
            title.Length.Should().Be(1);
            title.Single().InnerText.Should().Be("About");
        }

    }
}
