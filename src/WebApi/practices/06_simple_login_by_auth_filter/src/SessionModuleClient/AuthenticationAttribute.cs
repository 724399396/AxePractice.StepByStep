using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Filters;
using Newtonsoft.Json;

namespace SessionModuleClient
{
    public class AuthenticationAttribute : Attribute, IAuthenticationFilter
    {
        const string SessionCookieKey = "X-Session-Token";

        public bool AllowMultiple { get; } = false;

        public bool RedirectToLoginOnChallenge { get; set; }

        public async Task AuthenticateAsync(
            HttpAuthenticationContext context,
            CancellationToken cancellationToken)
        {
            #region Please implement the following method

            /*
             * We need to create IPrincipal from the authentication token. If
             * we can retrive user session, then the structure of the IPrincipal
             * should be in the following form:
             * 
             * ClaimsPrincipal
             *   |- ClaimsIdentity (Primary)
             *        |- Claim: { key: "token", value: "$token value$" }
             *        |- Claim: { key: "userFullName", value: "$user full name$" }
             * 
             * If user session cannot be retrived, then the context principal
             * should be an empty ClaimsPrincipal (unauthenticated).
             */
            var authorization = context.Request.Headers.GetCookies(SessionCookieKey).FirstOrDefault();

            if (authorization == null)
            {
                context.Principal = new ClaimsPrincipal();
                return;
            }

            var token = authorization[SessionCookieKey].Value;

            var httpClient = context.Request.GetDependencyScope().GetService(typeof(HttpClient)) as HttpClient;

            Uri requestUri = context.Request.RequestUri;
            var request = new HttpRequestMessage(HttpMethod.Get, $"{requestUri.Scheme}://{requestUri.UserInfo}{requestUri.Authority}/session/{token}");

            HttpResponseMessage httpResponseMessage = await httpClient.SendAsync(request, cancellationToken);

            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                return;
            }

            var response = JsonConvert.DeserializeAnonymousType(await httpResponseMessage.Content.ReadAsStringAsync(), new
            {
                token = default(string),
                userFullname = default(string),
            });

            var principal = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim("token", response.token),
                new Claim("userFullName", response.userFullname), 
            }));
            context.Principal = principal;

            #endregion
        }

        public async Task ChallengeAsync(
            HttpAuthenticationChallengeContext context,
            CancellationToken cancellationToken)
        {
            #region Please implement the following method

            /*
             * The challenge method will try checking the configuration of
             * RedirectToLoginOnChallenge property. If the value is true,
             * then it will replace the response to redirect to login page.
             * And if the value is false, then simply keeps the original
             * response.
             */

            if (RedirectToLoginOnChallenge)
            {
                context.Result = new RedirectToLoginPageIfUnauthorizedResult(context.Request, context.Result);
            }

            #endregion
        }
    }
}