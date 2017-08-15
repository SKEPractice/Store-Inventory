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
    public partial class frmBrand : Form
    {
        public frmBrand()
        {
            InitializeComponent();
        }
        BALBrand balBrand = new BALBrand();
        BALCategory balCategory = new BALCategory();
             
        private void txtBrandName_TextChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = balBrand.GetBrand(txtBrandName.Text);
            LoadGrid(dt);
        }

        private void frmBrand_Load(object sender, EventArgs e)
        {
            LoadCboCategory();
        }
        private void LoadCboCategory()
        {
            DataTable dt = new DataTable();
            dt = balCategory.GetAllCategory(string.Empty);
            DataRow dr = dt.NewRow();
            dr["CategoryName"] = "- -Please Select- -";
            dt.Rows.InsertAt(dr, 0);
            cboCategory.DataSource = dt;
            cboCategory.DisplayMember = "CategoryName";
            cboCategory.ValueMember = "CategoryID";
        }
        //private void LoadCboCategory(string filterString)
        //{
        //    DataTable dt = new DataTable();
        //    dt = balCategory.GetAllCategory(filterString);
        //    cboCategory.DataSource = dt;
        //    cboCategory.DisplayMember = "CategoryName";
        //    cboCategory.ValueMember = "CategoryID";
        //}

        private void LoadGrid(DataTable dt)
        {
            this.dgvBrand.DataSource = null;
            this.dgvBrand.Rows.Clear();
            dgvBrand.Columns["colBrandName"].Visible = true;
            dgvBrand.Columns["colSN"].Visible = true;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dgvBrand.Rows.Add();
                dgvBrand.Rows[i].Cells["colBrandID"].Value = dt.Rows[i]["BrandID"].ToString();
                dgvBrand.Rows[i].Cells["colBrandName"].Value = dt.Rows[i]["BrandName"].ToString();
                dgvBrand.Rows[i].Cells["colSN"].Value = i;
                dgvBrand.Rows[i].Cells["colCategoryID"].Value = dt.Rows[i]["CategoryID"].ToString();
                dgvBrand.Rows[i].Cells["colCategoryName"].Value = dt.Rows[i]["CategoryName"].ToString();
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
                MessageBox.Show("Plese Provide Brand Name and Category", "Save Category", MessageBoxButtons.OK);
                return;
            }
            if (balBrand.AddBrand(txtBrandName.Text.Trim(),Convert.ToInt32(cboCategory.SelectedValue.ToString())))
            {
                MessageBox.Show("New Category Added", "Category Added", MessageBoxButtons.OK);
                LoadGrid(balBrand.GetAllBrand(string.Empty));
                this.txtBrandName.Text = string.Empty;
            } 
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (ValidateField())
            {
                MessageBox.Show("Plese select Brand", "Delete Category",MessageBoxButtons.OK);
                return;
            }
            if (balBrand.DeleteBrand(Convert.ToInt32(txtBrandID.Text.Trim())))
            {
                MessageBox.Show("Deleted Brand" + txtBrandName.Text, "Category Deleted", MessageBoxButtons.OK);
                LoadGrid(balBrand.GetAllBrand(string.Empty));
                this.txtBrandName.Text = string.Empty;
            }
        }

        private void btnGetAllBrand_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt=balBrand.GetAllBrand(string.Empty);
            LoadGrid(dt);
            this.txtBrandName.Text = string.Empty;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (ValidateField())
            {
                MessageBox.Show("Plese select Brand and Category First", "Update Category", MessageBoxButtons.OK);
                return;
            }
            //updating category 
            if (balBrand.UpdateBrand(txtBrandName.Text, Convert.ToInt32(txtBrandID.Text),Convert.ToInt32(cboCategory.SelectedValue.ToString())))
            {
                MessageBox.Show("Value Updated Successfully", "Update Category", MessageBoxButtons.OK);
                //reloading category values into grid
                LoadGrid(balBrand.GetAllBrand(string.Empty));
                this.txtBrandName.Text = string.Empty;
            }
           
        }

        private bool ValidateField()
        {
            if (txtBrandName.Text.Trim()==string.Empty|| cboCategory.SelectedIndex==0)
            {
                return true;
            }
            return false;
        }

        private void clear(object sender, EventArgs e)
        {
            cboCategory.SelectedIndex = 0;
            txtBrandID.Text = string.Empty;
            txtBrandName.Text = string.Empty;
            dgvBrand.Rows.Clear();
            dgvBrand.Refresh();
        }

        //load grid value to the text box when a row is slected from a data grid view
        private void dgvBrand_MouseClick(object sender, MouseEventArgs e)
        {
            if (dgvBrand.Rows.Count<=0)
            {
                return;
            }
            txtBrandID.Text = dgvBrand.CurrentRow.Cells["colBrandID"].Value.ToString();
            txtBrandName.Text = dgvBrand.CurrentRow.Cells["colBrandName"].Value.ToString();
            cboCategory.SelectedValue = dgvBrand.CurrentRow.Cells["colCategoryID"].Value.ToString();
            //LoadGrid(balBrand.GetAllBrand(string.Empty));
        }

        private void incButton1_Click(object sender, EventArgs e)
        {
            frmCategory categoryForm = new frmCategory();
            categoryForm.ShowDialog();
        }

        private void _CloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOpenCategory_Click(object sender, EventArgs e)
        {
            frmCategory categoryForm = new frmCategory();
            categoryForm.ShowDialog();
        }

        private void cboCategory_TextChanged(object sender, EventArgs e)
        {
            //DataTable dt = new DataTable();
            //dt=balBrand.GetAllBrandByCategory(Convert.ToInt32(cboCategory.SelectedValue.ToString()));
            //LoadGrid(dt);
        }
    }
}
