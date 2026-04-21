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
using DVLD.Properties;
using static System.Net.Mime.MediaTypeNames;

namespace DVLD
{
    public partial class ctrlSecheduledTest : UserControl
    {

        private clsLocalDrivingLicenseApplication _localDrivingLicenseApplication;
        private int _localDrivingLicenseApplicationID = -1;

        private clsTestType.enTestType _TestTypeID = clsTestType.enTestType.VisionTest;

        private clsTestAppointments _testAppointment;

        private int _testAppointmentID = -1;
        public int TestAppointmentID
        {
            get { return _testAppointmentID; }
        }

        private int _testID = -1;
        public int TestID
        {
            get { return _testID; }
        }

        public ctrlSecheduledTest()
        {
            InitializeComponent();
        }
        public clsTestType.enTestType TestTypeID
        {
            get
            {
                return _TestTypeID;
            }
            set
            {
                _TestTypeID = value;

                switch (_TestTypeID)
                {

                    case clsTestType.enTestType.VisionTest:
                        {
                            gbTestType.Text = "Vision Test";
                            pbTestTypeImage.Image = Resources.Vision_512;
                            break;
                        }

                    case clsTestType.enTestType.WrittenTest:
                        {
                            gbTestType.Text = "Written Test";
                            pbTestTypeImage.Image = Resources.Written_Test_512;
                            break;
                        }
                    case clsTestType.enTestType.StreetTest:
                        {
                            gbTestType.Text = "Street Test";
                            pbTestTypeImage.Image = Resources.driving_test_512;
                            break;


                        }
                }
            }
        }
       
        public void  LoadInfo(int testAppointmentID)
        {
          _testAppointmentID = testAppointmentID;

            _testAppointment = clsTestAppointments.FindTestAppointmentByID(testAppointmentID);

           if( _testAppointment == null )
            {
                MessageBox.Show("Error: No  Appointment ID = " + _testAppointmentID.ToString(),
                   "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _testAppointmentID = -1;
                return;
            }
           _testID = _testAppointment.TestID;
            _localDrivingLicenseApplicationID = _testAppointment.LocalDrivingLicenseApplicationID;
            _localDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindLocalDrivingLicenseApplicationByID(_localDrivingLicenseApplicationID);

            if(_localDrivingLicenseApplication == null )
            {
                MessageBox.Show("Error: No Local Driving License Application with ID = " + _localDrivingLicenseApplicationID.ToString(),
                   "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            lblDLAPPIDResult.Text = _localDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();
            lblDClass.Text = _localDrivingLicenseApplication.LicenseClassInfo.ClassName;
            lblNameResult.Text = _localDrivingLicenseApplication.PersonFullName;

            lblTrialResult.Text = _localDrivingLicenseApplication.TotalTrialsPerTest(_TestTypeID).ToString();

            lblDateResult.Text = clsFormat.DateToShort(_testAppointment.AppointmentDate);
            lblFeesResult.Text = _testAppointment.PaidFees.ToString();
            lblTestIDResult.Text = (_testAppointment.TestID ==-1)? "Not Taken Yet" :_testAppointment.TestID.ToString();
        }

        public void TestIDValue(int TestID)
        {
            _testID = TestID;
            lblTestIDResult.Text = _testID.ToString();
        }
      
    }
}
