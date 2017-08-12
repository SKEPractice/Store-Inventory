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

        BALVendor balVendor = new BALVendor();
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
            if (!ValidateField() && balVendor.AddVendor(txtVendorName.Text, txtVendorAddress.Text, txtVendorPhone.Text, txtVendorEmail.Text))
            {
                MessageBox.Show("Vendor Added Successfully", "Added Vendor", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearControls();
                LoadGridVendor();
            }
            
        }

        private void LoadGridVendor()
        {
            DataTable dtVendor = new DataTable();
            dtVendor = balVendor.GetAllVendor(string.Empty);
            for (int i = 0; i < dtVendor.Rows.Count; i++)
            {
                dgvVendor.Rows.Add();
                dgvVendor.Rows[i].Cells["colSN"].Value = i;
                dgvVendor.Rows[i].Cells["colVendorID"].Value = dtVendor.Rows[i]["VendorID"];
                dgvVendor.Rows[i].Cells["colVendorName"].Value = dtVendor.Rows[i]["VendorName"];
                dgvVendor.Rows[i].Cells["colVendorAddress"].Value = dtVendor.Rows[i]["VendorAddress"];
                dgvVendor.Rows[i].Cells["colVendorPhone"].Value = dtVendor.Rows[i]["VendorPhone"];
                dgvVendor.Rows[i].Cells["colVendorEmail"].Value = dtVendor.Rows[i]["VendorEmail"];
            }
        }
        private void ClearControls()
        {
            txtVendorEmail.Text = string.Empty;
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

        private bool ValidateField()
        {
            if (txtVendorName.Text.Trim()==string.Empty)
            {
                txtVendorName.Focus();
                erpGeneral.SetError(txtVendorName, "Please provide vendor Name");
                return true;
            }
            else if (txtVendorAddress.Text.Trim() == string.Empty)
            {
                txtVendorAddress.Focus();
                erpGeneral.SetError(txtVendorAddress, "Please provide Vendor Address");
                return true;
            }
            else if (txtVendorPhone.Text.Trim() == string.Empty)
            {
                txtVendorPhone.Focus();
                erpGeneral.SetError(txtVendorPhone, "Please Provide Phone Number");
                return true;
            }
            else if (txtVendorEmail.Text.Trim() == string.Empty)
            {
                txtVendorEmail.Focus();
                erpGeneral.SetError(txtVendorEmail, "Please provide Email Address");
                return true;
            }
            else
            {
                return false;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult check= MessageBox.Show("Do you want to Delete", "Varify", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (check == DialogResult.Yes && txtVendorID.Text != string.Empty && balVendor.DeleteVendor(Convert.ToInt32(txtVendorID.Text)))
            {
                MessageBox.Show("Vendor Deleted Successfully", "Delete Vendor", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearControls();
                LoadGridVendor();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            DialogResult check = MessageBox.Show("Are you sure you want to Update", "Varify", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (check == DialogResult.Yes && !ValidateField() && balVendor.UpdateVendor(Convert.ToInt32(txtVendorID.Text), txtVendorName.Text, txtVendorAddress.Text, txtVendorPhone.Text, txtVendorEmail.Text))
            {
                MessageBox.Show("Vendor Updated Successfully", "Update Vendor", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearControls();
                LoadGridVendor();
            }
        }

        private void btnGetAllVendor_Click(object sender, EventArgs e)
        {
            LoadGridVendor();
        }

        private void btnClear_Click(object sender,EventArgs e)
        {
            ClearControls();
        }

        private void dgvVendor_MouseClick(object sender, MouseEventArgs e)
        {
            txtVendorID.Text = dgvVendor.CurrentRow.Cells["colVendorID"].Value.ToString();
            txtVendorName.Text = dgvVendor.CurrentRow.Cells["colVendorName"].Value.ToString();
            txtVendorAddress.Text = dgvVendor.CurrentRow.Cells["colVendorAddress"].Value.ToString();
            txtVendorPhone.Text = dgvVendor.CurrentRow.Cells["colVendorPhone"].Value.ToString();
            txtVendorEmail.Text = dgvVendor.CurrentRow.Cells["colVendorEmail"].Value.ToString();
        }
    }
}
