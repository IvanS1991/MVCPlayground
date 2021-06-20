namespace MVCPlayground.Framework.Http.Response.Content
{
    using MVCPlayground.Framework.Http.Constants;
    using MVCPlayground.Framework.Http.Headers;

    public class TextResponse : ContentResponse
    {
        public TextResponse(string text)
            : base(text)
        {
            this.headers.Add(new HttpHeader(HttpHeaderName.ContentType, HttpHeaderValue.TextPlain));
        }
    }
}
