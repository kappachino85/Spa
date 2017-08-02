using DbConnector.Adapter;
using Spa.Domain;
using Spa.Service.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Spa.Service
{
    public class StateProvinceService : IStateProvinceService
    {
        IBaseService _baseService;

        public StateProvinceService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public IEnumerable<StateProvince> ReadAll()
        {
            return _baseService.SqlAdapter.LoadObject<StateProvince>(new DbCommandDef
            {
                DbCommandText = "dbo.BlogCategory_SelectAll",
                DbCommandType = System.Data.CommandType.StoredProcedure
            });
        }

        public StateProvince ReadById(int id)
        {
            return _baseService.SqlAdapter.LoadObject<StateProvince>(new DbCommandDef
            {
                DbCommandText = "dbo.StateProvince_SelectById",
                DbCommandType = System.Data.CommandType.StoredProcedure,
                DbParameters = new SqlParameter[]
                {
                    new SqlParameter("@Id", id)
                }

            }).FirstOrDefault();
        }
    }
}