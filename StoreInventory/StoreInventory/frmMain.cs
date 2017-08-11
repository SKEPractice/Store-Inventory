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
            categoryForm.Show();
            categoryForm.MdiParent = this;
        }
    }
}
