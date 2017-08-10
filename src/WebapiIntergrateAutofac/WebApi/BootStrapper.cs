using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;

namespace WebApi
{
    public class BootStrapper
    {
        private readonly ContainerBuilder containerBuilder = new ContainerBuilder();

        public void Init(HttpConfiguration configuration)
        {
            BuildRoute(configuration);
            BuildContainer(configuration);
        }

        public ContainerBuilder GetContainerBuilder()
        {
            return containerBuilder;
        }

        private void BuildContainer(HttpConfiguration configuration)
        {

            containerBuilder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            containerBuilder.RegisterType<MessageProducer>().InstancePerLifetimeScope();
            var container = containerBuilder.Build();

            configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private void BuildRoute(HttpConfiguration configuration)
        {
            configuration.Routes.MapHttpRoute("message-id", "message/{id}", new {controller = "Message"});
            configuration.Routes.MapHttpRoute("another-message", "another-message", new {controller = "Message"});
        }
    }
}