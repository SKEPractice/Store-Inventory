using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using System.Data;
using System.Data.SqlClient;

namespace BussinessLayer
{
   public class BALUser
    {
        DAO dataAccess = new DAO();
        public Int32 checkUser(string userName,string password,Int32 userType)
        {
            DataTable dt = new DataTable();
            SqlParameter[] pram = new SqlParameter[]
            {
                new SqlParameter("@userName",userName)
            };
            dt = DAO.GetTable("Select RoleID,UserName,Password,FirstName,LastName,IsActive from _user where userName=@userName", pram, CommandType.Text);
            if (dt.Rows.Count >0 && dt.Rows[0]["Password"].ToString()==password)
            {
                return Convert.ToInt32(dt.Rows[0]["RoleID"].ToString());
            }
            return 0; ;
        }
        public DataTable GetUserType(int i)
        {
            DataTable dt = new DataTable();
            SqlParameter[] pram = new SqlParameter[]
            {
                new SqlParameter("@isActive",i)
            };
            dt = DAO.GetTable("select RoleID,Name from [Role] where IsActive=@isActive", pram, CommandType.Text);
            return dt;
        }

    }
}
