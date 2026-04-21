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
    public partial class frmInternationalLicenses : Form
    {
        private int _selectedLicense = -1;
        private int _newInternationalLicense = -1;

        public frmInternationalLicenses()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public void SetupData()
        {
            lblApplicationDate.Text = clsFormat.DateToShort(DateTime.Now);
            lblExpirationDateResult.Text = clsFormat.DateToShort(DateTime.Now.AddYears(Convert.ToInt32(clsSettings.Find("Default Internatinal DefaultValidityLength").SettingValue)));
            lblIssueDateResult.Text = clsFormat.DateToShort(DateTime.Now);
            lblFeesResult.Text = clsApplicationTypes.GetApplicationTypeInfoByID((int)clsApplication.enApplicationType.NewInternationalLicense).ApplicationFees.ToString();
            lblCreatedbyResult.Text = clsGlobal.CurrentUser.UserName;
        }

      

        private void btnIssue_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to issue this international license?", "Confirm License Issuance", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.No)
                return;

            clsInternationalLicense _InternationalLicenseInfo = new clsInternationalLicense();

            _InternationalLicenseInfo.ApplicantPersonID = ctrlFilterWithDriverLicenseInfoCard1.SelectedLicenseInfo.DriverInfo.PersonID;
            _InternationalLicenseInfo.DriverID = ctrlFilterWithDriverLicenseInfoCard1.SelectedLicenseInfo.DriverID;
            _InternationalLicenseInfo.IssuedUsingLocalLicenseID = ctrlFilterWithDriverLicenseInfoCard1.SelectedLicenseInfo.LicenseID;
            _InternationalLicenseInfo.IssueDate = DateTime.Now;
            _InternationalLicenseInfo.ExpirationDate = DateTime.Now.AddYears(Convert.ToInt32(clsSettings.Find("Default Internatinal DefaultValidityLength").SettingValue));
            _InternationalLicenseInfo.IsActive = true;
            _InternationalLicenseInfo.CreatedByUserID = clsGlobal.CurrentUser.UserID;

            if(!_InternationalLicenseInfo.Save())
            {
                MessageBox.Show("Faild");
                return;
            }
            _newInternationalLicense = _InternationalLicenseInfo.InternationalLicenseID;
            lblILLicenseIDResult.Text = _InternationalLicenseInfo.InternationalLicenseID.ToString();
            lblApplicationIDResult.Text = _InternationalLicenseInfo.ApplicationID.ToString();
            ctrlFilterWithDriverLicenseInfoCard1.FilterEnabled = false;
            btnIssue.Enabled = false;
            lnkShowLicenseInfo.Enabled = true;
        }
        private void lnkShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowInternationalDriverLicenseInfo internationalDriverLicenseInfo = new frmShowInternationalDriverLicenseInfo(_newInternationalLicense);
            internationalDriverLicenseInfo.ShowDialog();
        }
       

        private void ctrlFilterWithDriverLicenseInfoCard1_OnLicenseSelected(int obj)
        {
            _selectedLicense = obj;
            if (_selectedLicense == -1)
                return;
            lnkShowLicenseHistory.Enabled = (_selectedLicense != -1);

            if (ctrlFilterWithDriverLicenseInfoCard1.SelectedLicenseInfo == null)
            {
                MessageBox.Show("This License not Exists","Confirm",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            // 3 = Class 3 - Ordinary driving license .
            if (ctrlFilterWithDriverLicenseInfoCard1.SelectedLicenseInfo.LicenseClassID != 3)
            {
                MessageBox.Show(
           $"This license (ID: {ctrlFilterWithDriverLicenseInfoCard1.SelectedLicenseInfo.LicenseClassID}) is not allowed because it's not of Class 3.",
           "Not Allowed",
           MessageBoxButtons.OK,
           MessageBoxIcon.Warning);
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

            if(clsInternationalLicense.IsInternationalLicenseActive(ctrlFilterWithDriverLicenseInfoCard1.SelectedLicenseInfo.DriverID))
            {
                MessageBox.Show("This Person has a active international allready ", "Confirm", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            btnIssue.Enabled = true;
        }

        private void lnkShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowPersonLicenseHistory showPersonLicenseHistory = new frmShowPersonLicenseHistory(ctrlFilterWithDriverLicenseInfoCard1.SelectedLicenseInfo.DriverInfo.PersonID);
            showPersonLicenseHistory.ShowDialog();
        }

        private void frmInternationalLicenses_Load(object sender, EventArgs e)
        {
            SetupData();
        }
    }
}
