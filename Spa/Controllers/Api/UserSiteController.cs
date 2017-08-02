using DbConnector.Tools;
using Spa.Domain;
using Spa.Filter;
using Spa.Models.Requests;
using Spa.Models.Responses;
using Spa.Service;
using Spa.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;

namespace Spa.Controllers.Api
{
    [RoutePrefix("api/users")]
    public class UserSiteController : ApiController
    {
        IUserSiteService _userSiteService;

        public UserSiteController(IUserSiteService userSiteService)
        {
            _userSiteService = userSiteService;
        }

        [AllowAnonymous]
        [Route("registration"), HttpPost]
        public HttpResponseMessage Insert(UserSiteAddRequest model)
        {
            try
            {
                _userSiteService.Insert(new UserSiteAddRequest
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    Password = model.Password.EncryptString()
                });

                return Request.CreateResponse(HttpStatusCode.OK, "Registration Was A Success");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, ex.Message);
            }
        }

        [Route("login"), HttpPost]
        public HttpResponseMessage Login(LoginRequest model)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, _userSiteService.Login(model.Email));
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

    }
}
