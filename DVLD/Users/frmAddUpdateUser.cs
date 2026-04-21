using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessLayer_DVLD;

namespace DVLD
{
    public partial class frmAddUpdateUser : Form
    {
        public enum enMode { AddNew = 0, Update = 1 }
        private static enMode _Mode = enMode.Update;

        private int _UserID;

        private clsUsers _UserInfo;
        public frmAddUpdateUser()
        {
            InitializeComponent();
            _Mode = enMode.AddNew;
        }

        public frmAddUpdateUser(int UserID)
        {
            InitializeComponent();
            _UserID = UserID;
            _Mode = enMode.Update;
        }


        private void btnSave_Click(object sender, EventArgs e)
        {

            if(!this.ValidateChildren())
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            _UserInfo.PersonID = ctrlCardPersonInfoWithFilter1.PersonID;
            _UserInfo.UserName = txtUserName.Text.Trim();
            _UserInfo.Password = txtPassword.Text.Trim();
            _UserInfo.IsActive = chkIsActive.Checked;

            if (_UserInfo.Save())
            {
                lblUserIDResult.Text = _UserInfo.UserID.ToString();
                //change form mode to update.
                _Mode = enMode.Update;
                lblTitle.Text = "Update User";
                this.Text = "Update User";

                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        void _ResetDefaultValues()
        {
            if (_Mode == enMode.AddNew)
            {
                lblTitle.Text = "Add New User";
                this.Text = "Add New User";
                _UserInfo = new clsUsers();

                tpLoginInfo.Enabled = false;

                ctrlCardPersonInfoWithFilter1.FilterFocus();

            }
            else
            {
                lblTitle.Text = "Update User";
                this.Text = "Update User";

                tpLoginInfo.Enabled = true;
                btnSave.Enabled = true;


            }

            txtUserName.Text = "";
            txtPassword.Text = "";
            txtConfirmPassword.Text = "";
            chkIsActive.Checked = true;
        }

        void _LoadData()
        {
            _UserInfo = clsUsers.FindUserByID(_UserID);
            ctrlCardPersonInfoWithFilter1.FilterEnabled = false;

            if (_UserInfo == null)
            {
                MessageBox.Show("No User with ID = " + _UserID, "User Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();

                return;
            }

            lblUserID.Text = _UserInfo.UserID.ToString();
            txtUserName.Text = _UserInfo.UserName;
            txtPassword.Text = _UserInfo.Password;
            txtPassword.Enabled = false;
            txtConfirmPassword.Text = _UserInfo.Password;
            txtConfirmPassword.Enabled = false;
            chkIsActive.Checked = _UserInfo.IsActive;
            ctrlCardPersonInfoWithFilter1.LoadPersonInfo(_UserInfo.PersonID);

        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (_Mode == enMode.Update)
            {
                btnSave.Enabled = true;
                tpLoginInfo.Enabled = true;
                tcUserInfo.SelectedTab = tcUserInfo.TabPages["tpLoginInfo"];
                return;
            }

            //incase of add mode .

            if (ctrlCardPersonInfoWithFilter1.PersonID != -1)
            {
                if (clsUsers.IsUserExist(ctrlCardPersonInfoWithFilter1.PersonID))
                {
                    MessageBox.Show("The selected person already has a user account. Please select another person or update the existing account.", "Duplicate User", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ctrlCardPersonInfoWithFilter1.FilterFocus();
                }
                else
                {
                    btnSave.Enabled = true;
                    tpLoginInfo.Enabled = true;
                    tcUserInfo.SelectedTab = tcUserInfo.TabPages["tpLoginInfo"];
                }


            }
            else
            {
                MessageBox.Show("You should to Choose a Perosn.", "Select a Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ctrlCardPersonInfoWithFilter1.FilterFocus();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmAddNewUser_Load(object sender, EventArgs e)
        {
            _ResetDefaultValues();

            if (_Mode == enMode.Update)
                _LoadData();
        }

        private void frmAddUpdateUser_Activated(object sender, EventArgs e)
        {
            ctrlCardPersonInfoWithFilter1.FilterFocus();
        }

        private void txtUserName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtUserName.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtUserName, "User name cannot be blank");
                return;

            }
            else
            {
                errorProvider1.SetError(txtUserName, null);
            };

            if (_Mode == enMode.AddNew)
            {
                if (clsUsers.IsUserExist(txtUserName.Text.Trim()))
                {
                    e.Cancel = true;
                    errorProvider1.SetError(txtUserName, "User name is used by another user");
                }
                else
                {
                    errorProvider1.SetError(txtUserName, null);
                };
            }
            else
            {
                //incase update make sure not to use another user name 

                if(_UserInfo.UserName != txtUserName.Text.Trim())
                {
                    if (clsUsers.IsUserExist(txtUserName.Text.Trim()))
                    {
                        e.Cancel = true;
                        errorProvider1.SetError(txtUserName, "User name is used by another user");
                    }
                    else
                    {
                        errorProvider1.SetError(txtUserName, null);
                    };
                }

            }
        }

        private void txtPassword_Validating(object sender, CancelEventArgs e)
        {
            if(string.IsNullOrEmpty(txtPassword.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtPassword, "Password cannot by blank");

            }
            else
            {
                errorProvider1.SetError(txtPassword, null);
            };

        }

        private void txtConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            if(txtConfirmPassword.Text.Trim() != txtPassword.Text.Trim())
            {
                e.Cancel = true;
                errorProvider1.SetError(txtConfirmPassword, "Password confirmation does not match password!");
            }
            else
            {
                errorProvider1.SetError(txtConfirmPassword, null);
            };
        }
    }
}
