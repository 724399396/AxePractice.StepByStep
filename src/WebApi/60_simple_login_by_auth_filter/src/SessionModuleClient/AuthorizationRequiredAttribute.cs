using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace SessionModuleClient
{
    public class AuthorizationRequiredAttribute : AuthorizationFilterAttribute
    {
        public override Task OnAuthorizationAsync(
            HttpActionContext actionContext, 
            CancellationToken cancellationToken)
        {
            #region Please implement the method

            /*
             * This authorization attribute will try checking if IPrincipal is valid.
             * If it is not valid, set the response to an unauthorized status. That
             * means, all users that is authenticated is allowd to access resources
             * annotated by this attribute.
             */

            ClaimsPrincipal principal = actionContext.RequestContext.Principal as ClaimsPrincipal;

            bool? success = principal?.Identities.SelectMany(i => i.Claims).Any(i => i.Type == "userFullName");

            if (!success.HasValue || !success.Value)
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            }
            return Task.CompletedTask;

            #endregion
        }
    }
}