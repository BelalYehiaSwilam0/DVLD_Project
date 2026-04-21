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
using static BusinessLayer_DVLD.clsLicense;

namespace DVLD
{
    public partial class frmReplacementForLostOrDamagedLicense : Form
    {
       int _newLicenseID;


        public frmReplacementForLostOrDamagedLicense()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
       private enIssueReason _GetIssueReason()
        {
            if (rbDamagedLicense.Checked)
                return enIssueReason.DamagedReplacement;
            else
                return enIssueReason.LostReplacement;
        }
        private void btnIssue_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to Issue a Replacement for the license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            clsLicense NewLicense = 
                ctrlFilterWithDriverLicenseInfoCard1.SelectedLicenseInfo.Replace(_GetIssueReason(), clsGlobal.CurrentUser.UserID);

            if(NewLicense == null)
            {
                MessageBox.Show("Faild to Issue a replacemnet for this  License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }
            btnIssue.Enabled = false;
            _newLicenseID = NewLicense.LicenseID;
            lblReplacedLicenseIDResult.Text = NewLicense.LicenseID.ToString();
            lblLRApplicationIDResult.Text = NewLicense.ApplicationID.ToString();
            gbReplacementFor.Enabled = false;
            ctrlFilterWithDriverLicenseInfoCard1.FilterEnabled = false;
            lnkShowLicenseInfo.Enabled = true;
        }
        private void lnkShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowPersonLicenseHistory showPersonLicenseHistory = new frmShowPersonLicenseHistory(ctrlFilterWithDriverLicenseInfoCard1.SelectedLicenseInfo.DriverInfo.PersonID);
            showPersonLicenseHistory.ShowDialog();
        }
        private void lnkShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicense showLicense = new frmShowLicense(_newLicenseID);
            showLicense.ShowDialog();
        }

        private void ctrlFilterWithDriverLicenseInfoCard1_OnLicenseSelected(int obj)
        {
            int SelectedLicense = obj;
            lblOldLocalLicenseIDResult.Text = SelectedLicense.ToString();
            lnkShowLicenseHistory.Enabled = (SelectedLicense != -1);

            if (SelectedLicense == -1)
                return;

            // Dont allow Replace if license not active. 
            if (!ctrlFilterWithDriverLicenseInfoCard1.SelectedLicenseInfo.IsActive)
            {
                MessageBox.Show("Selected License is not Not Active, choose an active license."
                   , "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnIssue.Enabled = false;
                return;
            }

            btnIssue.Enabled = true;
        }

        private void frmReplacementForLostOrDamagedLicense_Validated(object sender, EventArgs e)
        {
            ctrlFilterWithDriverLicenseInfoCard1.TxtLicenseIDFocus();
        }

        private void frmReplacementForLostOrDamagedLicense_Load(object sender, EventArgs e)
        {
            lblApplicationDate.Text = clsFormat.DateToShort(DateTime.Now);
            lblCreatedbyResult.Text = clsGlobal.CurrentUser.UserName;
            rbDamagedLicense.Enabled = true;
        }

        private int _GetApplicationType()
        {
            if (rbDamagedLicense.Checked)
                return (int)clsApplication.enApplicationType.ReplaceDamagedDrivingLicense;
            else
                return (int)clsApplication.enApplicationType.ReplaceLostDrivingLicense;
        }
        private void rbDamagedLicense_CheckedChanged(object sender, EventArgs e)
        {
            lblTitle.Text = "Replacement for Damaged License";
            this.Text = lblTitle.Text;
            lblApplicationFeesResult.Text = clsApplicationTypes.GetApplicationTypeInfoByID(_GetApplicationType()).ApplicationFees.ToString();
        }

        private void rbLostLicense_CheckedChanged(object sender, EventArgs e)
        {
            lblTitle.Text = "Replacement for Lost License";
            this.Text = lblTitle.Text;
            lblApplicationFeesResult.Text = clsApplicationTypes.GetApplicationTypeInfoByID(_GetApplicationType()).ApplicationFees.ToString();
        }
    }
}
