using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using DataAccessLayer;

namespace BussinessLayer
{
   public class BALPurchase
    {
        public DataTable GetPurchaseDetail()
        {
            string query = @"select PurchaseID,ProductID,VendorID,Price,Quantity,product.productName,vendor.vendorName
                            from Purchase 
                            inner join vendor on purchase.vendorID = vendor.VendorID
                            inner join product on purchase.productID = product.productID";
            return DAO.GetTable(query,null, CommandType.Text);
        }
        public bool AddPurchase(long vendorID,decimal totalAmount)
        {
            DateTime purchaseDate=DateTime.Today;
            SqlParameter[] pram = new SqlParameter[]
            {
                new SqlParameter("@vendorID",vendorID),
                new SqlParameter("@totalAmount",totalAmount),
                new SqlParameter("@purchaseDate",purchaseDate),
            };
            if (DAO.IUD("insert into Purchase values(@vendorID,@purchaseDate,@totalAmount)", pram, CommandType.Text) > 0)
            {
                return true;
            }
            return false;
        }
        public int LastPurchaseID()
        {
            DataTable dt = new DataTable();
            dt = DAO.GetTable("select top(1) purchaseID from Purchase order by purchaseID desc ", null, CommandType.Text);
            return Convert.ToInt32(dt.Rows[0]["PurchaseID"].ToString());
        }
        public bool AddPurchaseItem(int purchaseQuantity,decimal purchasePrice,long purchaseID,long productID)
        {
            SqlParameter[] pram = new SqlParameter[]
            {
                new SqlParameter("@purchaseQuantity",purchaseQuantity),
                new SqlParameter("@purchasePrice",purchasePrice),
                new SqlParameter("@purchaseID",purchaseID),
                new SqlParameter("@productID",productID)
            };
            if (DAO.IUD("insert into PurchaseDetails values(@purchaseQuantity,@purchasePrice,@purchaseID,@productID)", pram, CommandType.Text) > 0)
            {
                return true;
            }
            return false;
        }
        public bool DeletePurchase(int purchaseID)
        {
            SqlParameter[] pram = new SqlParameter[]
            {
                new SqlParameter("@purchaseID",purchaseID)
            };
            if (DAO.IUD("Delete from Purchase where purchaseID = @purchaseID", pram, CommandType.Text) > 0)
            {
                return true;
            }
            return false;
        }
        public bool UpdatePurchase(long purchaseID,long productID, Int32 vendorID, long productPrice, Int32 quantity)
        {
            SqlParameter[] pram = new SqlParameter[]
            {
                new SqlParameter("@purchaseID",purchaseID),
                new SqlParameter("@productID",productID),
                new SqlParameter("@vendorID",vendorID),
                new SqlParameter("@productPrice",productPrice),
                new SqlParameter("@quantity",quantity)
            };
            string query = @"update product set ProductID=@productID,VendorID=@vendorID,ProductPrice,productPrice";
            if (DAO.IUD("Update category set CategoryName=@categoryName where CategoryID=@categoryID", pram, CommandType.Text) > 0)
            {
                return true;
            }
            return false;
        }
    }
}
