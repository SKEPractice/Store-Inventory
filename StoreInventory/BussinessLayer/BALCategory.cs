using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using System.Data.SqlClient;

namespace BussinessLayer
{
    public class BALCategory
    {
        public DataTable GetCategory(string filterString)
        {
            DataTable dt = new DataTable();
            SqlParameter[] pram = new SqlParameter[]
            {
                new SqlParameter("@filterString","%"+filterString+"%")
            };
            dt=DAO.GetTable("select top(5) CategoryID,CategoryName from Category where CategoryName like @filterString order by CategoryID desc", pram, CommandType.Text);
            return dt;
        }
        public DataTable GetAllCategory(String filterString)
        {
            DataTable dt = new DataTable();
            SqlParameter[] pram=new SqlParameter[]{
                new SqlParameter("@filterString",filterString)
            };
            dt = DAO.GetTable("select CategoryID,CategoryName from Category order by CategoryName asc", pram, CommandType.Text);
            return dt;
        }
        public bool AddCategory(string categoryName)
        {
            SqlParameter[] pram = new SqlParameter[]
            {
                new SqlParameter("@categoryName",categoryName)
            };
            if (DAO.IUD("insert into category values(@categoryName)", pram, CommandType.Text) > 0)
            {
                return true;
            }
            return false;
        }
        public bool DeleteCategory(int categoryID)
        {
            SqlParameter[] pram = new SqlParameter[]
            {
                new SqlParameter("@categoryID",categoryID)
            };
            if (DAO.IUD("Delete from Category where categoryID = @categoryID", pram, CommandType.Text) > 0)
            {
                return true;
            }
            return false;
        }
        public bool UpadateCategory(string categoryName,int categoryID)
        {
            SqlParameter[] pram = new SqlParameter[]
            {
                new SqlParameter("@categoryName",categoryName),
                new SqlParameter("@categoryID",categoryID)
            };
            if (DAO.IUD("Update category set CategoryName=@categoryName where CategoryID=@categoryID", pram, CommandType.Text)>0)
            {
                return true;
            }
            return false;
        }
    }
}
