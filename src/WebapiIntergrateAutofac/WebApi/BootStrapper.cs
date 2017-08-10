using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;

namespace WebApi
{
    public class BootStrapper
    {
        public static void Init(HttpConfiguration configuration)
        {
            configuration.Routes.MapHttpRoute("message-id", "message/{id}", new {controller = "Message"});
            configuration.Routes.MapHttpRoute("another-message", "another-message", new {controller = "Message"});

            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType<MessageProducer>().InstancePerLifetimeScope();
            var container = builder.Build();

            configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}