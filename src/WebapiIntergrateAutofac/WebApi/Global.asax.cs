using System;
using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;

namespace WebApi
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            BootStrapper.Init(GlobalConfiguration.Configuration);
        }
    }

    public class BootStrapper
    {
        public static void Init(HttpConfiguration configuration)
        {
            configuration.Routes.MapHttpRoute("message-id", "message/{id}", new {controller = "Message"});


            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType<MessageSaver>().InstancePerLifetimeScope();
            var container = builder.Build();

            configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}