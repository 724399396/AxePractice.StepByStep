using System;
using System.Web.Http;

namespace WebApi
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            BootStrapper.Init(GlobalConfiguration.Configuration);
        }
    }
}