using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using System.Data;

namespace BussinessLayer
{
    public class BALStock
    {
        public bool CheckStock(long productID)
        {
            SqlParameter[] pram = new SqlParameter[]
            {
                new SqlParameter("@productID",productID),
            };
            if (DAO.IUD("select StockID from Stock where ProductID=@productID", pram, CommandType.Text) > 0)
            {
                return true;
            }
            return false;
        }
        public DataTable GetStock(long productID)
        {
            SqlParameter[] pram = new SqlParameter[]
            {
                new SqlParameter("@productID",productID),
            };
            return DAO.GetTable("select sum(StockQuantity) as AvailableQuantity from stock where ProductID=@productID group by stockID ", pram, CommandType.Text);
        }
        public bool UpdateStock(long productID, Int32 stockQuantity)
        {
            DateTime stockModifiedon = DateTime.Today;
            SqlParameter[] pram = new SqlParameter[]
            {
                new SqlParameter("@productID",productID),
                new SqlParameter("@stockQuantity",stockQuantity),
                new SqlParameter("@stockModifiedon",stockModifiedon),
            };
            if (DAO.IUD("Update Stock set StockQuantity = @stockQuantity,StockModified = @stockModifiedon where ProductID=@productID", pram, CommandType.Text) > 0)
            {
                return true;
            }
            return false;
        }
        public bool AddStock(long productID, Int32 stockQuantity)
        {
            DateTime AddedOn = DateTime.Today;
            SqlParameter[] pram = new SqlParameter[]
            {
                new SqlParameter("@productID",productID),
                new SqlParameter("@stockQuantity",stockQuantity),
                new SqlParameter("@AddedOn",AddedOn),
            };
            if (DAO.IUD("Insert into Stock values(@productID,@stockQuantity,@AddedOn,null)", pram, CommandType.Text) > 0)
            {
                return true;
            }
            return false;
        }
    }
}
