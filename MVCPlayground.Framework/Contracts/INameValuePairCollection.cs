namespace MVCPlayground.Framework.Contracts
{
    using MVCPlayground.Framework.Common;

    interface INameValuePairCollection<T>
        where T : NameValuePair
    {
        void Add(T nameValuePair);

        bool Contains(string name);

        T Get(string name);
    }
}
