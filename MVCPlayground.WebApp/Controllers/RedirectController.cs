namespace MVCPlayground.WebApp.Controllers
{
    using MVCPlayground.Framework.Common;
    using MVCPlayground.Framework.Http.Controllers;
    using MVCPlayground.Framework.Http.Request;
    using MVCPlayground.Framework.Http.Response;

    class RedirectController : Controller
    {
        public RedirectController(HttpRequest request)
            : base(request) { }

        public HttpResponse GoogleSearch()
        {
            string searchQueryKey = "search";
            string searchValue;

            Url url = new Url();

            url.Path = "https://www.google.com/search";

            if (this.Request.Url.Query.TryGetValue(searchQueryKey, out searchValue))
            {
                url.Query.Add("q", searchValue);
            }

            return Redirect(url.ToString());
        }
    }
}
