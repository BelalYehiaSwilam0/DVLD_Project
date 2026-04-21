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
using DVLD.Classes;
using DVLD.Global_Classes;
using DVLD_UserContext;

namespace DVLD
{
    public partial class frmDetainLicense : Form
    {
        private int _DetainID = -1;
        private int _SelectedLicenseID = -1;
        public frmDetainLicense()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void lnkShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
            frmShowPersonLicenseHistory showPersonLicenseHistory = new frmShowPersonLicenseHistory(ctrlFilterWithDriverLicenseInfoCard1.SelectedLicenseInfo.DriverInfo.PersonID);
            showPersonLicenseHistory.ShowDialog();
        }
        private void lnkShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicense showLicense = new frmShowLicense(ctrlFilterWithDriverLicenseInfoCard1.SelectedLicenseInfo.LicenseID);
            showLicense.ShowDialog();
        }

        private void ctrlFilterWithDriverLicenseInfoCard1_OnLicenseSelected(int obj)
        {
            _SelectedLicenseID = obj;
            lblLicenseIDResult.Text = _SelectedLicenseID.ToString();
            lnkShowLicenseHistory.Enabled = (_SelectedLicenseID != -1);

            if (_SelectedLicenseID == -1)
                return;

            //Make sure the license is not detained
            if(ctrlFilterWithDriverLicenseInfoCard1.SelectedLicenseInfo == null)
            {
                MessageBox.Show("This License not Exists", "Confirm", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if(ctrlFilterWithDriverLicenseInfoCard1.SelectedLicenseInfo.IsDetained)
            {
                MessageBox.Show("Selected License i already detained, choose another one.", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!ctrlFilterWithDriverLicenseInfoCard1.SelectedLicenseInfo.IsActive)
            {
                MessageBox.Show(
             $"This license is not allowed because it's not active.",
             "Not Allowed",
             MessageBoxButtons.OK,
             MessageBoxIcon.Warning);
                return;
            }

            txFineFees.Focus();
            btnDetain.Enabled = true;
        }

        private void frmDetainLicense_Load(object sender, EventArgs e)
        {
            lblDetainDateResult.Text = clsFormat.DateToShort(DateTime.Now);
            lblCreatedbyResult.Text = clsGlobal.CurrentUser.UserName;
        }

        private void btnDetain_Click(object sender, EventArgs e)
        {
            //try this when you come back ..
            if (!this.ValidateChildren())
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show("Are you sure you want to detain this license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            _DetainID = ctrlFilterWithDriverLicenseInfoCard1.SelectedLicenseInfo.Detain(Convert.ToDecimal(txFineFees.Text.Trim()),clsGlobal.CurrentUser.UserID);

            if(_DetainID == -1)
            {
                MessageBox.Show("Faild to Detain License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            lblDetainIDResult.Text = _DetainID.ToString();
            MessageBox.Show("License Detained Successfully with ID=" + _DetainID.ToString(), "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnDetain.Enabled = false;
            ctrlFilterWithDriverLicenseInfoCard1.FilterEnabled = false;
            txFineFees.Enabled = false;
            lnkShowLicenseInfo.Enabled = true;
        }

        private void txFineFees_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txFineFees.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txFineFees, "Fees cannot be empty!");
                return;
            }
            else
            {
                errorProvider1.SetError(txFineFees, null);

            };

            if(!clsValidatoin.IsNumber(txFineFees.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txFineFees, "Invalid Number.");
            }
            {
                errorProvider1.SetError(txFineFees, null);
            };
        }

        private void frmDetainLicense_Activated(object sender, EventArgs e)
        {
            ctrlFilterWithDriverLicenseInfoCard1.TxtLicenseIDFocus();
        }
    }
}
