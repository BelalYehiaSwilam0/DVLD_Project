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
using DVLD_UserContext;

namespace DVLD
{
    public partial class ctrlReleaseDetainedLicense : UserControl
    {
        int _LocalLicenseID;
        clsLicense _localLicenseInfo;
        clsDetainedLicenses _detainedLicenses;

        clsApplication _NewApplication;
        clsApplicationTypes _ApplicationTypeInfo;
        public ctrlReleaseDetainedLicense()
        {
            InitializeComponent();
        }
        private enum enApplicationStatus
        {
            Completed = 3,
        }
        private void LoadApplicationType()
        {
            _ApplicationTypeInfo = clsApplicationTypes.GetApplicationTypeInfoByName("Release Detained Driving License");

            if (_ApplicationTypeInfo == null)
            {
                // MessageBox.Show("Failed to Get New International License application. Please check the data and try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblApplicationFeesResult.Text = "Faild to get applicatoin fees";
                return;
            }

            lblApplicationFeesResult.Text = _ApplicationTypeInfo.ApplicationFees.ToString("N2") ?? "0.00";
        }
        public void ReleaseDetainedLocalLicense(int localLicenseID)
        {

            if (localLicenseID <= 0)
            {
                MessageBox.Show("Please enter a valid local license ID.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblLicenseID.Text = "[???]";
                SetupData();
                return;
            }
            _LocalLicenseID = localLicenseID;

            if (!FindLocaLicenseByID(_LocalLicenseID))
            {
                MessageBox.Show($"No local license found with ID {_LocalLicenseID}.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SetupData();
                return;
            }

            if (!FindDetainedLiceseInfoByLicenseID())
            {
                MessageBox.Show("Failed to go get detained license info . Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SetupData();
                return;
            }
            ShowDetainedLicenseInfo();

            lblLicenseIDResult.Text = localLicenseID.ToString();
        }
        public void SetupData()
        {
            lblDetainIDResult.Text = "[???]";
            lblDetainDateResult.Text = "[???]";
            lblFineFeesResult.Text = "[???]";
            lblTotalFeesResult.Text = "[???]";
            LoadApplicationType();
            lblLicenseIDResult.Text = "[???]";
            lblCreatedbyResult.Text = clsUsers.FindUserByID(clsGlobal.CurrentUser.UserID).UserName;
        }
        void InitializeNewApplication()
        {
            _NewApplication = new clsApplication();
        }
        private int FindPersonIDFromDriverID(int driverID)
        {
            clsDriver driverInfo = clsDriver.FindDriverInfoByID(driverID);

            return driverInfo != null ? driverInfo.PersonID : 0;
        }
        bool FindLocaLicenseByID(int localLicenseID)
        {
            _localLicenseInfo = clsLicense.FindLicenseByID(localLicenseID);

            if (_localLicenseInfo == null)
            {
                return false;
            }
            return true;
        }
        bool SaveApplication()
        {
            InitializeNewApplication();
            _NewApplication.ApplicantPersonID = FindPersonIDFromDriverID(_localLicenseInfo.DriverID);
            _NewApplication.ApplicationDate = DateTime.Now;
            _NewApplication.ApplicationTypeID = _ApplicationTypeInfo.ApplicationTypeID;
            _NewApplication.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
            _NewApplication.LastStatusDate = DateTime.Now;
            _NewApplication.PaidFees = _ApplicationTypeInfo.ApplicationFees;
            _NewApplication.CreatedByUserID = clsGlobal.CurrentUser.UserID;

            if (_NewApplication.Save())
            {
                lblRApplicationIDResult.Text = _NewApplication.ApplicationID.ToString();

                SaveReleaceDetainedLicense();
                return true;
            }
            else
            {
                MessageBox.Show("Failed to save application data. Please try again later.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

        }
        bool FindDetainedLiceseInfoByLicenseID()
        {
            _detainedLicenses = clsDetainedLicenses.GetDetainedLicenseByLicenseID(_LocalLicenseID);
            return _detainedLicenses != null;
        }
        void ShowDetainedLicenseInfo()
        {
            if (_ApplicationTypeInfo == null)
            {
                LoadApplicationType();
            }

            if(_detainedLicenses != null)
            {
                lblDetainIDResult.Text = _detainedLicenses.DetainID.ToString();
                lblDetainDateResult.Text = _detainedLicenses.DetainDate.ToString("dd/MMM/yyyy");
                lblFineFeesResult.Text = _detainedLicenses.FineFees.ToString();
            }

            if (_detainedLicenses != null && _ApplicationTypeInfo != null)
            {
                lblTotalFeesResult.Text = (_detainedLicenses.FineFees + _ApplicationTypeInfo.ApplicationFees).ToString("N2");
                return;
            }

            lblTotalFeesResult.Text = "0.00";
            MessageBox.Show("Failed to calculate total fees. Missing license or application fee info.",
                            "Calculation Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
        }
        void ReleaceDatainedLicense()
        {
            if (_detainedLicenses == null)
            {
                MessageBox.Show("No detained license found for the selected ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SetupData();
                return;
            }
            _detainedLicenses.IsReleased = true;
            _detainedLicenses.ReleaseDate = DateTime.Now;
            _detainedLicenses.ReleasedByUserID = clsGlobal.CurrentUser.UserID;
            _detainedLicenses.ReleaseApplicationID = _NewApplication.ApplicationID;
        }
        bool SaveReleaceDetainedLicense()
        {
            ReleaceDatainedLicense();

            if (_detainedLicenses == null)
            {
                MessageBox.Show("No detained license found for the selected ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (_detainedLicenses.Save())
            {
                MessageBox.Show("License has been successfully released.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            return false;
        }
        public bool IssueReleaseDetainLicense()
        {
            return SaveApplication();
        }

       
    }
}
