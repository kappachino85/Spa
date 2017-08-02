using DbConnector.Tools;
using Spa.Domain;
using Spa.Filter;
using Spa.Models.Requests;
using Spa.Service;
using Spa.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web;
using System.Web.Helpers;
using System.Web.Http;

namespace Spa.Controllers.Api
{
    [ApiAuthenticationFilter]
    [RoutePrefix("api/authenticate")]
    public class AuthenticationController : ApiController
    {
        IUserSiteService _userSiteService = new UserSiteService(new BaseService());

        [Route("login"), HttpPost]
        public HttpResponseMessage Authenticate()
        {
            if(Thread.CurrentPrincipal != null && Thread.CurrentPrincipal.Identity.IsAuthenticated)
            {
                BasicAuthenticationIdentity identity = Thread.CurrentPrincipal.Identity as BasicAuthenticationIdentity;
                
                if(identity != null)
                {
                   int SiteUserId = identity.UserId;
                   return GetAuthToken(SiteUserId);
                }
                
            }
            return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "No Token Created");
        }

        private HttpResponseMessage GetAuthToken(int userId)
        {
            IUserSiteService _userSiteService = new UserSiteService(new BaseService());
            TokenAddRequest TokenObject = new TokenAddRequest();
            TokenObject.TokenGuid = Guid.NewGuid();
            TokenObject.ExpireDate = DateTime.Now.AddMinutes(60);
            TokenObject.SiteUserId = userId;

            _userSiteService.InsertToken(TokenObject);

            HttpResponseMessage response = Request.CreateResponse(System.Net.HttpStatusCode.OK, "Authorized");

            response.Headers.Add("Token", TokenObject.TokenGuid.ToString().EncryptString());
            response.Headers.Add("TokenExpire", TokenObject.ExpireDate.ToString());
            response.Headers.Add("Access-Control-Expose-Headers", "Token, TokenExpire");

            return response;
        }
    }
}