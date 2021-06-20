namespace MVCPlayground.Framework.Http.Cookies
{
    using MVCPlayground.Framework.Common;
    using MVCPlayground.Framework.Contracts;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class HttpCookieCollection : INameValuePairCollection<HttpCookie>, IEnumerable<HttpCookie>
    {
        private readonly List<HttpCookie> cookies;
        public HttpCookieCollection()
        {
            this.cookies = new List<HttpCookie>();
        }

        public void Add(HttpCookie cookie)
        {
            Guard.AgainstNull(cookie, nameof(cookie));

            this.cookies.Add(cookie);
        }

        public bool Contains(string name)
        {
            return this.cookies.Any(x => x.Name == name);
        }

        public HttpCookie GetValue(string name)
        {
            return this.cookies.FirstOrDefault(x => x.Name == name);
        }

        public bool IsEmpty()
        {
            return !this.cookies.Any();
        }

        public IEnumerator<HttpCookie> GetEnumerator()
        {
            return this.cookies.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public override string ToString()
        {
            return string.Join("; ", this.cookies
                .Select(x => string.Join('=', x)));
        }
    }
}
