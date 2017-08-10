using System;
using System.Net.Http;
using System.Web.Http;
using Autofac;
using Moq;
using WebApi;

namespace Test
{
    public class ApiTestBase : IDisposable
    {
        protected HttpServer server;
        protected HttpClient client;
        protected Mock<ILogger> mock;
        protected ILogger logger;

        public ApiTestBase()
        {
            HttpConfiguration configuration = new HttpConfiguration();
            BootStrapper bootStrapper = new BootStrapper();
            mock = new Mock<ILogger>();
            mock.Setup(log => log.Log(It.IsAny<string>()));
            logger = mock.Object;
            bootStrapper.GetContainerBuilder().RegisterInstance(logger).As<ILogger>();
            bootStrapper.Init(configuration);

            server = new HttpServer(configuration);
            client = new HttpClient(server);
        }

        public void Dispose()
        {
            client?.Dispose();
            server?.Dispose();
        }
    }
}