namespace MVCPlayground.Framework.Http.Response
{
    using MVCPlayground.Framework.Http.Constants;

    class NotFoundResponse : HttpResponse
    {
        public NotFoundResponse()
            : base(HttpResponseCode.NotFound) { }
    }
}
