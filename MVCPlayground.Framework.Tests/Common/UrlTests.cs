using MVCPlayground.Framework.Common;
using NUnit.Framework;
using System;

namespace MVCPlayground.Framework.Tests.Common
{
    class UrlTests
    {
        private UrlTestsData data;

        [SetUp]
        public void SetUp()
        {
            data = new UrlTestsData("/test/asd", "first=5&second=true");
        }

        [TearDown]
        public void TearDown()
        {
            data = null;
        }

        [Test]
        public void ToString_ShouldCorrectlyFormatUrl()
        {
            var url = Url.Parse(data.Url);

            Assert.AreEqual(url.ToString(), data.Url);
        }
    }

    class UrlTestsStatic
    {
        private UrlTestsData data;

        [SetUp]
        public void SetUp()
        {
            data = new UrlTestsData("/test/asd", "first=5&second=true");
        }

        [TearDown]
        public void TearDown()
        {
            data = null;
        }

        [Test]
        public void ParseQuery_ShouldThrowWithNullString()
        {
            Assert.Throws(
                Is.TypeOf<ArgumentNullException>(),
                () => Url.ParseQuery(null));
        }

        [Test]
        public void ParseQuery_ShouldCorrectlyParseValidQueryString()
        {
            var parsedQuery = Url.ParseQuery(data.Query);

            Assert.AreEqual(parsedQuery["first"], "5");
            Assert.AreEqual(parsedQuery["second"], "true");
        }

        [Test]
        public void ParseQuery_ShouldCorrectlyParseEmptyQueryString()
        {
            var parsedQuery = Url.ParseQuery("");

            Assert.AreEqual(parsedQuery.Count, 0);
        }

        [Test]
        public void Parse_ShouldThrowWithNullString()
        {
            Assert.Throws(
                Is.TypeOf<ArgumentNullException>(),
                () => Url.ParseQuery(null));
        }

        [Test]
        public void Parse_ShouldCorrectlyParseValidUrl()
        {
            Url parsedUrl = Url.Parse(data.Url);

            Assert.AreEqual(parsedUrl.Path, data.Path);

            Assert.AreEqual(parsedUrl.Query["first"], "5");
            Assert.AreEqual(parsedUrl.Query["second"], "true");

        }

        [Test]
        public void ParseQuery_ShouldCorrectlyParseEmptyUrlString()
        {
            Url parsedUrl = Url.Parse("");

            Assert.AreEqual(parsedUrl.Path, null);
            Assert.AreEqual(parsedUrl.Query.Count, 0);
        }
    }
    class UrlTestsData
    {
        public UrlTestsData(string path, string query)
        {
            this.Path = path;
            this.Query = query;
        }

        public string Path { get; }

        public string Query { get; }

        public string Url => $"{Path}?{Query}";
    }
}
