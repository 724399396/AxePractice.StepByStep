using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;

namespace WebApi
{
    public class BootStrapper
    {
        public IContainer Init(HttpConfiguration configuration)
        {
            BuildRoute(configuration);
            return BuildContainer(configuration);
        }

        IContainer BuildContainer(HttpConfiguration configuration)
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            containerBuilder.RegisterType<MessageProducer>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<Logger>().As<ILogger>();

            IContainer container = containerBuilder.Build();

            configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            return container;
        }

        void BuildRoute(HttpConfiguration configuration)
        {
            configuration.Routes.MapHttpRoute("message-id", "message/{id}", new {controller = "Message"});
            configuration.Routes.MapHttpRoute("another-message", "another-message", new {controller = "Message"});
        }
    }
}