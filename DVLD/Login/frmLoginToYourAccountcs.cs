using System;
using System.Diagnostics;
using System.Windows.Forms;
using BusinessLayer_DVLD;
using DVLD.Global_Classes;
using DVLD_UserContext;
using System.Security.Cryptography;
using System.Text;

using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using DVLD.Classes;

namespace DVLD
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            //Check if UserName or Password is Empty
            if (string.IsNullOrWhiteSpace(txtUserName.Text))
            {
                MessageBox.Show("UserName is required", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Password is required", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            clsUsers _UserInfo = clsUsers.FindUserByUsernameAndPassword(txtUserName.Text.Trim(), clsUtil.ComputeHash(txtPassword.Text.Trim()));

            if(_UserInfo == null)
            {
                txtUserName.Focus();
                MessageBox.Show("Invalid Username/Password.", "Wrong Credintials", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsEventLog.EventLogs("DVDL_Application", "Application", "Wrong Credentials", EventLogEntryType.Error);
                return;
            }
            clsEventLog.EventLogs("DVDL_Application", "Application", $"Login Successful for user {txtUserName.Text}", EventLogEntryType.Information);
            if (chkRememberMe.Checked)
            {
                // store username and password 
                clsGlobal.RememberUserNameAndPassword(txtUserName.Text.Trim(), clsUtil.ComputeHash(txtPassword.Text.Trim()));
            }
            else
            {
                // story empty username and password
                clsGlobal.RememberUserNameAndPassword("", "");
            }

            //Incase the user is not active
            if(!_UserInfo.IsActive)
            {
                txtUserName.Focus();
                MessageBox.Show("This account exists, but it's not active , Contact Admin.", "Account Not Active", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            clsGlobal.CurrentUser = _UserInfo;
            this.Hide();
            frmMain main = new frmMain(this);
            main.ShowDialog();
        }
        
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            string Username = "", Password = "";

            if(clsGlobal.GetStoredCredential(ref  Username))
            {
                txtUserName.Text = Username;
                //txtPassword.Text = Password;
                chkRememberMe.Checked = true;
            }
            else
                chkRememberMe.Checked = false;

        }
    }
}
