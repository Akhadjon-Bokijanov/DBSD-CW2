using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace DBSD_CW2_7510_8775_7912.DAL
{
    public class DbConnection
    {
        protected SqlConnection Connection { 
            get 
            {
                return new SqlConnection(ConfigurationManager.ConnectionStrings["connection_1"].ConnectionString);
            } 
        }
        
    }
}