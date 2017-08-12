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
    public partial class frmProduct : Form
    {
        public frmProduct()
        {
            InitializeComponent();
        }
        BALBrand balBrand = new BALBrand();
        BALCategory balCategory = new BALCategory();
        BALProduct balProduct = new BALProduct();
   
        private void _CloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmProduct_Load(object sender, EventArgs e)
        {
            DataTable dtCategory = new DataTable();
            DataTable dtBrand = new DataTable();
            dtCategory = balCategory.GetAllCategory(string.Empty);
            DataRow drCategory = dtCategory.NewRow();
            drCategory["CategoryName"] = "- - Please Select - -";
            dtCategory.Rows.InsertAt(drCategory, 0);
            cboCategory.DataSource = dtCategory;
            cboCategory.ValueMember = "CategoryID";
            cboCategory.DisplayMember = "CategoryName";
        }

        private void cboCategory_TextChanged(object sender, EventArgs e)
        {
            DataTable dtCategory = new DataTable();
            dtCategory = balCategory.GetAllCategory(cboCategory.Text);
            DataRow dr = dtCategory.NewRow();
            dr["CategoryName"] = "--Please Select--";
            dtCategory.Rows.InsertAt(dr, 0);
        }

        private void cboCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboCategory.SelectedIndex !=0)
            {
                //retriving all values from brand to populate it into combo box
                DataTable dtBrand = new DataTable();
                dtBrand = balBrand.GetAllBrand(string.Empty,Convert.ToInt32 (cboCategory.SelectedValue.ToString()));
                DataRow drBrand = dtBrand.NewRow();
                drBrand["BrandName"] = "- - Please Select - -";
                dtBrand.Rows.InsertAt(drBrand, 0);
                cboBrand.DataSource = dtBrand;
                cboBrand.ValueMember = "BrandID";
                cboBrand.DisplayMember = "BrandName";
            }
            else
            {
                cboBrand.DataSource = null;
                DataTable dtBrand = new DataTable();
                dtBrand.Columns.Add("BrandName");
                DataRow drBrand = dtBrand.NewRow();
                drBrand["BrandName"] = "- - Please Select - -";
                dtBrand.Rows.InsertAt(drBrand, 0);
                cboBrand.DataSource = dtBrand;
                cboBrand.DisplayMember = "BrandName";
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearControls();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateField())
            {
                string productFeatures= dgvFeatures.Rows[0].Cells["colFeaturesList"].Value.ToString();
                for (int i = 1; i < dgvFeatures.Rows.Count-1; i++)
                {
                    productFeatures = productFeatures+"|"+ dgvFeatures.Rows[i].Cells["colFeaturesList"].Value.ToString();
                }
                if (balProduct.AddProduct(txtProductName.Text.Trim(), Convert.ToDouble(txtPrice.Text), Convert.ToInt32(cboBrand.SelectedValue.ToString()), productFeatures))
                {
                    MessageBox.Show("New product added Successfully", "Product added", MessageBoxButtons.OK);
                    ClearControls();
                    LoadGridProduct(balProduct.GetAllProduct(string.Empty));
                } 
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //retriving features from Grid
            string productFeatures = dgvFeatures.Rows[0].Cells["colFeaturesList"].Value==null?string.Empty: dgvFeatures.Rows[0].Cells["colFeaturesList"].Value.ToString();
            for (int i = 1; i < dgvFeatures.Rows.Count-1; i++)
            {
                productFeatures = productFeatures + "|" + dgvFeatures.Rows[i].Cells["colFeaturesList"].Value.ToString();
            }
            Int32 productID=Convert.ToInt32 (txtProductID.Text);
            double productPrice = Convert.ToDouble(txtPrice.Text);
            Int32 brandID = Convert.ToInt32(cboBrand.SelectedValue.ToString());
            if (balProduct.UpdateProduct(productID, txtProductName.Text, productPrice, brandID, productFeatures))
            {
                MessageBox.Show("Product Detail Updated Successfully", "Update Product", MessageBoxButtons.OK);
                ClearControls();
                LoadGridProduct(balProduct.GetAllProduct(string.Empty));
            }
            {

            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult check;
            check=MessageBox.Show("Are you sure you want to Delete", "Delete Varification", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (check==DialogResult.No)
            {
                return;
            }
            if (balProduct.DeleteProduct(Convert.ToInt32(txtProductID.Text)))
            {
                MessageBox.Show("Product Deleted Successfully", "Delete Product", MessageBoxButtons.OK);
                ClearControls();
                LoadGridProduct(balProduct.GetAllProduct(string.Empty));
            }
        }

        private void btnGetAllBrand_Click(object sender, EventArgs e)
        {
            DataTable dtProduct = new DataTable();
            dtProduct = balProduct.GetAllProduct(string.Empty);
            LoadGridProduct(dtProduct);
        }

        private void ClearControls()
        {
            erpGeneral.Clear();
            dgvProduct.DataSource = null;
            dgvFeatures.DataSource = null;
            dgvFeatures.Rows.Clear();
            dgvProduct.Rows.Clear();
           // dgvFeatures.ClearSelection();
            Action<Control.ControlCollection> func = null;

            func = (controls) =>
            {
                foreach (Control control in controls)
                {
                    if (control is TextBox)
                    {
                        (control as TextBox).Clear();
                    }
                    else if (control is ComboBox)
                    {
                        (control as ComboBox).SelectedIndex = 0;
                    }
                    else
                    {
                        func(control.Controls);
                    }
                }
                    
            };

            func(Controls);
        }

        private void btnAddCategory_Click(object sender, EventArgs e)
        {
            frmCategory categoryForm = new frmCategory();
            categoryForm.ShowDialog();
        }

        private void btnAddBrand_Click(object sender, EventArgs e)
        {
            frmBrand brandForm = new frmBrand();
            brandForm.ShowDialog();
        }

        private void txtPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            //only allows a single decimal
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }

        }
        private bool  ValidateField()
        {
            if (cboCategory.SelectedIndex == 0)
            {
                cboCategory.Focus();
                erpGeneral.SetError(cboCategory, "Please Select Category");
                return true;
            }
            else if (cboBrand.SelectedIndex==0)
            {
                cboBrand.Focus();
                erpGeneral.SetError(cboBrand, "Please Select Brand");
                return true;
            }
            else if (txtProductName.Text.Trim()==string.Empty)
            {
                txtProductName.Focus();
                erpGeneral.SetError(txtProductName, "Please provide product Name");
                return true;
            }
            else if (txtPrice.Text.Trim()==string.Empty)
            {
                txtPrice.Focus();
                erpGeneral.SetError(txtPrice, "Please provide price");
                return true;
            }
            else if (dgvFeatures.Rows.Count-1<=0 )
            {
                dgvFeatures.Focus();
                erpGeneral.SetError(dgvFeatures, "Please provide at least one feature");
                return true;
            }
            else
            {
                return false;
            }
        }

        private void txtProductName_TextChanged(object sender, EventArgs e)
        {
            DataTable dtProduct = new DataTable();
            dtProduct = balProduct.GetAllProduct(txtProductName.Text);
            LoadGridProduct(dtProduct);
        }

        private void LoadGridProduct(DataTable dtProduct)
        {
            dgvProduct.Visible = true;
            dgvProduct.DataSource = null;
            dgvProduct.Rows.Clear();
            for (int i = 0; i < dtProduct.Rows.Count; i++)
            {
                dgvProduct.Rows.Add();
                dgvProduct.Rows[i].Cells["colSN"].Value = i;
                dgvProduct.Rows[i].Cells["colProductID"].Value = dtProduct.Rows[i]["ProductID"].ToString();
                dgvProduct.Rows[i].Cells["colProductName"].Value = dtProduct.Rows[i]["ProductName"].ToString();
                dgvProduct.Rows[i].Cells["colPrice"].Value = dtProduct.Rows[i]["price"].ToString();
                dgvProduct.Rows[i].Cells["colFeatures"].Value = dtProduct.Rows[i]["features"].ToString();
                dgvProduct.Rows[i].Cells["colBrandID"].Value = dtProduct.Rows[i]["BrandID"].ToString();
                dgvProduct.Rows[i].Cells["colBrandName"].Value = dtProduct.Rows[i]["BrandName"].ToString();
                dgvProduct.Rows[i].Cells["colCategoryID"].Value = dtProduct.Rows[i]["CategoryID"].ToString();
                dgvProduct.Rows[i].Cells["colCategoryName"].Value = dtProduct.Rows[i]["CategoryName"].ToString();
            }
        }

        private void dgvProduct_Click(object sender, EventArgs e)
        {
            if (dgvProduct.Rows.Count<=0)
            {
                return;
            }
            string features = dgvProduct.CurrentRow.Cells["colFeatures"].Value.ToString();
            string[] featuresList= features.Split('|');
            dgvFeatures.Rows.Clear();
            dgvFeatures.DataSource = null;
            for (int i = 0; i < featuresList.Length; i++)
            {
                dgvFeatures.Rows.Add();
                dgvFeatures.Rows[i].Cells["colFeaturesList"].Value = featuresList[i];
            }
            txtProductID.Text = dgvProduct.CurrentRow.Cells["colProductID"].Value.ToString();
            txtPrice.Text = dgvProduct.CurrentRow.Cells["colPrice"].Value.ToString();
            cboCategory.SelectedValue = dgvProduct.CurrentRow.Cells["colCategoryID"].Value;
            cboBrand.SelectedValue = dgvProduct.CurrentRow.Cells["colBrandID"].Value;
            txtProductName.Text = dgvProduct.CurrentRow.Cells["colProductName"].Value.ToString();
        }
    }
}

