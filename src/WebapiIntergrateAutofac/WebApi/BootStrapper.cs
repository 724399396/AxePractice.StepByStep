using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;

namespace WebApi
{
    public class BootStrapper
    {
        public void Init(HttpConfiguration configuration, ContainerBuilder containerBuilder)
        {
            BuildRoute(configuration);
            BuildContainer(configuration, containerBuilder);
        }

        void BuildContainer(HttpConfiguration configuration, ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            containerBuilder.RegisterType<MessageProducer>().InstancePerLifetimeScope();
            IContainer container = containerBuilder.Build();

            configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        void BuildRoute(HttpConfiguration configuration)
        {
            configuration.Routes.MapHttpRoute("message-id", "message/{id}", new {controller = "Message"});
            configuration.Routes.MapHttpRoute("another-message", "another-message", new {controller = "Message"});
        }
    }
}