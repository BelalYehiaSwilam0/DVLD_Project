using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using BusinessLayer_DVLD;
using DVLD.Applications.Local_Driving_License;
using DVLD_UserContext;

namespace DVLD
{
    public partial class frmManageLocalDrivingLicenseApplications : Form
    {
        DataTable _dtAllLocalDrivingLicenseApplications;
        clsTestType.enTestType _TestType = clsTestType.enTestType.VisionTest;
        public frmManageLocalDrivingLicenseApplications()
        {
            InitializeComponent();
            
        }
        void _LoadData()
        {
            _dtAllLocalDrivingLicenseApplications = clsLocalDrivingLicenseApplication.GetAllLocalDrivingLicenseApplications();
            dgvLocalDrivingLicenseApplications.DataSource = _dtAllLocalDrivingLicenseApplications;

            if (dgvLocalDrivingLicenseApplications.Rows.Count > 0)
            {

                dgvLocalDrivingLicenseApplications.Columns[0].HeaderText = "L.D.L.AppID";
                dgvLocalDrivingLicenseApplications.Columns[0].Width = 120;

                dgvLocalDrivingLicenseApplications.Columns[1].HeaderText = "Driving Class";
                dgvLocalDrivingLicenseApplications.Columns[1].Width = 300;

                dgvLocalDrivingLicenseApplications.Columns[2].HeaderText = "National No.";
                dgvLocalDrivingLicenseApplications.Columns[2].Width = 150;

                dgvLocalDrivingLicenseApplications.Columns[3].HeaderText = "Full Name";
                dgvLocalDrivingLicenseApplications.Columns[3].Width = 350;

                dgvLocalDrivingLicenseApplications.Columns[4].HeaderText = "Application Date";
                dgvLocalDrivingLicenseApplications.Columns[4].Width = 170;

                dgvLocalDrivingLicenseApplications.Columns[5].HeaderText = "Passed Tests";
                dgvLocalDrivingLicenseApplications.Columns[5].Width = 150;
            }
            _RecordsResults();
        }
        private void _RecordsResults()
        {
            lblRecordsResult.Text = dgvLocalDrivingLicenseApplications.Rows.Count.ToString();
        }
        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Visible = (cbFilterBy.Text != "None");

