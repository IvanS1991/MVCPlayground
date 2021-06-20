namespace MVCPlayground.Framework.Http.Response
{
    using MVCPlayground.Framework.Common;
    using MVCPlayground.Framework.Http.Constants;
    using MVCPlayground.Framework.Http.Headers;
    using System;
    using System.Text;

    public abstract class HttpResponse
    {
        protected readonly HttpHeaderCollection headers;
        private readonly HttpResponseCode responseCode;

        public HttpResponse(HttpResponseCode code)
        {
            Guard.AgainstNull(code, nameof(code));

            this.responseCode = code;
            this.headers = new HttpHeaderCollection();

            this.headers.Add(new HttpHeader(HttpHeaderName.Server, HttpHeaderValue.ServerName));
            this.headers.Add(new HttpHeader(HttpHeaderName.Date, DateTime.UtcNow.ToString()));
        }

        protected string Content { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"HTTP/{HttpServer.HttpVersion} {(int)this.responseCode} {this.responseCode.ToString()}");

            sb.AppendLine(this.headers.ToString());

            if (this.Content != null)
            {
                sb.AppendLine("");
                sb.AppendLine(this.Content);
            }

            return sb.ToString().Trim();
        }
    }
}
