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
        protected HttpServer Server;
        protected HttpClient Client;
        protected Mock<ILogger> Logger;

        public ApiTestBase()
        {
            HttpConfiguration configuration = new HttpConfiguration();
            BootStrapper bootStrapper = new BootStrapper();
            Logger = new Mock<ILogger>();
            Logger.Setup(log => log.Log(It.IsAny<string>()));
            ILogger logger = Logger.Object;
            ContainerBuilder containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterInstance(logger).As<ILogger>();
            bootStrapper.Init(configuration, containerBuilder);

            Server = new HttpServer(configuration);
            Client = new HttpClient(Server);
        }

        public void Dispose()
        {
            Client?.Dispose();
            Server?.Dispose();
        }
    }
}