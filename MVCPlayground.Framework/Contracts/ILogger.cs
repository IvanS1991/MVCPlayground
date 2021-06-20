namespace MVCPlayground.Framework.Contracts
{
    using System.Threading.Tasks;

    public interface ILogger
    {
        Task Log(string message);
    }
}
