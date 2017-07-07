using System;
using System.Web.Http;

namespace SimpleSolution.WebApp
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            Bootstrapper.Init(GlobalConfiguration.Configuration);
        }
    }
}