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
using DVLD.Global_Classes;
using DVLD_UserContext;

namespace DVLD
{
    public partial class frmRelaseDetainedLicense : Form
    {
        int _SelectedLicenseID = -1;
        public frmRelaseDetainedLicense()
        {
            InitializeComponent();
        }
        public frmRelaseDetainedLicense(int LicenseID)
        {
            InitializeComponent();

            _SelectedLicenseID = LicenseID;
            ctrlFilterWithDriverLicenseInfoCard1.LoadLiceseInfo(_SelectedLicenseID);
            ctrlFilterWithDriverLicenseInfoCard1.FilterEnabled = false;
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

            if (_SelectedLicenseID == -1)
                return;

            lblLicenseIDResult.Text = _SelectedLicenseID.ToString();

            lnkShowLicenseHistory.Enabled = (_SelectedLicenseID != -1);

            if(!ctrlFilterWithDriverLicenseInfoCard1.SelectedLicenseInfo.IsDetained)
            {
                MessageBox.Show("Selected License i is not detained, choose another one.", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            lblCreatedbyResult.Text = clsGlobal.CurrentUser.UserName;
            lblApplicationFeesResult.Text = clsApplicationTypes.GetApplicationTypeInfoByID((int)clsApplication.enApplicationType.ReleaseDetainedDrivingLicsense).ApplicationFees.ToString();

            lblDetainIDResult.Text = ctrlFilterWithDriverLicenseInfoCard1.SelectedLicenseInfo.DetainedLicenseInfo.DetainID.ToString();
            lblDetainDateResult.Text = clsFormat.DateToShort(ctrlFilterWithDriverLicenseInfoCard1.SelectedLicenseInfo.DetainedLicenseInfo.DetainDate);
            lblFineFeesResult.Text = ctrlFilterWithDriverLicenseInfoCard1.SelectedLicenseInfo.DetainedLicenseInfo.FineFees.ToString();
            lblTotalFeesResult.Text = (Convert.ToSingle(lblApplicationFeesResult.Text.Trim()) + Convert.ToSingle(lblFineFeesResult.Text.Trim())).ToString();
            btnRelease.Enabled = true;
        }

        private void frmRelaseDetainedLicense_Activated(object sender, EventArgs e)
        {
            ctrlFilterWithDriverLicenseInfoCard1.TxtLicenseIDFocus();
        }

        private void btnRelease_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to release this detained  license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            int ApplicationID = -1;

            bool IsReleased = ctrlFilterWithDriverLicenseInfoCard1.
                SelectedLicenseInfo.ReleaseDetainedLicense(clsGlobal.CurrentUser.UserID, ref ApplicationID);

            lblRApplicationIDResult.Text = ApplicationID.ToString();

            if (!IsReleased)
            {
                MessageBox.Show("Faild to to release the Detain License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show("Detained License released Successfully ", "Detained License Released", MessageBoxButtons.OK, MessageBoxIcon.Information);

            btnRelease.Enabled = false;
            ctrlFilterWithDriverLicenseInfoCard1.FilterEnabled = false;
            lnkShowLicenseInfo.Enabled = true;
        }
    }
}
