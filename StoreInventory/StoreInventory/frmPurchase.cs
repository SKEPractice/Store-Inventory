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

        //private void LoadCategoryGrid(DataTable dt)
        //{
        //    this.dgvVendor.DataSource = null;
        //    this.dgvVendor.Rows.Clear();
        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        dgvVendor.Rows.Add();
        //        dgvVendor.Rows[i].Cells["colSN"].Value = i;
        //        dgvVendor.Rows[i].Cells["colCategoryID"].Value = dt.Rows[i]["CategoryID"].ToString();
        //        dgvVendor.Rows[i].Cells["colCategoryName"].Value = dt.Rows[i]["CategoryName"].ToString();
        //    }
        //}

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

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }

        private void GetPurchaseDetail_Click(object sender, EventArgs e)
        {

        }

        private void btnClear_Click(object sender, EventArgs e)
        {

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
                erpGeneral.SetError(txtPrice, "Please provide product Name");
                return true;
            }
            else if (txtQuantity.Text.Trim() == string.Empty)
            {
                txtQuantity.Focus();
                erpGeneral.SetError(txtQuantity, "Please provide price");
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
            dgvVendor.DataSource = null;
            dgvVendor.Rows.Clear();
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
        }
    }
}

