namespace MVCPlayground.Framework.Logging
{
    using System;
    using System.Threading.Tasks;
    using MVCPlayground.Framework.Contracts;

    public class ConsoleLogger : ILogger
    {
        public Task Log(string message)
        {
            return Task.Run(() => Console.WriteLine($"{message}\n"));
        }
    }
}
