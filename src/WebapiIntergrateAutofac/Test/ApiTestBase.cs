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
        private IContainer container;

        public ApiTestBase()
        {
            HttpConfiguration configuration = new HttpConfiguration();
            BootStrapper bootStrapper = new BootStrapper();
            container =  bootStrapper.Init(configuration);
            Server = new HttpServer(configuration);
            Client = new HttpClient(Server);
        }

        protected void MockDepedency<T>(T t) where T : class
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterInstance(t).As<T>();
            containerBuilder.Update(container);
        }

        public void Dispose()
        {
            Client?.Dispose();
            Server?.Dispose();
        }
    }
}