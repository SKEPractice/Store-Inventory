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
            Application.Exit();
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
                    productFeatures = productFeatures+"|"+ dgvFeatures.Rows[i].Cells["colFeaturesList"].ToString();
                }
                if (balProduct.AddProduct(txtProductName.Text.Trim(), Convert.ToDouble(txtPrice.Text), Convert.ToInt32(cboBrand.SelectedValue.ToString()), productFeatures))
                {
                    MessageBox.Show("New product added Successfully", "Product added", MessageBoxButtons.OK);
                    ClearControls();
                } 
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //retriving features from Grid
            string productFeatures = dgvFeatures.Rows[0].Cells["colFeaturesList"].Value==null?string.Empty: dgvFeatures.Rows[0].Cells["colFeaturesList"].Value.ToString();
            for (int i = 1; i < dgvFeatures.Rows.Count-1; i++)
            {
                productFeatures = productFeatures + "|" + dgvFeatures.Rows[i].Cells["colFeaturesList"].ToString();
            }
            Int32 productID=Convert.ToInt32 (txtProductID.Text);
            double productPrice = Convert.ToDouble(txtPrice.Text);
            Int32 brandID = Convert.ToInt32(cboBrand.SelectedValue.ToString());
            if (balProduct.UpdateProduct(productID, txtProductName.Text, productPrice, brandID, productFeatures))
            {
                MessageBox.Show("Product Detail Updated Successfully", "Update Product", MessageBoxButtons.OK);
                ClearControls();
            }
            {

            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }

        private void btnGetAllBrand_Click(object sender, EventArgs e)
        {

        }

        private void ClearControls()
        {
            erpGeneral.Clear();
            dgvFeatures.DataSource = null;
            dgvFeatures.Rows.Clear();
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
            string features = dgvProduct.CurrentRow.Cells["colFeatures"].Value.ToString();
            string[] featuresList= features.Split('|');
            for (int i = 0; i < featuresList.Length-1; i++)
            {
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
//private void txtCategoryName_MouseClick(object sender, MouseEventArgs e)
//{
//    txtCategoryID.Text = string.Empty;
//    DataTable dt = new DataTable();
//    dt = balCategory.GetAllCategory();
//    LoadGridCategory(dt);
//}
//private void txtCategoryName_TextChanged(object sender, EventArgs e)
//{
//    DataTable dt = new DataTable();
//    dt = balCategory.GetCategory(txtCategoryName.Text);
//    LoadGridCategory (dt);
//}

//private void txtBrandName_TextChanged(object sender, EventArgs e)
//{
//    DataTable dt = new DataTable();
//    dt = balBrand.GetAllBrand(txtBrandName.Text);
//    LoadGrid(dt);
//}

//private void LoadGridCategory(DataTable dt)
//{
//    this.dgvProduct.DataSource = null;
//    this.dgvProduct.Rows.Clear();
//    dgvProduct.Columns["colBrandName"].Visible = false;
//    dgvProduct.Columns["colSN"].Visible = false;
//    for (int i = 0; i < dt.Rows.Count; i++)
//    {
//        dgvProduct.Rows.Add();
//        //dgvBrand.Rows[i].Cells["colBrandID"].Value = dt.Rows[i]["BrandID"].ToString();
//        //dgvBrand.Rows[i].Cells["colBrandName"].Value = dt.Rows[i]["BrandName"].ToString();
//        dgvProduct.Rows[i].Cells["colSN"].Value = i;
//        dgvProduct.Rows[i].Cells["colCategoryID"].Value = dt.Rows[i]["CategoryID"].ToString();
//        dgvProduct.Rows[i].Cells["colCategoryName"].Value = dt.Rows[i]["CategoryName"].ToString();
//    }
//}
//private void LoadGrid(DataTable dt)
//{
//    this.dgvProduct.DataSource = null;
//    this.dgvProduct.Rows.Clear();
//    dgvProduct.Columns["colBrandName"].Visible = true;
//    dgvProduct.Columns["colSN"].Visible = true;
//    for (int i = 0; i < dt.Rows.Count; i++)
//    {
//        dgvProduct.Rows.Add();
//        dgvProduct.Rows[i].Cells["colBrandID"].Value = dt.Rows[i]["BrandID"].ToString();
//        dgvProduct.Rows[i].Cells["colBrandName"].Value = dt.Rows[i]["BrandName"].ToString();
//        dgvProduct.Rows[i].Cells["colSN"].Value = i;
//        dgvProduct.Rows[i].Cells["colCategoryID"].Value = dt.Rows[i]["CategoryID"].ToString();
//        dgvProduct.Rows[i].Cells["colCategoryName"].Value = dt.Rows[i]["CategoryName"].ToString();
//    }
//}

//private void closeButton_MouseLeave(object sender, EventArgs e)
//{
//    closeButton.ForeColor = Color.White;
//}

//private void closeButton_MouseHover(object sender, EventArgs e)
//{
//    closeButton.ForeColor = Color.Red;
//}

//private void closeButton_Click(object sender, EventArgs e)
//{
//    Application.Exit();
//}

//private void btnSave_Click(object sender, EventArgs e)
//{
//    if (ValidateField())
//    {
//        MessageBox.Show("Plese Provide Brand Name and Category", "Save Category", MessageBoxButtons.OK);
//        return;
//    }
//    if (balBrand.AddBrand(txtBrandName.Text.Trim(),Convert.ToInt32(txtCategoryID .Text)))
//    {
//        MessageBox.Show("New Category Added", "Category Added", MessageBoxButtons.OK);
//        LoadGrid(balBrand.GetAllBrand(string.Empty));
//        this.txtBrandName.Text = string.Empty;
//    } 
//}

//private void btnDelete_Click(object sender, EventArgs e)
//{
//    if (ValidateField())
//    {
//        MessageBox.Show("Plese select Brand", "Delete Category",MessageBoxButtons.OK);
//        return;
//    }
//    if (balBrand.DeleteBrand(Convert.ToInt32(txtBrandID.Text.Trim())))
//    {
//        MessageBox.Show("Deleted Brand" + txtBrandName.Text, "Category Deleted", MessageBoxButtons.OK);
//        LoadGrid(balBrand.GetAllBrand(string.Empty));
//        this.txtBrandName.Text = string.Empty;
//    }
//}

//private void btnGetAllBrand_Click(object sender, EventArgs e)
//{
//    DataTable dt = new DataTable();
//    dt=balBrand.GetAllBrand(string.Empty);
//    LoadGrid(dt);
//    this.txtBrandName.Text = string.Empty;
//}

//private void btnUpdate_Click(object sender, EventArgs e)
//{
//    if (ValidateField())
//    {
//        MessageBox.Show("Plese select Brand and Category First", "Update Category", MessageBoxButtons.OK);
//        return;
//    }
//    //updating category 
//    if (balBrand.UpdateBrand(txtBrandName.Text, Convert.ToInt32(txtBrandID.Text),Convert.ToInt32(txtCategoryID.Text)))
//    {
//        MessageBox.Show("Value Updated Successfully", "Update Category", MessageBoxButtons.OK);
//        //reloading category values into grid
//        LoadGrid(balBrand.GetAllBrand(string.Empty));
//        this.txtBrandName.Text = string.Empty;
//    }

//}



//private void clear(object sender, EventArgs e)
//{
//    txtCategoryID.Text = string.Empty;
//    txtBrandID.Text = string.Empty;
//    txtBrandName.Text = string.Empty;
//    txtCategoryName.Text = string.Empty;
//    dgvProduct.Rows.Clear();
//    dgvProduct.Refresh();
//}

////load grid value to the text box when a row is slected from a data grid view
//private void dgvBrand_MouseClick(object sender, MouseEventArgs e)
//{
//    if (dgvProduct.Columns["colBrandName"].Visible == false)
//    {
//        txtCategoryID.Text = dgvProduct.CurrentRow.Cells["colCategoryID"].Value.ToString();
//        txtCategoryName.Text = dgvProduct.CurrentRow.Cells["colCategoryName"].Value.ToString();
//        LoadGridCategory(balCategory.GetAllCategory());
//        return;
//    }
//    txtBrandID.Text = dgvProduct.CurrentRow.Cells["colBrandID"].Value.ToString();
//    txtBrandName.Text = dgvProduct.CurrentRow.Cells["colBrandName"].Value.ToString();
//    txtCategoryID.Text = dgvProduct.CurrentRow.Cells["colCategoryID"].Value.ToString();
//    txtCategoryName.Text = dgvProduct.CurrentRow.Cells["colCategoryName"].Value.ToString();
//    LoadGrid(balBrand.GetAllBrand(string.Empty));
//}

//private void incButton1_Click(object sender, EventArgs e)
//{
//    frmCategory categoryForm = new frmCategory();
//    categoryForm.ShowDialog();
//}
