using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;


namespace Common
{
    public class DBConnectionString
    {
        public static string ConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["StoreInventory"].ConnectionString;
            }
        }
    }
}
