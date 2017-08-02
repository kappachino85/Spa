using Spa.Service.Interface;
using Spa.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Spa.Controllers.Api
{
    [AuthorizationRequired]
    [RoutePrefix("api/states")]
    public class StateProvinceController : ApiController
    {
        IStateProvinceService _stateProvinceService;
        public StateProvinceController(IStateProvinceService stateProvinceService)
        {
            _stateProvinceService = stateProvinceService;
        }

        [Route, HttpGet]
        public HttpResponseMessage GetAll()
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, _stateProvinceService.ReadAll());
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, ex.Message);
            }
        }

        [Route("{id:int}"), HttpGet]
        public HttpResponseMessage GetById(int id)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, _stateProvinceService.ReadById(id));
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}