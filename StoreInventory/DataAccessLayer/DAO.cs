using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using Common;

namespace DataAccessLayer
{
    public class DAO
    {
        //Retriving connection String from app.config
        public static AppSettingsReader  asr = new AppSettingsReader();
        
        public static SqlConnection GetConnection()
        {
            //SqlConnection con = new SqlConnection(asr.GetValue("StoreInventory", typeof(string)).ToString());
            SqlConnection con = new SqlConnection( DBConnectionString.ConnectionString);
            if (con.State !=ConnectionState.Open )
            {
                con.Open();
            }
            return con;
        }
        public static DataTable GetTable(string sql,SqlParameter[] pram, CommandType  cmdType)
        {
            using (SqlCommand cmd=new SqlCommand(sql,GetConnection()))
            {
                cmd.CommandType = cmdType;
                if (pram != null || pram.Length!=0 )
                {
                    cmd.Parameters.AddRange(pram);
                }
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                return dt;
            }         
        }
        public static int IUD(string sql, SqlParameter[] pram, CommandType cmdType)
        {
            using (SqlCommand cmd=new SqlCommand(sql,GetConnection ()))
            {
                cmd.CommandType = cmdType;
                if (pram.Length!=0)
                {
                    cmd.Parameters.AddRange(pram);
                }
                int i = cmd.ExecuteNonQuery();
                return i;
            }
        }
    }
}
