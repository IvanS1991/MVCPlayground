using MVCPlayground.Framework.Http.Constants;
using MVCPlayground.Framework.Http.Request;
using NUnit.Framework;
using System;
using System.Text;

namespace MVCPlayground.Framework.Tests.Http.Request
{
    class HttpRequestTests
    {
        [Test]
        public void Constructor_ShouldParseRequestCorrectly_1()
        {
            HttpMethod method = HttpMethod.PUT;
            string url = "/test/asd?first=5";

            string requestText = new HttpRequestTestsDataBuilder()
                .SetEndpoint(method, url)
                .SetHeader("test_header", "5")
                .SetBody("test_body")
                .Result();

            HttpRequest request = new HttpRequest(requestText);

            Assert.AreEqual(method, request.Method);
            Assert.AreEqual(url, request.Url.ToString());
            Assert.AreEqual("5", request.Headers.Get("test_header").Value);
            Assert.AreEqual(true, request.Cookies.IsEmpty());
            Assert.AreEqual("test_body", request.Body);
        }

        [Test]
        public void Constructor_ShouldParseRequestCorrectly_2()
        {
            HttpMethod method = HttpMethod.DELETE;
            string url = "/asd/dsa";

            string requestText = new HttpRequestTestsDataBuilder()
                .SetEndpoint(method, url)
                .SetHeader("test_header_1", "666")
                .SetBody("test_body_1")
                .Result();

            HttpRequest request = new HttpRequest(requestText);

            Assert.AreEqual(method, request.Method);
            Assert.AreEqual(url, request.Url.ToString());
            Assert.AreEqual("666", request.Headers.Get("test_header_1").Value);
            Assert.AreEqual(true, request.Cookies.IsEmpty());
            Assert.AreEqual("test_body_1", request.Body);
        }

        [Test]
        public void Constructor_ShouldParseRequestCorrectly_3()
        {
            HttpMethod method = HttpMethod.DELETE;
            string url = "/asd/dsa?test=true";

            string requestText = new HttpRequestTestsDataBuilder()
                .SetEndpoint(method, url)
                .Result();

            HttpRequest request = new HttpRequest(requestText);

            Assert.AreEqual(method, request.Method);
            Assert.AreEqual(url, request.Url.ToString());
            Assert.AreEqual(true, request.Headers.IsEmpty());
            Assert.AreEqual(true, request.Cookies.IsEmpty());
            Assert.AreEqual(null, request.Body);
        }

        [Test]
        public void Constructor_ShouldParseRequestCorrectly_4()
        {
            HttpMethod method = HttpMethod.DELETE;
            string url = "/asd/dsa?test=true";

            string requestText = new HttpRequestTestsDataBuilder()
                .SetEndpoint(method, url)
                .SetHeader(HttpHeaderName.Cookie, "cookie_name=cookie_val")
                .Result();

            HttpRequest request = new HttpRequest(requestText);

            Assert.AreEqual(method, request.Method);
            Assert.AreEqual(url, request.Url.ToString());
            Assert.AreEqual("cookie_name=cookie_val", request.Headers.Get(HttpHeaderName.Cookie).Value);
            Assert.AreEqual("cookie_val", request.Cookies.Get("cookie_name").Value);
            Assert.AreEqual(null, request.Body);
        }

        [Test]
        public void ToString_ShouldOutputSameRequest()
        {
            HttpMethod method = HttpMethod.DELETE;
            string url = "/asd/dsa?test=true";

            string requestText = new HttpRequestTestsDataBuilder()
                .SetEndpoint(method, url)
                .SetHeader(HttpHeaderName.Cookie, "cookie_name=cookie_val")
                .Result();

            HttpRequest request = new HttpRequest(requestText);

            Assert.AreEqual(requestText, request.ToString());
        }
    }

    class HttpRequestTestsDataBuilder
    {
        private readonly StringBuilder sb = new StringBuilder();
        
        public HttpRequestTestsDataBuilder SetEndpoint(HttpMethod method, string url)
        {
            sb.AppendLine($"{method} {url} HTTP/1.1");

            return this;
        }

        public HttpRequestTestsDataBuilder SetHeader(string name, string value)
        {
            sb.AppendLine($"{name}: {value}");

            return this;
        }

        public HttpRequestTestsDataBuilder SetBody(string body)
        {
            sb.AppendLine($"");
            sb.AppendLine(body);

            return this;
        }

        public string Result()
        {
            return this.sb.ToString();
        }
    }
}
