using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Newtonsoft.Json;

namespace SessionModuleClient
{
    public class LoginRequiredAttribute : ActionFilterAttribute
    {
        public override bool AllowMultiple { get; } = false;

        private const string SessionCookieKey = "X-Session-Token";

        public override async Task OnActionExecutingAsync(
            HttpActionContext context, 
            CancellationToken cancellationToken)
        {
            #region Please implement the method

            // This filter will try resolve session cookies. If the cookie can be
            // parsed correctly, then it will try calling session API to get the
            // specified session. To ease user session access, it will store the
            // session object in request message properties.

            CookieHeaderValue cookieHeaderValue = context.Request.Headers.GetCookies("X-Session-Token").FirstOrDefault();
            if (cookieHeaderValue == null)
            {
                context.Response = context.Request.CreateResponse(HttpStatusCode.Forbidden);
                return;
            }

            var httpClient = context.Request.GetDependencyScope().GetService(typeof(HttpClient)) as HttpClient;

            var request = new HttpRequestMessage(HttpMethod.Get, "http://" + context.Request.RequestUri.Host + $"/session/{cookieHeaderValue[SessionCookieKey].Value}");

            HttpResponseMessage httpResponseMessage = await httpClient.SendAsync(request, cancellationToken);

            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                context.Response = context.Request.CreateResponse(HttpStatusCode.Forbidden);
                return;
            }
            var content = await httpResponseMessage.Content.ReadAsStringAsync();
            context.Request.Properties["user"] = JsonConvert.DeserializeAnonymousType(
                content, new
            {
                userFullname = default(string)
            }).userFullname;

            #endregion
        }
    }
}