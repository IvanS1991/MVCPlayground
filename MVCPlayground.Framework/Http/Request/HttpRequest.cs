namespace MVCPlayground.Framework.Http.Request
{
    using MVCPlayground.Framework.Common;
    using MVCPlayground.Framework.Http.Constants;
    using MVCPlayground.Framework.Http.Headers;
    using System;
    using System.Linq;
    using System.Text;

    public class HttpRequest
    {
        public HttpRequest(string requestText)
        {
            Guard.AgainstNull(requestText, nameof(requestText));

            string[] requestLines = requestText.Split("\r\n");

            var (method, url, httpVersion) = HttpRequest.ParseEndpoint(requestLines);

            this.Method = method;
            this.Url = url;
            this.HttpVersion = httpVersion;

            this.Headers = HttpRequest.ParseHeaders(requestLines);
            this.Body = HttpRequest.ParseBody(requestLines, this.Headers.Count());
        }

        public HttpMethod Method { get; private set; }

        public string Url { get; set; }

        public string HttpVersion { get; set; }

        public HttpHeaderCollection Headers { get; private set; }

        public string Body { get; private set; }

        private static (HttpMethod, string, string) ParseEndpoint(string[] requestLines)
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

            return (method, url, httpVersion);
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
            return requestLines.Skip(2 + headersCount).First();
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
