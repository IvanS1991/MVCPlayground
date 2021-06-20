namespace MVCPlayground.WebApp
{
    using System.Threading.Tasks;
    using MVCPlayground.Framework.Http;
    using MVCPlayground.Framework.Http.Routing;
    using MVCPlayground.Framework.Logging;
    using MVCPlayground.WebApp.Controllers;

    class Program
    {
        static async Task Main(string[] args)
        {
            var server = new HttpServer();

            await server
                .WithLogging(new ConsoleLogger())
                .WithRouting(new HttpRoutingMap(), r => r
                    .MapGet("/", req => new HomeController(req).Home())
                    .MapGet("/Google", req => new RedirectController(req).GoogleSearch())
                    .MapGet("/Login", req => new UserController(req).Login())
                )
                .Start();
        }
    }
}
