

namespace MVCPlayground.Framework.Http.Headers
{
    using MVCPlayground.Framework.Common;
    using MVCPlayground.Framework.Contracts;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class HttpHeaderCollection : INameValuePairCollection<HttpHeader>, IEnumerable<HttpHeader>
    {
        private List<HttpHeader> headers;

        public HttpHeaderCollection()
        {
            this.headers = new List<HttpHeader>();
        }

        public void Add(HttpHeader header)
        {
            this.headers.Add(header);
        }

        public bool Contains(string name)
        {
            Guard.AgainstNull(name, nameof(name));

            return this.headers.Any(x => x.Name == name);
        }

        public HttpHeader GetValue(string name)
        {
            Guard.AgainstNull(name, nameof(name));

            return this.headers.FirstOrDefault(x => x.Name == name);
        }

        public bool IsEmpty()
        {
            return !this.headers.Any();
        }

        public IEnumerator<HttpHeader> GetEnumerator()
        {
            return this.headers.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public override string ToString()
        {
            return string.Join("\n", this.Select(x => x.ToString()));
        }
    }
}
