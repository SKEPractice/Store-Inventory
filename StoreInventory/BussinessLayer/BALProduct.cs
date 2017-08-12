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
            string query = string.Empty;
            query = @"select top(15) productID,productName,price,features,brand.brandID,BrandName,category.categoryID,categoryName 
                    from Product
                    inner join Brand on Product.BrandID=Brand.BrandID
                    inner join Category on Brand.CategoryID = Category.CategoryID
                    where BrandName like @filterString";
            dt = DAO.GetTable(query, pram, CommandType.Text);
            return dt;
        }
        public bool AddProduct(string productName, double price, int brandID, String Features)
        {
            SqlParameter[] pram = new SqlParameter[]
            {
                new SqlParameter("@productName",productName),
                new SqlParameter("@brandID",brandID),
                new SqlParameter("@price",price),
                new SqlParameter("@features",Features)
            };
            if (DAO.IUD("insert into Product values(@productName,@brandID,@price,@features)", pram, CommandType.Text) > 0)
            {
                return true;
            }
            return false;
        }
        public bool DeleteProduct(int productID)
        {
            SqlParameter[] pram = new SqlParameter[]
            {
                new SqlParameter("@productID",productID)
            };
            if (DAO.IUD("Delete from Product where ProductID = @ProductID", pram, CommandType.Text) > 0)
            {
                return true;
            }
            return false;
        }
        public bool UpdateProduct(long productID, string productName, double price, int brandID, String Features)
        {
            SqlParameter[] pram = new SqlParameter[]
            {
                new SqlParameter("productID",productID),
                new SqlParameter("@productName",productName),
                new SqlParameter("@brandID",brandID),
                new SqlParameter("@price",price),
                new SqlParameter("@features",Features)
            };
            if (DAO.IUD("Update Product set productName=@productName,BrandID=@brandID,price=@price,features=@features where productID=@productID", pram, CommandType.Text) > 0)
            {
                return true;
            }
            return false;
        }
    }
}
