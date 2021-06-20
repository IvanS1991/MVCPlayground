namespace MVCPlayground.Framework.Common
{
    public abstract class NameValuePair
    {
        public NameValuePair(string name, string value)
        {
            Guard.AgainstNull(name, nameof(name));
            Guard.AgainstNull(value, nameof(value));

            this.Name = name;
            this.Value = value;
        }

        public string Name { get; private set; }

        public string Value { get; private set; }
    }
}
