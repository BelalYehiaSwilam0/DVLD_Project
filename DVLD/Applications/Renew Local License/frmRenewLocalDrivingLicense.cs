using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessLayer_DVLD;
using DVLD.Global_Classes;
using DVLD_UserContext;

namespace DVLD
{
    public partial class frmRenewLocalDrivingLicense : Form
    {
        clsLicense _newLicenseinfo;
        public frmRenewLocalDrivingLicense()
        {
            InitializeComponent();
        }
        private void lnkShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowPersonLicenseHistory showPersonLicenseHistory = new frmShowPersonLicenseHistory(ctrlFilterWithDriverLicenseInfoCard1.SelectedLicenseInfo.DriverInfo.PersonID);
            showPersonLicenseHistory.ShowDialog();
        }
        private void lnkShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicense showLicense = new frmShowLicense(_newLicenseinfo.LicenseID);
            showLicense.ShowDialog();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnIssue_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to Renew the license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            clsLicense NewLicense = 
                ctrlFilterWithDriverLicenseInfoCard1.SelectedLicenseInfo.RenewLicense(txtNotes.Text.Trim(),clsGlobal.CurrentUser.UserID);

            if(NewLicense == null)
            {
                MessageBox.Show("Faild to Renew the License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            lblRLApplicationIDResult.Text = NewLicense.ApplicationID.ToString();
            lblRenewedLicenseIDResult.Text = NewLicense.LicenseID.ToString();

            MessageBox.Show("Licensed Renewed Successfully with ID=" + NewLicense.LicenseID.ToString(), "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);

            btnRenewLicense.Enabled = false;
            ctrlFilterWithDriverLicenseInfoCard1.FilterEnabled = false;
            lnkShowLicenseInfo.Enabled = true;
        }
        public void SetupData()
        {

            lblRLApplicationIDResult.Text = "[???]";
            lblApplicationDate.Text = clsFormat.DateToShort(DateTime.Now);
            lblIssueDateResult.Text = lblApplicationDate.Text;
            lblApplicationFeesResult.Text = clsApplicationTypes.GetApplicationTypeInfoByID((int)clsApplication.enApplicationType.RenewDrivingLicense).ApplicationFees.ToString();
            lblExpirationDateResult.Text = "[???]";
            lblCreatedbyResult.Text = clsGlobal.CurrentUser.UserName; ;
            lblLicenseFeesResult.Text = "[$$$]";
            lblTotalFeesResult.Text = "[$$$]";
            txtNotes.Text = string.Empty;
        }

        private void ctrlFilterWithDriverLicenseInfoCard1_OnLicenseSelected(int obj)
        {
            int SelectedLicenseID = obj;

            lnkShowLicenseHistory.Enabled = (SelectedLicenseID != -1);

            if (SelectedLicenseID == -1)
                return;

            lblOldLocalLicenseIDResult.Text = SelectedLicenseID.ToString();
            int DefaultValidityLength = ctrlFilterWithDriverLicenseInfoCard1.SelectedLicenseInfo.LicenseClassInfo.DefaultValidityLength;
            lblExpirationDateResult.Text = clsFormat.DateToShort(DateTime.Now.AddYears(DefaultValidityLength));
            lblLicenseFeesResult.Text = ctrlFilterWithDriverLicenseInfoCard1.SelectedLicenseInfo.LicenseClassInfo.ClassFees.ToString();
            lblTotalFeesResult.Text = (Convert.ToSingle(lblApplicationFeesResult.Text) + Convert.ToSingle(lblLicenseFeesResult.Text)).ToString();
            txtNotes.Text = ctrlFilterWithDriverLicenseInfoCard1.SelectedLicenseInfo.Notes;

            if (ctrlFilterWithDriverLicenseInfoCard1.SelectedLicenseInfo == null)
            {
                MessageBox.Show($"Local license with ID = {SelectedLicenseID} is not exist",
                     "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnRenewLicense.Enabled = false;
                return;
            }

            if (!ctrlFilterWithDriverLicenseInfoCard1.SelectedLicenseInfo.IsLicenseExpired())
            {
                MessageBox.Show("Selected License is not yet expiared, it will expire on: " + clsFormat.DateToShort(ctrlFilterWithDriverLicenseInfoCard1.SelectedLicenseInfo.ExpirationDate)
                    , "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnRenewLicense.Enabled = false;
                return;
            }

            if(!ctrlFilterWithDriverLicenseInfoCard1.SelectedLicenseInfo.IsActive)
            {
                MessageBox.Show("Selected License is not Not Active, choose an active license."
                    , "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnRenewLicense.Enabled = false;
                return;
            }
            btnRenewLicense.Enabled = true;

        }

        private void frmRenewLocalDrivingLicense_Load(object sender, EventArgs e)
        {
            ctrlFilterWithDriverLicenseInfoCard1.TxtLicenseIDFocus();
            SetupData();
        }

        private void frmRenewLocalDrivingLicense_Activated(object sender, EventArgs e)
        {
            ctrlFilterWithDriverLicenseInfoCard1.TxtLicenseIDFocus();
        }
    }
}
