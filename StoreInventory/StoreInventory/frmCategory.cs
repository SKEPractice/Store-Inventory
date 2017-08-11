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
    public partial class frmCategory : Form
    {
        public frmCategory  ()
        {
            InitializeComponent();
        }

        BALCategory balCategory = new BALCategory();
           private void txtCategoryName_TextChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = balCategory.GetCategory(txtCategoryName.Text);
            LoadCategoryGrid(dt);
        }

        private void LoadCategoryGrid(DataTable dt)
        {
            this.dgvCategory.DataSource = null;
            this.dgvCategory.Rows.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dgvCategory.Rows.Add();
                dgvCategory.Rows[i].Cells["colSN"].Value = i;
                dgvCategory.Rows[i].Cells["colCategoryID"].Value = dt.Rows[i]["CategoryID"].ToString();
                dgvCategory.Rows[i].Cells["colCategoryName"].Value = dt.Rows[i]["CategoryName"].ToString();
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
            Application.Exit();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateField())
            {
                MessageBox.Show("Plese select Category First", "Save Category", MessageBoxButtons.OK);
                return;
            }
            if (balCategory.AddCategory(txtCategoryName.Text))
            {
                MessageBox.Show("New Category Added", "Category Added", MessageBoxButtons.OK);
                LoadCategoryGrid(balCategory.GetCategory(string.Empty));
                this.txtCategoryName.Text = string.Empty;
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
                MessageBox.Show("Deleted category" + txtCategoryName.Text.Trim(), "Category Deleted", MessageBoxButtons.OK);
                LoadCategoryGrid(balCategory.GetCategory(string.Empty));
                this.txtCategoryName.Text = string.Empty;
            }
        }

        private void btnGetAllCategory_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt=balCategory.GetCategory(string.Empty);
            LoadCategoryGrid(dt);
            this.txtCategoryName.Text = string.Empty;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (ValidateField())
            {
                MessageBox.Show("Plese select Category First", "Update Category", MessageBoxButtons.OK);
                return;
            }
            //updating category 
            if (balCategory.UpadateCategory(txtCategoryName.Text, Convert.ToInt32(txtID.Text)))
            {
                MessageBox.Show("Value Updated Successfully", "Update Category", MessageBoxButtons.OK);
                //reloading category values into grid
                LoadCategoryGrid(balCategory.GetCategory(string.Empty));
                this.txtCategoryName.Text = string.Empty;
            }
           
        }

        private void dgvCategory_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            txtID.Text = dgvCategory.CurrentRow.Cells["colCategoryID"].Value.ToString();
            txtCategoryName.Text = dgvCategory.CurrentRow.Cells["colCategoryName"].Value.ToString();
            LoadCategoryGrid(balCategory.GetCategory(string.Empty));
        }
        private bool ValidateField()
        {
            if (txtCategoryName.Text.Trim()==string.Empty)
            {
                return true;
            }
            return false;
        }
    }
}
