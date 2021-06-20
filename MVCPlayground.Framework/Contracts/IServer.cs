namespace MVCPlayground.Framework.Contracts
{
    using MVCPlayground.Framework.Http.Routing;
    using System;
    using System.Threading.Tasks;

    public interface IServer
    {
        IServer WithLogging(ILogger logger);

        IServer WithRouting(IHttpRoutingMap routingMap, Action<IHttpRoutingMap> configurator);

        Task Start();
    }
}
