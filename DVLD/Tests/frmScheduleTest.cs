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
using static DVLD.ctrlScheduleTest;

namespace DVLD
{
    public partial class frmScheduleTest : Form
    {
        int _localDrivingLicenseApplicationID = 0;
        clsTestType.enTestType _testType = clsTestType.enTestType.VisionTest;
        int _testAppointmentID = 0;
        public frmScheduleTest(int LocalDrivingLicenseApplicationID, clsTestType.enTestType TestTypeID , int testAppointmentID = -1)
        {
            InitializeComponent();
            _localDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            _testType = TestTypeID;
            _testAppointmentID = testAppointmentID;
        }

        void _LoadData()
        {
            ctrlScheduleTest1.TestTypeID = _testType;
            ctrlScheduleTest1.LoadInfo(_localDrivingLicenseApplicationID,_testAppointmentID);
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void frmTests_Load(object sender, EventArgs e)
        {
            _LoadData();
        }
    }
}
