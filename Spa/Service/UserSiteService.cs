using DbConnector.Adapter;
using DbConnector.Tools;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Spa.Domain;
using Spa.Models;
using Spa.Models.Requests;
using Spa.Models.Responses;
using Spa.Service.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Web;

namespace Spa.Service
{
    public class UserSiteService : IUserSiteService
    {
        IBaseService _baseService;

        public UserSiteService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public int Insert (UserSiteAddRequest model)
        {
            SqlParameter id = SqlDbParameter.Instance.BuildParam("@Id", 0, System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Output);
            _baseService.SqlAdapter.LoadObject<UserSite>(new DbCommandDef
            {
                DbCommandText = "dbo.SiteUser_Insert",
                DbCommandType = System.Data.CommandType.StoredProcedure,
                DbParameters = new SqlParameter[]
                {
                    new SqlParameter("@FirstName", model.FirstName),
                    new SqlParameter("@LastName", model.LastName),
                    new SqlParameter("@Email", model.Email),
                    new SqlParameter("@Password", model.Password),
                    id
                }
            });
            return id.Value.ToInt32();
        }

        public UserSite Login(string email)
        {
            return _baseService.SqlAdapter.LoadObject<UserSite>(new DbCommandDef
            {
                DbCommandText = "dbo.SiteUser_Select",
                DbCommandType = System.Data.CommandType.StoredProcedure,
                DbParameters = new SqlParameter[]
            {
                new SqlParameter("@Email", email)
            }

            }).FirstOrDefault();
        }

        public void InsertToken(TokenAddRequest model)
        {
            _baseService.SqlAdapter.ExecuteQuery(new DbCommandDef
            {
                DbCommandText = "dbo.SysTokens_Insert",
                DbCommandType = System.Data.CommandType.StoredProcedure,
                DbParameters = new SqlParameter[]
                {
                    new SqlParameter("@TokenGuid", model.TokenGuid),
                    new SqlParameter("@SiteUserId", model.SiteUserId),
                    new SqlParameter("@ExpireDate", model.ExpireDate)
                }
            });
        }

        //public ServiceResponse Login(string email, string password)
        //{
        //    try
        //    {
        //        bool result = false;

        //        if (!IsUser(email))
        //        {
        //            return new ServiceResponse { IsSuccessful = false, ResponseMessage = "Email was Not Found" };
        //        }

        //        ApplicationUserManager userManager = GetUserManager();
        //        IAuthenticationManager authenticationManager = HttpContext.Current.GetOwinContext().Authentication;

        //        //I think the tricky part is since we aren't using Identity Framework(I don't think we are?) and AspNetUser datatable,
        //        //we are going to have to figure out another way to select from usersite table by email and compare passwords
        //        ApplicationUser user = userManager.Find(email, password); 

        //        if (user != null)
        //        {
        //            ClaimsIdentity signin = userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
        //            authenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = true }, signin);
        //            return new ServiceResponse { IsSuccessful = true, ResponseMessage = "Success" };
        //        }
        //        return new ServiceResponse { IsSuccessful = result, ResponseMessage = "Password was Invalid" };
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}

        //private ApplicationUserManager GetUserManager()
        //{
        //    try
        //    {
        //        return HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}

        //public bool IsUser(string emailaddress)
        //{
        //    try
        //    {
        //        bool result = false;

        //        ApplicationUserManager userManager = GetUserManager();
        //        IAuthenticationManager authenticationManager = HttpContext.Current.GetOwinContext().Authentication;

        //        ApplicationUser user = userManager.FindByEmail(emailaddress);

        //        if (user != null)
        //        {
        //            result = true;
        //        }
        //        return result;
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}
    }
}