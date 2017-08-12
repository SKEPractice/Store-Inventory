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
    public class BALVendor
    {
        public DataTable GetAllVendor(string filterString)
        {
            SqlParameter[] pram = new SqlParameter[]
            {
                new SqlParameter("@filterString","%"+filterString+"%")
            };
            string query = @"select VendorID,VendorName,VendorAddress,VendorPhone,VendorEmail from Vendor
                            where VendorName like @filterString";
            return DAO.GetTable(query, pram, CommandType.Text);
        }
        public bool AddVendor(string vendorName, string vendorAddress,string vendorPhone, string vendorEmail)
        {
            SqlParameter[] pram = new SqlParameter[]
            {
                new SqlParameter("@vendorName",vendorName),
                new SqlParameter("@vendorAddress",vendorAddress),
                new SqlParameter("@vendorPhone",vendorPhone),
                new SqlParameter("@vendorEmail",vendorEmail)
            };
            if (DAO.IUD("insert into Vendor values(@vendorName,@vendorAddress,@vendorPhone,@vendorEmail)", pram, CommandType.Text) > 0)
            {
                return true;
            }
            return false;
        }
        public bool DeleteVendor(int vendorID)
        {
            SqlParameter[] pram = new SqlParameter[]
            {
                new SqlParameter("@vendorID",vendorID)
            };
            if (DAO.IUD("Delete from Vendor where vendorID = @vendorID", pram, CommandType.Text) > 0)
            {
                return true;
            }
            return false;
        }
        public bool UpdateVendor(long vendorID, string vendorName, string vendorAddress, string vendorPhone, string vendorEmail)
        {
            SqlParameter[] pram = new SqlParameter[]
            {
                new SqlParameter("@vendorID",vendorID),
                new SqlParameter("@vendorName",vendorName),
                new SqlParameter("@vendorAddress",vendorAddress),
                new SqlParameter("@vendorPhone",vendorPhone),
                new SqlParameter("@vendorEmail",vendorEmail)
            };
            string query = @"Update Vendor set vendorName=@vendorName,vendorAddress=@vendorAddress,VendorPhone=@vendorPhone,
                            VendorEmail=@vendorEmail where VendorID=@vendorID";
            if (DAO.IUD(query, pram, CommandType.Text) > 0)
            {
                return true;
            }
            return false;
        }
    }
}
