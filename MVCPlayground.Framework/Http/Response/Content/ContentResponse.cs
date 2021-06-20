namespace MVCPlayground.Framework.Http.Response.Content
{
    using MVCPlayground.Framework.Common;
    using MVCPlayground.Framework.Http.Constants;

    public class ContentResponse : HttpResponse
    {
        public ContentResponse(string content)
            : base(HttpResponseCode.OK)
        {
            Guard.AgainstNull(content, nameof(content));

            this.Content = content;
        }
    }
}
