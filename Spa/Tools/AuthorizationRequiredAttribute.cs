using DbConnector.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Spa.Tools
{
    public class AuthorizationRequiredAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (actionContext.Request.Headers.Contains("Token"))
            {
                string tokenVal = actionContext.Request.Headers.GetValues("Token").First();
                string hardcodedTokenCheck = tokenVal.DecryptString();
                string expire = actionContext.Request.Headers.GetValues("TokenExpire").First();
                var tokenExpire = DateTime.Parse(expire);

                if (string.IsNullOrEmpty(tokenVal))
                {
                    actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized) { ReasonPhrase = "No Token Found" };
                }
                if(tokenVal.DecryptString() != hardcodedTokenCheck)
                {
                    actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized) { ReasonPhrase = "Tokens do not match" };
                }
                if(Convert.ToDateTime(expire) <= DateTime.Now)
                {
                    actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized) { ReasonPhrase = "Token Expired" };
                }

            }
            else
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized) { ReasonPhrase = "Not Authorized" };
            }
            base.OnActionExecuting(actionContext);
        }
    }
}