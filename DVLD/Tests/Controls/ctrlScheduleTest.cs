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
using static System.Net.Mime.MediaTypeNames;
using DVLD_UserContext;

namespace DVLD
{
    public partial class ctrlScheduleTest : UserControl
    {
        private clsLocalDrivingLicenseApplication _localDrivingLicenseApplication;
        private int _LocalDrivingLicenseApplicationID = -1;
        clsTestAppointments _testAppointment;
        private int _testAppointmentID = -1;

        private enum enMode { AddNew = 1, Update = 2 }
        enMode _Mode = enMode.AddNew;

        public enum enCreationMode { FirstTimeSchedule = 0 , RetakeTestSchedule = 1}
        private enCreationMode _creationMode = enCreationMode.FirstTimeSchedule;

        private clsTestType.enTestType _TestTypeID = clsTestType.enTestType.VisionTest;
        public ctrlScheduleTest()
        {
            InitializeComponent();
            
        }
        public clsTestType.enTestType TestTypeID
        {
            get { return _TestTypeID; }

            set
            {
                _TestTypeID = value;

                switch( _TestTypeID )
                {
                    case clsTestType.enTestType.VisionTest:
                        {
                            gbTestType.Text = "Vision Test";
                            pictureBox1.Image = Resources.Vision_512;
                            break;
                        }
                    case clsTestType.enTestType.WrittenTest:
                        {
                            gbTestType.Text = "Written Test";
                            pictureBox1.Image = Resources.Written_Test_512;
                            break;
                        }
                    case clsTestType.enTestType.StreetTest:
                        {
                            gbTestType.Text = "Street Test";
                            pictureBox1.Image = Resources.driving_test_512;
                            break;
                        }

                }
            }
        }