            if (txtFilterValue.Visible)
            {
                txtFilterValue.Text = "";
                txtFilterValue.Focus();
            }
        }
        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.Text == "L.D.L.AppID" || cbFilterBy.Text == "Passed Tests")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        private void frmLocalDrivingLicenseApplications_Load(object sender, EventArgs e)
        {
            _LoadData();
        }
        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterCoulmn = "";
            switch (cbFilterBy.Text)
            {
                case "L.D.L.AppID":
                    FilterCoulmn = "LocalDrivingLicenseApplicationID";
                    break;

                case "Driving Class":
                    FilterCoulmn = "ClassName";
                    break;

                case "Natinal No.":
                    FilterCoulmn = "NationalNo";
                    break;


                case "FullName":
                    FilterCoulmn = "FullName";
                    break;

                //case "Application Date":
                //    FilterCoulmn = "ApplicationDate";
                //    break;

                case "Passed Tests":
                    FilterCoulmn = "PassedTestCount";
                    break;

                case "Status":
                    FilterCoulmn = "Status";
                    break;


            }
            if (txtFilterValue.Text.Trim() == "" || cbFilterBy.Text == "None")
            {
                _dtAllLocalDrivingLicenseApplications.DefaultView.RowFilter = "";
                _RecordsResults();
                return;
            }

            if (FilterCoulmn == "LocalDrivingLicenseApplicationID" || FilterCoulmn == "PassedTestCount")

                _dtAllLocalDrivingLicenseApplications.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterCoulmn, txtFilterValue.Text.Trim());
           // i can't do a filter on date ...
           else
                _dtAllLocalDrivingLicenseApplications.DefaultView.RowFilter = string.Format("[{0}] LIKE '%{1}%'", FilterCoulmn, txtFilterValue.Text.Trim());

            _RecordsResults();
            return;
        }
        
        private void pbAddNew_Click(object sender, EventArgs e)
        {
            frmAddUpdateLocalDrivingLicesnseApplication newLocalDrivingLicenseApplication = new frmAddUpdateLocalDrivingLicesnseApplication();
            newLocalDrivingLicenseApplication.ShowDialog();
            frmLocalDrivingLicenseApplications_Load(null, null);
        }
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (dgvLocalDrivingLicenseApplications.CurrentRow != null)
            {
                _TestType = clsTestType.enTestType.VisionTest;
                frmListTestAppointments vitionTestAppointments = new frmListTestAppointments((int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value, _TestType);
                vitionTestAppointments.Text = "Vision Test";
                vitionTestAppointments.ShowDialog();
                frmLocalDrivingLicenseApplications_Load(null, null);
            }
            else
            {
                MessageBox.Show("Please select a valid row first.");
                return;
            }

        }
        int GetPersonIDByNationalNO()
        {
            return clsPerson.Find((string)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[2].Value).PersonID;
        }
        private void CancelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvLocalDrivingLicenseApplications.CurrentRow != null)
            {
                if (MessageBox.Show("Do you want to cancel this application ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;
                clsLocalDrivingLicenseApplication localDrivingLicenseApplication =
                    clsLocalDrivingLicenseApplication.FindLocalDrivingLicenseApplicationByID((int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value);
                if(localDrivingLicenseApplication == null)
                {
                    MessageBox.Show("Could not cancel application.","Error",MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                
                if(localDrivingLicenseApplication.Cancel())
                {
                    MessageBox.Show("Application cancelled successfully.","Cancelled",MessageBoxButtons.OK, MessageBoxIcon.Information);

                    //refresh the form again
                    frmLocalDrivingLicenseApplications_Load(null, null);
                }

            }
            else
            {
                MessageBox.Show("Please select an application to cancel.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
           
        }
        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvLocalDrivingLicenseApplications.CurrentRow != null)
            {
                if (MessageBox.Show("Are you sure do want to delete this application?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;

                clsLocalDrivingLicenseApplication LocalDrivingLicenseApplication =
               clsLocalDrivingLicenseApplication.FindLocalDrivingLicenseApplicationByID((int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value);

                if(LocalDrivingLicenseApplication == null)
                {
                    MessageBox.Show("Could not delete applicatoin, other data depends on it.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (LocalDrivingLicenseApplication.Delete())
                {
                    MessageBox.Show("Application Deleted Successfully.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //refresh the form again.
                    frmLocalDrivingLicenseApplications_Load(null, null);
                }
            }
            else
            {
                MessageBox.Show("Please select a valid row first.");
                return;
            }
           
        }
        private void toolSchedualWrittenTest_Click(object sender, EventArgs e)
        {
            if (dgvLocalDrivingLicenseApplications.CurrentRow != null)
            {
                _TestType = clsTestType.enTestType.WrittenTest;
                frmListTestAppointments vitionTestAppointments = new frmListTestAppointments((int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value, _TestType);
                vitionTestAppointments.Text = "Written Test";
                vitionTestAppointments.ShowDialog();
                frmLocalDrivingLicenseApplications_Load(null, null);
            }
            else
            {
                MessageBox.Show("Please select a valid row first.");
                return;
            }

        }
        private void toolSchedualStreetTest_Click(object sender, EventArgs e)
        {
            if (dgvLocalDrivingLicenseApplications.CurrentRow != null)
            {
                _TestType = clsTestType.enTestType.StreetTest;
                frmListTestAppointments vitionTestAppointments = new frmListTestAppointments((int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value, _TestType);
                vitionTestAppointments.Text = "Street Test";
                
                vitionTestAppointments.ShowDialog();
                frmLocalDrivingLicenseApplications_Load(null, null);
            }
            else
            {
                MessageBox.Show("Please select a valid row first.");
                return;
            }

        }
 
        private void cmsEditDelete_Opening(object sender, CancelEventArgs e)
        {
            if (dgvLocalDrivingLicenseApplications.CurrentRow == null)
            {
                e.Cancel = true;
                return;
            }

            clsLocalDrivingLicenseApplication localDrivingLicenseApplication 
                = clsLocalDrivingLicenseApplication.FindLocalDrivingLicenseApplicationByID((int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value);

            int TotalPassedTest = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[5].Value;
            bool IsLicenseExists = localDrivingLicenseApplication.IsLicenseIssued();

            tsmIssueDrivingLicenseFT.Enabled = (TotalPassedTest == 3) && !IsLicenseExists;
            editToolStripMenuItem.Enabled = !IsLicenseExists && (localDrivingLicenseApplication.ApplicationStatus == clsApplication.enApplicationStatus.New);
            toolSchdualTests.Enabled = !IsLicenseExists;
            CancelToolStripMenuItem.Enabled = (localDrivingLicenseApplication.ApplicationStatus == clsApplication.enApplicationStatus.New);
            tsmShowLicense.Enabled = IsLicenseExists;
            DeleteToolStripMenuItem.Enabled = (localDrivingLicenseApplication.ApplicationStatus == clsApplication.enApplicationStatus.New);

            //Enable Disable Schedule menue and it's sub menue
            bool PassedVisionTest = localDrivingLicenseApplication.DoesPassTestType(clsTestType.enTestType.VisionTest);
            bool PassedWrittenTest = localDrivingLicenseApplication.DoesPassTestType(clsTestType.enTestType.WrittenTest);
            bool PassedStreetTest = localDrivingLicenseApplication.DoesPassTestType(clsTestType.enTestType.StreetTest);

            toolSchdualTests.Enabled = (!PassedVisionTest || !PassedWrittenTest || !PassedStreetTest)&& (localDrivingLicenseApplication.ApplicationStatus == clsApplication.enApplicationStatus.New);


            if (toolSchdualTests.Enabled)
            {
                // to allow schedule vistion test , person must not passed the same test before.
                toolSchedualVistionTest.Enabled = !PassedVisionTest;

                // to allow schedule written test , person must allow vistion test and not passed the same test before.
                toolSchedualWrittenTest.Enabled = (PassedVisionTest && !PassedWrittenTest);
                // to allow schedule street test , person must allow vistion test and written test and not passed the same test before.
                toolSchedualStreetTest.Enabled = (PassedVisionTest&&PassedWrittenTest )&& !PassedStreetTest;

            }
          
        }
        private void tsmIssueDrivingLicenseFT_Click(object sender, EventArgs e)
        {
            if (dgvLocalDrivingLicenseApplications.CurrentRow != null)
            {
                frmIssueLocalDriverLicense issueDriverLicenseForTheFirstTime = new frmIssueLocalDriverLicense((int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value);
                issueDriverLicenseForTheFirstTime.ShowDialog();
                //refresh
                frmLocalDrivingLicenseApplications_Load(null, null);
            }
            else
            {
                MessageBox.Show("Please select a valid row first.");
                return;
            }
             
        }
        private void tsmShowLicense_Click(object sender, EventArgs e)
        {
            int LocalDrivingLicenseApplicationID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;
            int LocalLicenseID = clsLocalDrivingLicenseApplication.FindLocalDrivingLicenseApplicationByID(LocalDrivingLicenseApplicationID).GetActiveLicenseID();

            if (LocalLicenseID == -1)
            {
                MessageBox.Show(
                 "No local driving license was found for the selected application. " +
                 "Please verify the application details and try again.",
                 "License Not Found",
                 MessageBoxButtons.OK,
                 MessageBoxIcon.Warning);

                return;
            }

            frmShowLicense showLicense = new frmShowLicense(LocalLicenseID);
            showLicense.ShowDialog();
        }
        private void tsmShowPersonLicenseHistory_Click(object sender, EventArgs e)
        {
            
            frmShowPersonLicenseHistory showPersonLicenseHistory = new frmShowPersonLicenseHistory(GetPersonIDByNationalNO());
            showPersonLicenseHistory.ShowDialog();
        }

        private void showDetilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLocalDrivingLicenseApplicationInfo localDrivingLicenseApplicationInfo = new frmLocalDrivingLicenseApplicationInfo((int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value);
            localDrivingLicenseApplicationInfo.ShowDialog();

        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdateLocalDrivingLicesnseApplication newLocalDrivingLicenseApplication = new frmAddUpdateLocalDrivingLicesnseApplication((int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value);
            newLocalDrivingLicenseApplication.ShowDialog();
            frmLocalDrivingLicenseApplications_Load(null, null);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
