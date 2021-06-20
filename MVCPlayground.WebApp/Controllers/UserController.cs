namespace MVCPlayground.WebApp.Controllers
{
    using MVCPlayground.Framework.Http.Constants;
    using MVCPlayground.Framework.Http.Controllers;
    using MVCPlayground.Framework.Http.Request;
    using MVCPlayground.Framework.Http.Response;
    using System;

    class UserController : Controller
    {
        public UserController(HttpRequest request)
            : base(request) { }

        public HttpResponse Login()
        {
            return this.Redirect("/")
                .SetCookie(HttpCookieName.SessionId, Guid.NewGuid().ToString());
        }
    }
}
