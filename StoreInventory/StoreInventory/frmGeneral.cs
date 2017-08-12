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
    public partial class frmGeneral : Form
    {
        public frmGeneral()
        {
            InitializeComponent();
        }

        BALVendor balVendor = new BALVendor();
         
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
        private void ClearControls()
        {
            //erpGeneral.Clear();
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            
        }
        //private bool ValidateField()
        //{
        //    if (txtVendorName.Text.Trim()==string.Empty)
        //    {
        //        txtVendorName.Focus();
        //        erpGeneral.SetError(cboCategory, "Please Select Category");
        //        return true;
        //    }
        //    else if (cboBrand.SelectedIndex == 0)
        //    {
        //        cboBrand.Focus();
        //        erpGeneral.SetError(cboBrand, "Please Select Brand");
        //        return true;
        //    }
        //    else if (txtProductName.Text.Trim() == string.Empty)
        //    {
        //        txtProductName.Focus();
        //        erpGeneral.SetError(txtProductName, "Please provide product Name");
        //        return true;
        //    }
        //    else if (txtPrice.Text.Trim() == string.Empty)
        //    {
        //        txtPrice.Focus();
        //        erpGeneral.SetError(txtPrice, "Please provide price");
        //        return true;
        //    }
        //    else if (dgvFeatures.Rows.Count - 1 <= 0)
        //    {
        //        dgvFeatures.Focus();
        //        erpGeneral.SetError(dgvFeatures, "Please provide at least one feature");
        //        return true;
        //    }
            //else
            //{
            //    return false;
            //}
        }
    }

