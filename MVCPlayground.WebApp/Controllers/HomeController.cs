namespace MVCPlayground.WebApp.Controllers
{
    using MVCPlayground.Framework.Http.Constants;
    using MVCPlayground.Framework.Http.Controllers;
    using MVCPlayground.Framework.Http.Request;
    using MVCPlayground.Framework.Http.Response;

    class HomeController : Controller
    {
        public HomeController(HttpRequest request)
            : base(request) { }

        public HttpResponse Home()
        {
            if (this.Request.Cookies.Contains(HttpCookieName.SessionId))
            {
                return View("<h1>Hello, you are logged in!</h1>");
            }

            return View("<a href=\"/Login\">To Login</a>");
        }
    }
}
