namespace MVCPlayground.Framework.Http.Headers
{
    using MVCPlayground.Framework.Common;

    public class HttpHeader : NameValuePair
    {
        public HttpHeader(string name, string value)
            : base(name, value) { }

        public override string ToString()
        {
            return $"{this.Name}: {this.Value}";
        }
    }
}
