using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD.Global_Classes;
using DVLD_UserContext;

namespace DVLD
{
    public partial class frmMain : Form
    {
        frmLogin _frmLogin;
        public enum enMode { New = 1, Cancel = 2, Completed = 3 }
        enMode Mode = enMode.New;
        public frmMain(frmLogin login)
        {
            InitializeComponent();
            
            _frmLogin = login;
        }
      
        private void peopleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmManagePeople();
            frm.ShowDialog();
        }

        private void driversToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmManageDrivers manageDrivers = new frmManageDrivers();
            manageDrivers.ShowDialog();
        }

        private void employeesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmManageUsers();
            frm.ShowDialog();
        }

        private void currentUserInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUserInfo userInfo = new frmUserInfo(clsGlobal.CurrentUser.UserID);
            userInfo.ShowDialog();
        }

        private void toolStripMenuItem18_Click(object sender, EventArgs e)
        {
            frmChangePassword changePassword = new frmChangePassword(clsGlobal.CurrentUser.UserID);
            changePassword.ShowDialog();
        }

        private void toolStripMenuItem19_Click(object sender, EventArgs e)
        {
            clsEventLog.EventLogs("DVDL_Application", "Application", $"User '{clsGlobal.CurrentUser.UserName}' logged out successfully.", EventLogEntryType.Information);
            clsGlobal.CurrentUser = null;
            _frmLogin.Show();
            this.Close();
        }

        private void localLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdateLocalDrivingLicesnseApplication newLocalDrivingLicenseApplication = new frmAddUpdateLocalDrivingLicesnseApplication();
            newLocalDrivingLicenseApplication.ShowDialog();
        }

        private void internationalLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmInternationalLicenses internationalLicenses = new frmInternationalLicenses();
            internationalLicenses.ShowDialog();
        }

        private void renewDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRenewLocalDrivingLicense renewLocalDrivingLicense = new frmRenewLocalDrivingLicense();
            renewLocalDrivingLicense.ShowDialog();
        }

        private void ReplacementLostOrDamagedDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReplacementForLostOrDamagedLicense replacementForLostOrDamagedLicense = new frmReplacementForLostOrDamagedLicense();
            replacementForLostOrDamagedLicense.ShowDialog();
        }

        private void releaseDetainedDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRelaseDetainedLicense relaseDetainedLicense = new frmRelaseDetainedLicense();
            relaseDetainedLicense.ShowDialog();
        }

        private void retakeTestToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmManageLocalDrivingLicenseApplications RetakeTests = new frmManageLocalDrivingLicenseApplications();
            RetakeTests.ShowDialog();
        }

        private void manageLocalDrivingLicenseApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmManageLocalDrivingLicenseApplications localDrivingLicenseApplications = new frmManageLocalDrivingLicenseApplications();
            localDrivingLicenseApplications.ShowDialog();
        }

        private void ManageInternationaDrivingLicenseToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmListInternationalLicesnseApplications manageInternationalLicenseApplications = new frmListInternationalLicesnseApplications();
            manageInternationalLicenseApplications.ShowDialog();
        }

        private void ManageDetainedLicensestoolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmListDetainedLicenses manageDetainedLicenses = new frmListDetainedLicenses();
            manageDetainedLicenses.ShowDialog();
        }

        private void detainLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDetainLicense detainLicense = new frmDetainLicense();
            detainLicense.ShowDialog();
        }

        private void releaseDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRelaseDetainedLicense relaseDetainedLicense = new frmRelaseDetainedLicense();
            relaseDetainedLicense.ShowDialog();
        }

        private void manageApplicationTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListApplicationTypes applicationTypes = new frmListApplicationTypes();
            applicationTypes.ShowDialog();
        }

        private void manageTestTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListTestTypes manageTestTypes = new frmListTestTypes();
            manageTestTypes.ShowDialog();
        }

        private void tsmShowPersonLicenseHistory_Click(object sender, EventArgs e)
        {
            //frmShowPersonLicenseHistory showPersonLicenseHistory = new frmShowPersonLicenseHistory();
            //showPersonLicenseHistory.ShowDialog();

            MessageBox.Show("Not Emplemented yet", "Confirm", MessageBoxButtons.OK);
        }
    }
}
