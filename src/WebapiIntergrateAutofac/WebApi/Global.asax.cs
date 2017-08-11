using System;
using System.Web.Http;
using Autofac;

namespace WebApi
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            var bootStrapper = new BootStrapper();
            var container = new ContainerBuilder();
            container.RegisterType<Logger>().As<ILogger>();
            bootStrapper.Init(GlobalConfiguration.Configuration, container);
        }
    }
}