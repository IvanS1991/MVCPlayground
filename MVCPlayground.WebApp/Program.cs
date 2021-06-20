namespace MVCPlayground.WebApp
{
    using System.Threading.Tasks;
    using MVCPlayground.Framework.Http;
    using MVCPlayground.Framework.Http.Response.Content;
    using MVCPlayground.Framework.Http.Routing;
    using MVCPlayground.Framework.Logging;

    class Program
    {
        static async Task Main(string[] args)
        {
            var server = new HttpServer();

            await server
                .WithLogging(new ConsoleLogger())
                .WithRouting(new HttpRoutingMap(), r => r
                    .MapGet("/", req => new HtmlResponse("<h1>Works!</h1"))
                    .MapGet("/Cats", req => new HtmlResponse("<h1>Cats</h1>"))
                )
                .Start();
        }
    }
}
