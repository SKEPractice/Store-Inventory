using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;

namespace BussinessLayer
{
    public class BALProduct
    {

        public DataTable GetAllProduct(string filterString)
        {
            DataTable dt = new DataTable();
            SqlParameter[] pram = new SqlParameter[]
            {
                new SqlParameter("@filterString","%"+filterString+"%")
            };
            string query = "select top(15) BrandID,BrandName,Category.CategoryID,Category.CategoryName from Brand inner join Category on Brand.CategoryID = Category.CategoryID where BrandName like @filterString";
            dt = DAO.GetTable(query, pram, CommandType.Text);
            return dt;
        }
        public bool AddBrand(string brandName, int categoryID)
        {
            SqlParameter[] pram = new SqlParameter[]
            {
                new SqlParameter("@brandName",brandName),
                new SqlParameter("@categoryID",categoryID)
            };
            if (DAO.IUD("insert into Brand values(@brandName,@categoryID)", pram, CommandType.Text) > 0)
            {
                return true;
            }
            return false;
        }
        public bool DeleteBrand(int brandID)
        {
            SqlParameter[] pram = new SqlParameter[]
            {
                new SqlParameter("@brandID",brandID)
            };
            if (DAO.IUD("Delete from Brand where brandID = @brandID", pram, CommandType.Text) > 0)
            {
                return true;
            }
            return false;
        }
        public bool UpdateBrand(string brandName, int brandID, int categoryID)
        {
            SqlParameter[] pram = new SqlParameter[]
            {
                new SqlParameter("@brandID",brandID),
                new SqlParameter("@brandName",brandName),
                new SqlParameter("@categoryID",categoryID)
            };
            if (DAO.IUD("Update Brand set brandName=@brandName,CategoryID=@categoryID where BrandID=@brandID", pram, CommandType.Text) > 0)
            {
                return true;
            }
            return false;
        }
    }
}