        private void _InitializeANewAppointment()
        {
            _testAppointment = new clsTestAppointments();
        }
        private bool _InitializeUpdateModeToEditTestAppointment()
        {
            _testAppointment = clsTestAppointments.FindTestAppointmentByID(_testAppointmentID);

            if(_testAppointment == null )
            {
                MessageBox.Show("Error: No Application with iD = " + _testAppointmentID.ToString(),
                    "Error",MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnSave.Enabled = false;
                return false;
            }

            lblFeesResult.Text = _testAppointment.PaidFees.ToString();

            //we compare the current date with the appointment date to set the min date.

            if (DateTime.Compare(DateTime.Now, _testAppointment.AppointmentDate) < 0)
                dtpTestDate.MinDate = DateTime.Now;
            else
                dtpTestDate.MinDate = _testAppointment.AppointmentDate;

            dtpTestDate.Value = _testAppointment.AppointmentDate;

            if(_testAppointment.RetakeTestApplicationID == -1)
            {
                lblRAPPFeesResult.Text = "0";
                lblRTestAppIDResult.Text = "N/A";
            }
            else
            {
                lblRAPPFeesResult.Text = _testAppointment.RetakeTestApplicationInfo.PaidFees.ToString();
                gbRetakeTestInfo.Enabled = true;
                lblScheduleTestTitle.Text = "Schedule Retake Test";
                lblRTestAppIDResult.Text = _testAppointment.RetakeTestApplicationID.ToString();
            }
            return true;
            
        }

        private bool _HandleActiveTestAppointmentConstraint()
        {
            if(_Mode == enMode.AddNew && clsLocalDrivingLicenseApplication.IsThereAnActiveScheduledTest(_LocalDrivingLicenseApplicationID, TestTypeID))
            {
                lblUserMessage.Text = "Person Already have an active appointment for this test";
                btnSave.Enabled = false;
                dtpTestDate.Enabled = false;
                return false;
            }
            return true;
        }
        private bool _HandleAppointmentLockedConstraint()
        {
            // if appointment is locked that means the person already sat for this test
            // we cannot update locked appointment

            if(_testAppointment.IsLocked)
            {
                lblUserMessage.Text = "Person already sat for the test, appointment loacked.";
                lblUserMessage.Visible = true;
                dtpTestDate.Enabled = false;
                btnSave.Enabled = false;
                return false;
            }

            else
                lblUserMessage.Visible = false;

            return true;
        }
        private bool _HandlePrviousTestConstraint()
        {
            // we need to make sure that this person passed the prvious test before apply to the new  test.
            // person cannot apply for written test unless s/he passes the vision test.
            // person cannot apply street test unless s/he passes the written test.

            switch(TestTypeID)
            {
                case clsTestType.enTestType.VisionTest:
                    lblUserMessage.Visible = false;
                    return true;
                case clsTestType.enTestType.WrittenTest:
                    // you cannot schedule it before person passes the vision test
                    //we check if pass vision test
                    if (!_localDrivingLicenseApplication.DoesPassTestType(clsTestType.enTestType.VisionTest))
                    {
                        lblUserMessage.Text = "Cannot Sechule, Vision Test should be passed first";
                        lblUserMessage.Visible = true;
                        btnSave.Enabled = false;
                        dtpTestDate.Enabled = false;
                        return false;
                    }
                    else
                    {
                        lblUserMessage.Visible = false;
                        btnSave.Enabled = true;
                        dtpTestDate.Enabled = true;
                    }
                    return true;
                case clsTestType.enTestType.StreetTest:
                    // you cannot schedule it before person passes the WrittenTest test
                    //we check if pass WrittenTest test
                    if (!_localDrivingLicenseApplication.DoesPassTestType(clsTestType.enTestType.WrittenTest))
                    {
                        lblUserMessage.Text = "Cannot Sechule, Written Test should be passed first";
                        lblUserMessage.Visible = true;
                        btnSave.Enabled = false;
                        dtpTestDate.Enabled = false;
                        return false;
                    }
                    else
                    {
                        lblUserMessage.Visible = false;
                        btnSave.Enabled = true;
                        dtpTestDate.Enabled = true;
                    }
                    return true;
            }
            return true;
        }

        private bool _HandleRetakeApplication()
        {
            // this will decide to create a seperater application for retake test or not .
            // and will  create it if needed , then it will linkit to the appointment.

            if(_Mode == enMode.AddNew && _creationMode == enCreationMode.RetakeTestSchedule)
            {
                // Incase the mode is add new  and creation mode is retake test we should create a seperate appliction for it.
                 //then we linked it with the appointment.

                clsApplication application = new clsApplication();
                application.ApplicantPersonID = _localDrivingLicenseApplication.ApplicantPersonID;
                application.ApplicationDate = DateTime.Now;
                application.ApplicationTypeID = (int)clsApplication.enApplicationType.RetakeTest;
                application.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
                application.LastStatusDate = DateTime.Now;
                application.PaidFees = clsApplicationTypes.GetApplicationTypeInfoByID((int)clsApplication.enApplicationType.RetakeTest).ApplicationFees;
                application.CreatedByUserID = clsGlobal.CurrentUser.UserID;

                if(!application.Save())
                {
                    _testAppointment.RetakeTestApplicationID = -1;

                    MessageBox.Show("Faild to Create application", "Faild", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                _testAppointment.RetakeTestApplicationID = application.ApplicationID;
            }
            return true;
        }
        public void LoadInfo(int localDrivingLicenseApplicationID, int AppointmentID = -1)
        {
            //if no appointment id  this means AddNew mode otherwise it's update mode .

            if (AppointmentID == -1)
                _Mode = enMode.AddNew;
            else
                _Mode = enMode.Update;

            _LocalDrivingLicenseApplicationID = localDrivingLicenseApplicationID;
            _testAppointmentID = AppointmentID;

            _localDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindLocalDrivingLicenseApplicationByID(_LocalDrivingLicenseApplicationID);

            if( _localDrivingLicenseApplication == null )
            {
                MessageBox.Show("Error: No local Driving License Application with ID = " + _LocalDrivingLicenseApplicationID.ToString(),
                    "Error ",MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnSave.Enabled = false;
                return;
            }

            // decide if the creation mode is retake test or not based if the person attended this test before.

            if (_localDrivingLicenseApplication.DoesAttendTestType(_TestTypeID))
                _creationMode = enCreationMode.RetakeTestSchedule;
            else
                _creationMode = enCreationMode.FirstTimeSchedule;

            if(_creationMode == enCreationMode.RetakeTestSchedule)
            {
                lblRAPPFeesResult.Text = clsApplicationTypes.GetApplicationTypeInfoByID((int)clsApplication.enApplicationType.RetakeTest).ApplicationFees.ToString();
                gbRetakeTestInfo.Enabled = true;
                lblScheduleTestTitle.Text = "Schedule Retake Test";
                lblRTestAppIDResult.Text = "0";
            }
            else
            {
                gbRetakeTestInfo.Enabled=false;
                lblScheduleTestTitle.Text = "Schedule  Test";
                lblRAPPFeesResult.Text = "0";
                lblRTestAppIDResult.Text = "N/A";
            }

            lblDLAPPIDResult.Text = _localDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();
            lblDClass.Text = _localDrivingLicenseApplication.LicenseClassInfo.ClassName;
            lblNameResult.Text = _localDrivingLicenseApplication.PersonFullName;

            //this will show the trials for this test before 
            lblTrialResult.Text = _localDrivingLicenseApplication.TotalTrialsPerTest(TestTypeID).ToString();

            if(_Mode == enMode.AddNew)
            {
                lblFeesResult.Text = clsTestType.Find(TestTypeID).TestTypeFees.ToString();
                dtpTestDate.MinDate = DateTime.Now;
                lblRTestAppIDResult.Text = "N/A";
                _InitializeANewAppointment();
            }
            else
            {
                if (!_InitializeUpdateModeToEditTestAppointment())
                    return;
            }

            lblTotalFeesResult.Text = (Convert.ToDecimal(lblFeesResult.Text) + Convert.ToDecimal(lblRAPPFeesResult.Text)).ToString();

            if (!_HandleActiveTestAppointmentConstraint())
                return;

            if (!_HandleAppointmentLockedConstraint())
                return;

            if (!_HandlePrviousTestConstraint())
                return;

        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!_HandleRetakeApplication())
                return;
            _testAppointment.TestTypeID = TestTypeID;
            _testAppointment.LocalDrivingLicenseApplicationID = _localDrivingLicenseApplication.LocalDrivingLicenseApplicationID;
            _testAppointment.AppointmentDate = dtpTestDate.Value;
            _testAppointment.PaidFees = Convert.ToDecimal(lblFeesResult.Text);
            _testAppointment.CreatedByUserID = clsGlobal.CurrentUser.UserID;

            if (_testAppointment.Save())
            {
                _Mode = enMode.Update;
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

    }
}
