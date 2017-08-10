using System;
using System.Web.Http;
using Autofac;
using Test;

namespace WebApi
{
    public class Global : System.Web.HttpApplication
    {
        private BootStrapper bootStrapper;

        public Global(BootStrapper bootStrapper)
        {
            this.bootStrapper = bootStrapper;
        }

        protected void Application_Start(object  sender, EventArgs e)
        {
            bootStrapper.GetContainerBuilder().RegisterType<Logger>().As<ILogger>();
            bootStrapper.Init(GlobalConfiguration.Configuration);
        }
    }
}