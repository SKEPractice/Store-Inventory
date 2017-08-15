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
    public partial class frmPurchase : Form
    {
        public frmPurchase()
        {
            InitializeComponent();
        }

        BALVendor balVendor = new BALVendor();
        BALProduct balProduct = new BALProduct();
        BALPurchase balPurchase = new BALPurchase();

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
            //balPurchase.AddPurchase
        }

        private void btnAdd_click(object sender, EventArgs e)
        {
            if (!ValidateField())
            {
                int i = dgvVendor.Rows.Count;
                dgvVendor.Rows.Add();
                dgvVendor.Rows[i].Cells["colSN"].Value = i;
                dgvVendor.Rows[i].Cells["colVendorID"].Value = cboVendor.SelectedValue;
                dgvVendor.Rows[i].Cells["colVendorName"].Value = cboVendor.Text;
                dgvVendor.Rows[i].Cells["colProductID"].Value = cboProduct.SelectedValue;
                dgvVendor.Rows[i].Cells["colProductName"].Value = cboProduct.Text;
                dgvVendor.Rows[i].Cells["colProductPrice"].Value = txtPrice.Text;
                dgvVendor.Rows[i].Cells["colProductQuantity"].Value = txtQuantity.Text;
                dgvVendor.Rows[i].Cells["colTotal"].Value = Convert.ToInt32(txtPrice.Text) * Convert.ToInt32(txtQuantity.Text);
                string grandTotal = dgvVendor.Rows[i].Cells["colTotal"].Value.ToString();
                ClearControls();
                txtGrandTotal.Text = grandTotal;
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            DialogResult check = MessageBox.Show("Are you sure you want to update", "Confirm Update", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (check == DialogResult.Yes || !ValidateField())
            {
                dgvVendor.Rows.RemoveAt(dgvVendor.CurrentRow.Index);
                int i = dgvVendor.Rows.Count;
                dgvVendor.Rows.Add();
                dgvVendor.Rows[i].Cells["colSN"].Value = i;
                dgvVendor.Rows[i].Cells["colVendorID"].Value = cboVendor.SelectedValue;
                dgvVendor.Rows[i].Cells["colVendorName"].Value = cboVendor.Text;
                dgvVendor.Rows[i].Cells["colProductID"].Value = cboProduct.SelectedValue;
                dgvVendor.Rows[i].Cells["colProductName"].Value = cboProduct.Text;
                dgvVendor.Rows[i].Cells["colProductPrice"].Value = txtPrice.Text;
                dgvVendor.Rows[i].Cells["colProductQuantity"].Value = txtQuantity.Text;
                ClearControls();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult check = MessageBox.Show("Are you sure you want to Delete", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (check == DialogResult.Yes)
            {
                dgvVendor.Rows.RemoveAt(dgvVendor.CurrentRow.Index);
                ClearControls();
            }
        }

        private void GetPurchaseDetail_Click(object sender, EventArgs e)
        {

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            dgvVendor.DataSource = null;
            dgvVendor.Rows.Clear();
            ClearControls();
        }
        private bool ValidateField()
        {
            if (cboVendor.SelectedIndex == 0)
            {
                cboVendor.Focus();
                erpGeneral.SetError(cboVendor, "Please Select Vendor First");
                return true;
            }
            else if (cboProduct.SelectedIndex == 0)
            {
                cboProduct.Focus();
                erpGeneral.SetError(cboProduct, "Please Select Product First");
                return true;
            }
            else if (txtPrice.Text.Trim() == string.Empty)
            {
                txtPrice.Focus();
                erpGeneral.SetError(txtPrice, "Please provide price for product");
                return true;
            }
            else if (txtQuantity.Text.Trim() == string.Empty)
            {
                txtQuantity.Focus();
                erpGeneral.SetError(txtQuantity, "Please provide number of Quantity");
                return true;
            }
            else
            {
                return false;
            }
        }
        private void ClearControls()
        {
            erpGeneral.Clear();
            //dgvVendor.DataSource = null;
            //dgvVendor.Rows.Clear();
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

        private void frmPurchase_Load(object sender, EventArgs e)
        {
            DataTable dtProduct = new DataTable();
            DataTable dtVendor = new DataTable();
            dtProduct = balProduct.GetAllProduct(string.Empty);
            DataRow drProduct = dtProduct.NewRow();
            drProduct["ProductName"] = "-- Please Select --";
            dtProduct.Rows.InsertAt(drProduct, 0);
            cboProduct.DataSource = dtProduct;
            cboProduct.DisplayMember = "ProductName";
            cboProduct.ValueMember = "ProductID";
            dtVendor = balVendor.GetAllVendor(string.Empty);
            DataRow drVendor = dtVendor.NewRow();
            drVendor["VendorName"] = "-- Please Select --";
            dtVendor.Rows.InsertAt(drVendor, 0);
            cboVendor.DataSource = dtVendor;
            cboVendor.DisplayMember = "VendorName";
            cboVendor.ValueMember = "VendorID";
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
        private void txtQuantity_KeyPress(object sender,KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void dgvVendor_MouseClick(object sender, MouseEventArgs e)
        {
            cboProduct.SelectedValue = dgvVendor.CurrentRow.Cells["colProductID"].Value;
            cboVendor.SelectedValue = dgvVendor.CurrentRow.Cells["colVendorID"].Value;
            txtPrice.Text = dgvVendor.CurrentRow.Cells["colProductPrice"].Value.ToString();
            txtQuantity.Text = dgvVendor.CurrentRow.Cells["colProductQuantity"].Value.ToString();
        }
    }
}

