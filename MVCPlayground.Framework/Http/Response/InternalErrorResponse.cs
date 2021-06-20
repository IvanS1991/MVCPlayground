namespace MVCPlayground.Framework.Http.Response
{
    using MVCPlayground.Framework.Http.Constants;

    class InternalErrorResponse : HttpResponse
    {
        public InternalErrorResponse()
            : base(HttpResponseCode.InternalError) { }
    }
}
