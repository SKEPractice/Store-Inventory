using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BussinessLayer;

namespace StoreInventory
{
    public partial class frmVendor : Form
    {
        public frmVendor()
        {
            InitializeComponent();
        }

        BALCategory balCategory = new BALCategory();
           private void txtCategoryName_TextChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = balCategory.GetCategory(txtVendorName.Text);
            LoadCategoryGrid(dt);
        }

        private void LoadCategoryGrid(DataTable dt)
        {
            this.dgvVendor.DataSource = null;
            this.dgvVendor.Rows.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dgvVendor.Rows.Add();
                dgvVendor.Rows[i].Cells["colSN"].Value = i;
                dgvVendor.Rows[i].Cells["colCategoryID"].Value = dt.Rows[i]["CategoryID"].ToString();
                dgvVendor.Rows[i].Cells["colCategoryName"].Value = dt.Rows[i]["CategoryName"].ToString();
            }
        }

        private void closeButton_MouseLeave(object sender, EventArgs e)
        {
            closeButton.ForeColor = Color.White;
        }

        private void closeButton_MouseHover(object sender, EventArgs e)
        {
            closeButton.ForeColor = Color.Red;
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateField())
            {
                MessageBox.Show("Plese select Category First", "Save Category", MessageBoxButtons.OK);
                return;
            }
            if (balCategory.AddCategory(txtVendorName.Text))
            {
                MessageBox.Show("New Category Added", "Category Added", MessageBoxButtons.OK);
                LoadCategoryGrid(balCategory.GetCategory(string.Empty));
                this.txtVendorName.Text = string.Empty;
            } 
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (ValidateField())
            {
                MessageBox.Show("Plese select Category First", "Delete Category",MessageBoxButtons.OK);
                return;
            }
            if (balCategory.DeleteCategory(Convert.ToInt32(txtID.Text.Trim())))
            {
                MessageBox.Show("Deleted category" + txtVendorName.Text.Trim(), "Category Deleted", MessageBoxButtons.OK);
                LoadCategoryGrid(balCategory.GetCategory(string.Empty));
                this.txtVendorName.Text = string.Empty;
            }
        }

        private void btnGetAllCategory_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt=balCategory.GetCategory(string.Empty);
            LoadCategoryGrid(dt);
            this.txtVendorName.Text = string.Empty;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (ValidateField())
            {
                MessageBox.Show("Plese select Category First", "Update Category", MessageBoxButtons.OK);
                return;
            }
            //updating category 
            if (balCategory.UpadateCategory(txtVendorName.Text, Convert.ToInt32(txtID.Text)))
            {
                MessageBox.Show("Value Updated Successfully", "Update Category", MessageBoxButtons.OK);
                //reloading category values into grid
                LoadCategoryGrid(balCategory.GetCategory(string.Empty));
                this.txtVendorName.Text = string.Empty;
            }
           
        }

        private void dgvCategory_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            txtID.Text = dgvVendor.CurrentRow.Cells["colCategoryID"].Value.ToString();
            txtVendorName.Text = dgvVendor.CurrentRow.Cells["colCategoryName"].Value.ToString();
            LoadCategoryGrid(balCategory.GetCategory(string.Empty));
        }
        private bool ValidateField()
        {
            if (txtVendorName.Text.Trim()==string.Empty)
            {
                return true;
            }
            return false;
        }
    }
}
