using Spa.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Spa.Filter
{
    public class GenericAuthenticationFilter : AuthorizationFilterAttribute
    {
        public GenericAuthenticationFilter() { }

        public virtual BasicAuthenticationIdentity FetchHeader(HttpActionContext context)
        {
            string authHeaderVal = null;
            System.Net.Http.Headers.AuthenticationHeaderValue authRequest = context.Request.Headers.Authorization;
            if(authRequest != null && !string.IsNullOrEmpty(authRequest.Scheme) && authRequest.Scheme == "Basic")
            {
                authHeaderVal = authRequest.Parameter;
            }

            if(string.IsNullOrEmpty(authHeaderVal)) return null;

            authHeaderVal = Encoding.Default.GetString(Convert.FromBase64String(authHeaderVal));
            string[] creds = authHeaderVal.Split(':');
            return creds.Length < 2 ? null : new BasicAuthenticationIdentity(creds[0], creds[1]);

        }

        private static void ChallengeAuthRequest(HttpActionContext context)
        {
            string dnsHost = context.Request.RequestUri.DnsSafeHost;
            context.Response = context.Request.CreateResponse(HttpStatusCode.Unauthorized);
            context.Response.Headers.Add("WWW-Authenticate", string.Format("Basic realm=\"{0}\"", dnsHost));
        }

        protected virtual bool OnAuthorizeUser(string userName, string password, HttpActionContext context)
        {
            if(string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                return false;
            }

            return true;
        }

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            BasicAuthenticationIdentity identity = FetchHeader(actionContext);
            if(identity == null)
            {
                ChallengeAuthRequest(actionContext);
                return;
            }
            GenericPrincipal gp = new GenericPrincipal(identity, null);
            Thread.CurrentPrincipal = gp;
            if(!OnAuthorizeUser(identity.UserName, identity.Password, actionContext))
            {
                ChallengeAuthRequest(actionContext);
                return;
            }
            base.OnAuthorization(actionContext);
        }
    }
}