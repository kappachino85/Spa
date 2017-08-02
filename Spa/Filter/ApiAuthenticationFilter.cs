using Spa.Domain;
using Spa.Service;
using Spa.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Http.Controllers;
using DbConnector.Tools;

namespace Spa.Filter
{
    public class ApiAuthenticationFilter : GenericAuthenticationFilter
    {
        protected override bool OnAuthorizeUser(string email, string password, HttpActionContext context)
        {
            IUserSiteService _userSiteService = new UserSiteService(new BaseService());

            UserSite login = _userSiteService.Login(email);

            string pass = EncryptDecrypt.DecryptString(login.Password);

            if (login.Email == email && pass == password)
            {
                BasicAuthenticationIdentity basicIdentity = Thread.CurrentPrincipal.Identity as BasicAuthenticationIdentity;
                if (basicIdentity != null)
                {
                    basicIdentity.UserId = login.Id;
                    basicIdentity.FullName = login.FirstName + ' ' + login.LastName;
                }
                return true;
            }
            return false;
        }

        //protected override bool OnAuthorizeUser(string userName, string password, HttpActionContext context)
        //{
        //    IUserSiteService _userSiteService = new UserSiteService(new BaseService());

        //    if (userName == "jaephizzle" && password == "Pqwerty12!")
        //    {
        //        BasicAuthenticationIdentity basicIdentity = Thread.CurrentPrincipal.Identity as BasicAuthenticationIdentity;
        //        if (basicIdentity != null)
        //        {
        //            basicIdentity.UserId = 2;
        //            basicIdentity.FullName = "Jae Phizzle30";
        //        }
        //        return true;
        //    }
        //    return false;
        //}
    }
}