namespace MVCPlayground.Framework.Http.Routing
{
    using MVCPlayground.Framework.Http.Constants;
    using MVCPlayground.Framework.Http.Request;
    using MVCPlayground.Framework.Http.Response;
    using System;

    public interface IHttpRoutingMap
    {
        IHttpRoutingMap Map(HttpMethod method, string path, Func<HttpRequest, HttpResponse> handler);

        IHttpRoutingMap MapGet(string path, Func<HttpRequest, HttpResponse> handler);

        IHttpRoutingMap MapPost(string path, Func<HttpRequest, HttpResponse> handler);

        IHttpRoutingMap MapPut(string path, Func<HttpRequest, HttpResponse> handler);

        IHttpRoutingMap MapDelete(string path, Func<HttpRequest, HttpResponse> handler);

        HttpResponse Handle(HttpRequest request);
    }
}
