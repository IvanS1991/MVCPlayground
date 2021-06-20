namespace MVCPlayground.Framework.Http.Response.Content
{
    using MVCPlayground.Framework.Http.Constants;
    using MVCPlayground.Framework.Http.Headers;

    public class HtmlResponse : ContentResponse
    {
        public HtmlResponse(string html)
            : base(html)
        {
            this.headers.Add(new HttpHeader(HttpHeaderName.ContentType, HttpHeaderValue.TextHtml));
        }
    }
}
