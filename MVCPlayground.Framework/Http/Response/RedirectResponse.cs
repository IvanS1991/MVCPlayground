using MVCPlayground.Framework.Http.Constants;
using MVCPlayground.Framework.Http.Headers;

namespace MVCPlayground.Framework.Http.Response
{
    public class RedirectResponse : HttpResponse
    {
        public RedirectResponse(string url)
            : base(HttpResponseCode.Found)
        {
            this.headers.Add(new HttpHeader(HttpHeaderName.Location, url));
        }
    }
}
