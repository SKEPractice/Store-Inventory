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
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();        
        }
        BALUser balUser = new BALUser();
        private void frmLogin_Load(object sender, EventArgs e)
        {
            LoadComboBox();
        }

        private void LoadComboBox()
        {
            DataTable dt = new DataTable();
            dt = balUser.GetUserType(1);
            DataRow dr = dt.NewRow();
            dr["Name"] = "Please Select";
            dt.Rows.InsertAt(dr, 0);

            cboUserType.DataSource = dt;
            cboUserType.DisplayMember = "Name";
            cboUserType.ValueMember = "RoleID";
        }

        //private void AssignEvents()
        //{
        //    btnCancle.Click += new EventHandler(this.QuitApplication);
        //    picClose.Click += new EventHandler(this.QuitApplication);
        //    picMinimize.Visible = false;
        //    picMaximize.Visible = false;
        //}
        private void btnLogin_Click(object sender, EventArgs e)
        {
            bool check=checkInput();
            if (check== false)
            {
                Int32 checkLogin = balUser.checkUser(txtUserName.Text, txtPassword.Text, Convert.ToInt32(cboUserType.SelectedValue.ToString()));
                if (checkLogin == 1)
                {
                    MessageBox.Show("Login Successful", "Login", MessageBoxButtons.OK);
                    this.Hide();
                    frmMain mainForm = new frmMain();
                    mainForm.Show();
                }
            }            
        }

        private bool checkInput()
        {
            if (txtUserName.Text.Length <= 0)
            {
                MessageBox.Show("Please input User Name", "Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtUserName.Focus();
                return true;
            }
            if (txtPassword.Text.Length <= 0)
            {
                MessageBox.Show("Please enter Password", "Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.Focus();
                return true;
            }
            if (cboUserType.Text == "Please Select")
            {
                MessageBox.Show("Please select User Type", "Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cboUserType.Focus();
                return true;
            }
            return false;
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void closeButton_MouseHover(object sender, EventArgs e)
        {            
            closeButton.ForeColor = System.Drawing.Color.Red;
        }

        private void closeButton_MouseLeave(object sender, EventArgs e)
        {
            closeButton.ForeColor = System.Drawing.Color.White;
        }
    }
}
