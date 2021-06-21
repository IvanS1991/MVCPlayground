namespace MVCPlayground.Framework.Http.Request
{
    using MVCPlayground.Framework.Common;
    using MVCPlayground.Framework.Http.Constants;
    using MVCPlayground.Framework.Http.Cookies;
    using MVCPlayground.Framework.Http.Headers;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class HttpRequest
    {
        public HttpRequest(string requestText)
        {
            Guard.AgainstNull(requestText, nameof(requestText));

            string[] requestLines = requestText.Split(Environment.NewLine);

            var (method, url, httpVersion) = HttpRequest.ParseEndpoint(requestLines);

            this.Method = method;
            this.Url = url;
            this.HttpVersion = httpVersion;

            this.Headers = HttpRequest.ParseHeaders(requestLines);
            this.Cookies = HttpRequest.ParseCookies(this.Headers);
            this.Body = HttpRequest.ParseBody(requestLines, this.Headers.Count());
        }

        public HttpMethod Method { get; }

        public Url Url { get; }

        public string HttpVersion { get; }

        public HttpHeaderCollection Headers { get; }

        public HttpCookieCollection Cookies { get; }

        public string Body { get;  }

        private static (HttpMethod, Url, string) ParseEndpoint(string[] requestLines)
        {
            string endpointLine = requestLines.First();
            string[] pieces = endpointLine.Split(" ");

            string methodString = pieces[0];
            string url = pieces[1];
            string httpVersion = pieces[2];

            Guard.AgainstNull(methodString, nameof(methodString));
            Guard.AgainstNull(url, nameof(url));
            Guard.AgainstNull(httpVersion, nameof(httpVersion));

            var method = Enum.Parse<HttpMethod>(methodString);

            return (method, Url.Parse(url), httpVersion);
        }

        private static HttpHeaderCollection ParseHeaders(string[] requestLines)
        {
            HttpHeaderCollection collection = new HttpHeaderCollection();

            foreach (var line in requestLines.Skip(1))
            {
                if (line.Trim() == string.Empty)
                {
                    break;
                }

                var pieces = line.Split(':')
                    .Select(x => x.Trim());

                if (pieces.Count() == 2)
                {
                    collection.Add(new HttpHeader(pieces.First(), pieces.Last()));
                }
            }

            return collection;
        }

        private static string ParseBody(string[] requestLines, int headersCount)
        {
            IEnumerable<string> bodyLines = requestLines.Skip(2 + headersCount);

            if (!bodyLines.Any())
            {
                return null;
            }

            return bodyLines.First();
        }

        private static HttpCookieCollection ParseCookies(HttpHeaderCollection headers)
        {
            HttpCookieCollection collection = new HttpCookieCollection();

            var cookieHeader = headers.Get(HttpHeaderName.Cookie);

            if (cookieHeader != null)
            {
                var cookies = cookieHeader.Value
                    .Split(';', StringSplitOptions.RemoveEmptyEntries)
                    .Select(cookie => cookie.Trim()
                        .Split('=', StringSplitOptions.RemoveEmptyEntries))
                    .Where(x => x.Length == 2)
                    .Select(x => new HttpCookie(x.First(), x.Last()));

                foreach (var cookie in cookies)
                {
                    collection.Add(cookie);
                }
            }

            return collection;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{this.Method} {this.Url} {this.HttpVersion}");
            sb.AppendLine(this.Headers.ToString());

            if (this.Body != null)
            {
                sb.AppendLine("");
                sb.AppendLine(this.Body);
            }

            return sb.ToString();
        }
    }
}
