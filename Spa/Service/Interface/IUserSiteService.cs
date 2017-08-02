using Spa.Domain;
using Spa.Models.Requests;
using Spa.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spa.Service.Interface
{
    public interface IUserSiteService
    {
        int Insert(UserSiteAddRequest model);

        UserSite Login(string email);

        void InsertToken(TokenAddRequest model);
    }
}
