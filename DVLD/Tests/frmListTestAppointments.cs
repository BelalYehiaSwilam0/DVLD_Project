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
using DVLD.Properties;

namespace DVLD
{
    public partial class frmListTestAppointments : Form
    {
        DataTable _dtAllTestAppointments;
        clsTestAppointments _TestAppointment;
        clsTest _TestResult;
        private int _LocalDrivingLicenseApplicationID = 0;
        //private byte _TestType =0;
        string _NationalNo = string.Empty;
        DateTime _ApplicationDate = DateTime.Now;
        bool isTestTaken = false;
        private clsTestType.enTestType _TestType = clsTestType.enTestType.VisionTest;
        public frmListTestAppointments(int localDrivingLicenseApplicationID , clsTestType.enTestType TestType)
        {
            InitializeComponent();
            _LocalDrivingLicenseApplicationID=localDrivingLicenseApplicationID;
            _TestType=TestType;
        }
        private void _LoadTestTypeImageAndTitle()
        {
            switch (_TestType)
            {

                case clsTestType.enTestType.VisionTest:
                    {
                        lblTestTitle.Text = "Vision Test Appointments";
                        this.Text = lblTestTitle.Text;
                        pbTestTypeImage.Image = Resources.Vision_512;
                        break;
                    }

                case clsTestType.enTestType.WrittenTest:
                    {
                        lblTestTitle.Text = "Written Test Appointments";
                        this.Text = lblTestTitle.Text;
                        pbTestTypeImage.Image = Resources.Written_Test_512;
                        break;
                    }
                case clsTestType.enTestType.StreetTest:
                    {
                        lblTestTitle.Text = "Street Test Appointments";
                        this.Text = lblTestTitle.Text;
                        pbTestTypeImage.Image = Resources.driving_test_512;
                        break;
                    }
            }
        }
        private void _LoadInfo()
        {
            ctrlDrivingLicenseAndApplication_BasicInfo1.LoadApplicationInfoByLocalDrivingAppID(_LocalDrivingLicenseApplicationID);
           
        }
        void _LoadData()
        {
            _LoadTestTypeImageAndTitle();
            _dtAllTestAppointments = clsTestAppointments.GetTestAppointmentsByLocalDrivingLicenseAppIDAndTestTypeID(_LocalDrivingLicenseApplicationID, _TestType);
            dgvLicenseTestAppointments.DataSource = _dtAllTestAppointments;

            if (dgvLicenseTestAppointments.Rows.Count > 0)
            {
                dgvLicenseTestAppointments.Columns[0].HeaderText = "Appointment ID";
                dgvLicenseTestAppointments.Columns[0].Width = 150;

                dgvLicenseTestAppointments.Columns[1].HeaderText = "Appointment Date";
                dgvLicenseTestAppointments.Columns[1].Width = 200;

                dgvLicenseTestAppointments.Columns[2].HeaderText = "Paid Fees";
                dgvLicenseTestAppointments.Columns[2].Width = 150;

                dgvLicenseTestAppointments.Columns[3].HeaderText = "Is Locked";
                dgvLicenseTestAppointments.Columns[3].Width = 100;
            }

            _RecordsResults();
        }
        private void _RecordsResults()
        {
            lblRecordsResult.Text = dgvLicenseTestAppointments.Rows.Count.ToString();
        }
        private void pbAddNew_Click(object sender, EventArgs e)
        {
            clsLocalDrivingLicenseApplication localDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindLocalDrivingLicenseApplicationByID(_LocalDrivingLicenseApplicationID);

            if (localDrivingLicenseApplication.IsThereAnActiveScheduledTest(_TestType))
            {
                MessageBox.Show("Person Already have an active appointment for this test, You cannot add new appointment", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            clsTest LastTest = localDrivingLicenseApplication.GetLastTestPerTestType(_TestType);

            if (LastTest == null)
            {
                frmScheduleTest scheduleTest1 = new frmScheduleTest(localDrivingLicenseApplication.LocalDrivingLicenseApplicationID, _TestType);
                scheduleTest1.ShowDialog();
                frmTestScheduling_Load(null, null);
                return;
            }

            if(LastTest.TestResult)
            {
                MessageBox.Show("This person already passed this test before, you can only retake faild test", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            frmScheduleTest scheduleTest2 = new frmScheduleTest(localDrivingLicenseApplication.LocalDrivingLicenseApplicationID, _TestType);
            scheduleTest2.ShowDialog();
            frmTestScheduling_Load(null, null);


        }
        private void frmTestScheduling_Load(object sender, EventArgs e)
        {
            
            _LoadInfo();
            _LoadData();
        }
        private void stmEdit_Click_1(object sender, EventArgs e)
        {
            int TestAppointmentID =((int)dgvLicenseTestAppointments.CurrentRow.Cells[0].Value);

            frmScheduleTest Tests = new frmScheduleTest(_LocalDrivingLicenseApplicationID, _TestType,TestAppointmentID);
            Tests.ShowDialog();
            frmTestScheduling_Load(null, null);
        }
        private void tsmTakeTest_Click(object sender, EventArgs e)
        {
            frmTakeTest TakeTests = new frmTakeTest((int)dgvLicenseTestAppointments.CurrentRow.Cells[0].Value,(clsTestType.enTestType)_TestType);
            TakeTests.ShowDialog();
            frmTestScheduling_Load(null, null);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
