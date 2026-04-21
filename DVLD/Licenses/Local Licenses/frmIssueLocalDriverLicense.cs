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
    public partial class frmIssueLocalDriverLicense : Form
    {
        private int _LocalDrivingLicenseApplicationID = 0;
        clsLocalDrivingLicenseApplication _LocalDrivingLicenseApplication;

        public delegate void DataBackEventHandler(object sender,bool IsCompleted);
        public event DataBackEventHandler Databack;
        enum enIssueLicenseTypes { FirstTime = 1, Renew =2, ReplacementforDamaged =3 ,ReplacementforLost = 4}
        enIssueLicenseTypes IssueLicenseTypes = enIssueLicenseTypes.FirstTime;
        public frmIssueLocalDriverLicense(int localDrivingLicenseApplicationID )
        {
            InitializeComponent();
            _LocalDrivingLicenseApplicationID=localDrivingLicenseApplicationID;

        }
        private bool _LoadData()
        {
            txtNotes.Focus();
            _LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindLocalDrivingLicenseApplicationByID(_LocalDrivingLicenseApplicationID);
            if( _LocalDrivingLicenseApplication == null )
            {
                MessageBox.Show("No Applicaiton with ID=" + _LocalDrivingLicenseApplicationID.ToString(), "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return false;
            }

            if(!_LocalDrivingLicenseApplication.PassedAllTests())
            {
                MessageBox.Show("Person Should Pass All Tests First.", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return false;
            }

            int LicenseID = _LocalDrivingLicenseApplication.GetActiveLicenseID();

            if(LicenseID != -1)
            {
                MessageBox.Show("Person already has License before with License ID=" + LicenseID.ToString(), "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return false;
            }

            ctrlDrivingLicenseAndApplication_BasicInfo1.LoadApplicationInfoByLocalDrivingAppID(_LocalDrivingLicenseApplicationID);

            return true;

        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnIssue_Click(object sender, EventArgs e)
        {
          
            int LicenseID = _LocalDrivingLicenseApplication.IssueLicenseForTheFirstTime(txtNotes.Text.Trim(),clsGlobal.CurrentUser.UserID);
           if(LicenseID != -1)
            {
                MessageBox.Show("License Issued Successfully with License ID = " + LicenseID.ToString(),
                   "Succeeded", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Close();
            }
            else
            {
                MessageBox.Show("License Was not Issued ! ",
                 "Faild", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmIssueLocalDriverLicense_Load(object sender, EventArgs e)
        {
            if (!_LoadData())
            {
                this.Close();
            }
        }
    }
}
