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
            bootStrapper.Init(GlobalConfiguration.Configuration);
        }
    }
}