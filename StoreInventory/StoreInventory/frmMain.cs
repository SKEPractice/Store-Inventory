using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StoreInventory
{
    public partial class frmMain : BlackForm
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void addCategoryToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            frmCategory categoryForm = new frmCategory();
            categoryForm.ShowDialog();
            categoryForm.MdiParent = this;
        }

        private void addBrandToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmBrand brandForm = new frmBrand();
            brandForm.ShowDialog();
            brandForm.MdiParent = this;
        }

        private void addProductToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmProduct productForm = new frmProduct();
            productForm.ShowDialog();
            productForm.MdiParent = this;
        }

        private void vendorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmVendor vendorForm = new frmVendor();
            vendorForm.ShowDialog();
           // vendorForm.WindowState = vendorForm.MaximumSize();
            vendorForm.MdiParent = this;
        }

        private void purchaseToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmPurchase purchaseForm = new frmPurchase();
            //purchaseForm.MdiParent = this;
            purchaseForm.ShowDialog();
        }

        private void calculatorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("Calc.exe");
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("Calc.exe");
        }

        private void saleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSales salesForm = new frmSales();
            salesForm.ShowDialog();
            salesForm.MdiParent = this;
        }
    }
}
