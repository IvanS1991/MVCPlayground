namespace MVCPlayground.Framework.Http.Cookies
{
    using MVCPlayground.Framework.Common;
    using System.Text;

    public class HttpCookie : NameValuePair
    {
        public HttpCookie(string name, string value)
            : base(name, value) { }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append($"{this.Name}=${this.Value}");

            return sb.ToString();
        }
    }
}
