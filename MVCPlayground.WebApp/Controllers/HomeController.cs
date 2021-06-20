namespace MVCPlayground.WebApp.Controllers
{
    using MVCPlayground.Framework.Http.Controllers;
    using MVCPlayground.Framework.Http.Request;
    using MVCPlayground.Framework.Http.Response;

    class HomeController : Controller
    {
        public HomeController(HttpRequest request)
            : base(request) { }

        public HttpResponse Home()
        {
            return View("<h1>Hello!</h1>");
        }
    }
}
