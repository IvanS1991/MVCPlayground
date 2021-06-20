using MVCPlayground.Framework.Http.Request;
using MVCPlayground.Framework.Http.Response;
using MVCPlayground.Framework.Http.Response.Content;

namespace MVCPlayground.Framework.Http.Controllers
{
    public abstract class Controller
    {
        public Controller(HttpRequest request)
        {
            this.Request = request;
        }

        protected HttpRequest Request { get; }

        protected HttpResponse View(string html)
        {
            return new HtmlResponse(html);
        }

        protected HttpResponse Text(string text)
        {
            return new TextResponse(text);
        }

        protected HttpResponse NotFound()
        {
            return new NotFoundResponse();
        }

        protected HttpResponse Redirect(string url)
        {
            return new RedirectResponse(url);
        }
    }
}
