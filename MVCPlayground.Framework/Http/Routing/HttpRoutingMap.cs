namespace MVCPlayground.Framework.Http.Routing
{
    using MVCPlayground.Framework.Common;
    using MVCPlayground.Framework.Http.Constants;
    using MVCPlayground.Framework.Http.Request;
    using MVCPlayground.Framework.Http.Response;
    using System;
    using System.Collections.Generic;

    public class HttpRoutingMap : IHttpRoutingMap
    {
        private Dictionary<string, Func<HttpRequest, HttpResponse>> handlers;

        public HttpRoutingMap()
        {
            this.handlers = new Dictionary<string, Func<HttpRequest, HttpResponse>>();
        }

        private string GetHandlerKey(HttpMethod method, string path)
        {
            return $"{method} {path}".ToLower();
        }

        public IHttpRoutingMap Map(HttpMethod method, string path, Func<HttpRequest, HttpResponse> handler)
        {
            Guard.AgainstNull(method, nameof(method));
            Guard.AgainstNull(path, nameof(path));
            Guard.AgainstNull(handler, nameof(handler));

            this.handlers[this.GetHandlerKey(method, path)] = handler;

            return this;
        }

        public IHttpRoutingMap MapGet(string path, Func<HttpRequest, HttpResponse> handler)
        {
            return this.Map(HttpMethod.GET, path, handler);
        }

        public IHttpRoutingMap MapPost(string path, Func<HttpRequest, HttpResponse> handler)
        {
            return this.Map(HttpMethod.POST, path, handler);
        }

        public IHttpRoutingMap MapPut(string path, Func<HttpRequest, HttpResponse> handler)
        {
            return this.Map(HttpMethod.PUT, path, handler);
        }

        public IHttpRoutingMap MapDelete(string path, Func<HttpRequest, HttpResponse> handler)
        {
            return this.Map(HttpMethod.DELETE, path, handler);
        }

        public Func<HttpRequest, HttpResponse> GetHandler(HttpMethod method, string path)
        {
            Guard.AgainstNull(method, nameof(method));
            Guard.AgainstNull(path, nameof(path));

            string key = GetHandlerKey(method, path);

            if (!this.handlers.ContainsKey(key))
            {
                return null;
            }

            return this.handlers[key];
        }

        public HttpResponse Handle(HttpRequest request)
        {
            Guard.AgainstNull(request, nameof(request));

            var key = this.GetHandlerKey(request.Method, request.Url.Path);

            if (!this.handlers.ContainsKey(key))
            {
                return new NotFoundResponse();
            }

            return this.handlers[key](request);
        }
    }
}
