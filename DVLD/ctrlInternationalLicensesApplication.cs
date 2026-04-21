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
    
    public partial class ctrlInternationalLicensesApplication : UserControl
    {
        clsApplication _ApplicationInfo;
        clsInternationalLicense _InternationalLicenseInfo;
        clsApplicationTypes _ApplicationTypeInfo;

        public event Action<int> AddNewInternationalLicense;
        protected virtual void OnInternationalLicenseID(int internationalLicenseID)
        {
            AddNewInternationalLicense?.Invoke(internationalLicenseID);
        }

        int _PersonID = 0;
        int _DriverID = 0;
        int _LocalLicenseID = 0;
        private enum enApplicationStatus
        {
            Completed = 3,
        }
        public ctrlInternationalLicensesApplication()
        {
            InitializeComponent();
           
        }

        private void LoadApplicationType()
        {
            _ApplicationTypeInfo = clsApplicationTypes.GetApplicationTypeInfoByName("New International License");

            if (_ApplicationTypeInfo == null)
            {
               // MessageBox.Show("Failed to Get New International License application. Please check the data and try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblFeesResult.Text = "Faild to get applicatoin fees";
                return;
            }

            lblFeesResult.Text = _ApplicationTypeInfo.ApplicationFees.ToString("N2") ?? "0.00";
        }
        public void AddNewInternationalLicenseApplication(int localLicenseID,int driverID ,int personID)
        {
            if (localLicenseID < 0)
            {
                MessageBox.Show("Invalid local license ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblLocalLicenseIDResult.Text = "[???]";
                return;
            }

            _LocalLicenseID= localLicenseID;
            _DriverID = driverID;
            _PersonID = personID;

            lblLocalLicenseIDResult.Text = localLicenseID.ToString();
            
        }
        public void SetupData()
        {
            DateTime startDate = DateTime.Now;
            lblApplicationDate.Text = startDate.ToString("dd/MMM/yyyy");
            lblExpirationDateResult.Text = startDate.AddYears(1).ToString("dd/MMM/yyyy");
            lblIssueDateResult.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            // lblFeesResult.Text = _ApplicationTypeInfo.ApplicationFees.ToString("N2") ?? "0.00";
            LoadApplicationType();
            lblCreatedbyResult.Text = clsUsers.FindUserByID(clsGlobal.CurrentUser.UserID).UserName;
        }
        void _InitializeNewApplication()
        {
            _ApplicationInfo = new clsApplication();
        }
        void _InitializeNewInternationalLicense()
        {
            _InternationalLicenseInfo = new clsInternationalLicense();
        }
        bool _SaveApplication()
        {
            _InitializeNewApplication();
            _ApplicationInfo.ApplicantPersonID = _PersonID;
            _ApplicationInfo.ApplicationDate = DateTime.Now;
            _ApplicationInfo.ApplicationTypeID = _ApplicationTypeInfo.ApplicationTypeID;
            _ApplicationInfo.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
            _ApplicationInfo.LastStatusDate = DateTime.Now;
            _ApplicationInfo.PaidFees = _ApplicationTypeInfo.ApplicationFees;
            _ApplicationInfo.CreatedByUserID = clsGlobal.CurrentUser.UserID;

            if(_ApplicationInfo.Save())
            {
                lblApplicationIDResult.Text = _ApplicationInfo.ApplicationID.ToString();

                _SaveInternationalLiceseInfo();
                return true;
            }
            else
            {
                MessageBox.Show("Failed to save application data. Please try again later.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                 return false;
            }

        }
        bool _SaveInternationalLiceseInfo()
        {
            _InitializeNewInternationalLicense();
            _InternationalLicenseInfo.ApplicationID = _ApplicationInfo.ApplicationID;
            _InternationalLicenseInfo.DriverID = _DriverID;
            _InternationalLicenseInfo.IssuedUsingLocalLicenseID = _LocalLicenseID;
            _InternationalLicenseInfo.IssueDate = DateTime.Now;
            _InternationalLicenseInfo.ExpirationDate = DateTime.Now.AddYears(1);
            _InternationalLicenseInfo.IsActive = DateTime.Now < _InternationalLicenseInfo.ExpirationDate;
            _InternationalLicenseInfo.CreatedByUserID = clsGlobal.CurrentUser.UserID;

            if (_InternationalLicenseInfo.Save())
            {
               lblILLicenseIDResult.Text = _InternationalLicenseInfo.InternationalLicenseID.ToString();
                OnInternationalLicenseID(_InternationalLicenseInfo.InternationalLicenseID);
                return true;
            }
            else
            {
                MessageBox.Show("Failed to issue international license. Please check the data and try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;  
            }
        }
        public bool IssueNewInternationalLicense()
        {
            return _SaveApplication();
        }

        
    }
}
