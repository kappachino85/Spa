using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DbConnector.Adapter;
using DbConnector.Tools;
using System.Configuration;
using System.Data.SqlClient;
using Spa.Service.Interface;

namespace Spa.Service
{
    public class BaseService : IBaseService
    {
        string _connectionString;

        public BaseService()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        public IDbAdapter SqlAdapter
        {
            get
            {
                return new DbAdapter(new SqlCommand(), new SqlConnection(_connectionString));
            }
        }
    }
}