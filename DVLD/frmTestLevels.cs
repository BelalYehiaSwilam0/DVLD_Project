using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD.Properties;

namespace DVLD
{
    public partial class frmLevelsTestAppointments : Form
    {
        int _LocalDrivingLicenseApplication = 0;
        string _NatioanlNo = string.Empty;
        DateTime _ApplicationsDateTime = DateTime.MinValue;

        private enum enTestType { VisionTest = 1, WrittenTest = 2, StreetTest = 3 };
        private enTestType _TestType = enTestType.VisionTest;

        public frmLevelsTestAppointments(int localDrivingLicenseApplication, string NatioanlNo, DateTime ApplicationsDateTime , byte testType)
        {
            InitializeComponent();
            this._LocalDrivingLicenseApplication=localDrivingLicenseApplication;
            this._NatioanlNo=NatioanlNo;
            this._ApplicationsDateTime=ApplicationsDateTime;
            this._TestType = (enTestType)testType;
        }
        void _CheckTestType(enTestType testType)
        {
            switch (testType)
            {
                case enTestType.VisionTest:
                    _TestType = enTestType.VisionTest;
                    pbForTest.Image = Resources.Vision_512;
                    lblTitleForTest.Text = "Vision Test Appointments";
                    break;
                case enTestType.WrittenTest:
                    _TestType = enTestType.WrittenTest;
                    pbForTest.Image = Resources.Written_Test_512;
                    lblTitleForTest.Text = "Written Test Appointments";
                    break;
                case enTestType.StreetTest:
                    _TestType = enTestType.StreetTest;
                    pbForTest.Image = Resources.driving_test_512;
                    lblTitleForTest.Text = "Street Test Appointments";
                    break;
            }
        }
        void _LoadData()
        {
            _CheckTestType(_TestType);
            ctrlDrivingLicenseAndApplication_BasicInfo1.GetApplicationInfo(_NatioanlNo,_ApplicationsDateTime);
            ctrlDrivingLicenseAndApplication_BasicInfo1.GetDrivingLicenseApplicationInfo(_LocalDrivingLicenseApplication);
            ctrlAppointments2.IsPersonHasAnActiveAppointmentForThisTest((byte)_TestType, _LocalDrivingLicenseApplication);
        }
        private void frmVitionTestAppointments_Load(object sender, EventArgs e)
        {
            _LoadData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
