namespace MVCPlayground.Framework.Http
{
    using System;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;
    using System.Threading.Tasks;
    using MVCPlayground.Framework.Common;
    using MVCPlayground.Framework.Contracts;
    using MVCPlayground.Framework.Http.Request;
    using MVCPlayground.Framework.Http.Response;
    using MVCPlayground.Framework.Http.Routing;

    public class HttpServer : IServer
    {
        public const string HttpVersion = "1.1";

        private readonly IPAddress IP;
        private readonly int port;
        private readonly int bufferSize;
        private readonly TcpListener listener;

        public ILogger logger;
        public IHttpRoutingMap routingMap;

        public HttpServer()
            : this("127.0.0.1") { }

        public HttpServer(string ipString)
            : this(ipString, 8080) {}

        public HttpServer(string ipString, int port)
            : this(ipString, port, 1024) { }

        public HttpServer(string ipString, int port, int bufferSize)
        {
            Guard.AgainstNull(ipString, nameof(ipString));
            
            this.IP = IPAddress.Parse(ipString);
            this.port = port;
            this.bufferSize = bufferSize;

            this.listener = new TcpListener(this.IP, this.port);
        }

        private async Task<HttpRequest> HandleRequest(NetworkStream stream)
        {
            StringBuilder requestBuilder = new StringBuilder();
            byte[] buffer = new byte[this.bufferSize];

            while (stream.DataAvailable)
            {
                int bytesRead = await stream.ReadAsync(buffer, 0, this.bufferSize);
                string text = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                requestBuilder.Append(text);
            }

            var requestText = requestBuilder.ToString();

            HttpRequest request = new HttpRequest(requestText);

            await this.logger.Log(requestText);

            return request;
        }

        private async Task WriteResponse(NetworkStream stream, HttpResponse response)
        {
            string responseText = response.ToString();

            await this.logger.Log(responseText);

            await stream.WriteAsync(Encoding.UTF8.GetBytes(responseText));
        }

        private async Task HandleResponse(NetworkStream stream, HttpRequest request)
        {
            HttpResponse response = this.routingMap.Handle(request);

            await this.WriteResponse(stream, response);
        }

        private async Task HandleClient()
        {
            var client = await listener.AcceptTcpClientAsync();

            await this.logger.Log($"Accepted client!");

            using var stream = client.GetStream();

            try
            {
                if (stream.DataAvailable)
                {
                    var request = await this.HandleRequest(stream);

                    await this.HandleResponse(stream, request);
                }
            }
            catch
            {
                await this.WriteResponse(stream, new InternalErrorResponse());
            }

            client.Close();
        }

        public async Task Start()
        {
            Guard.AgainstNull(this.logger, nameof(this.logger));
            Guard.AgainstNull(this.routingMap, nameof(this.routingMap));

            this.listener.Start();

            await this.logger.Log($"Listening at {this.IP}:{this.port}...");

            while (true)
            {
                await this.HandleClient();
            }
        }

        public IServer WithLogging(ILogger logger)
        {
            Guard.AgainstNull(logger, nameof(logger));

            this.logger = logger;

            return this;
        }

        public IServer WithRouting(IHttpRoutingMap routingMap, Action<IHttpRoutingMap> routingMapConfigurator)
        {
            Guard.AgainstNull(routingMap, nameof(routingMap));
            Guard.AgainstNull(routingMapConfigurator, nameof(routingMapConfigurator));

            this.routingMap = routingMap;
            routingMapConfigurator(this.routingMap);

            return this;
        }
    }
}
