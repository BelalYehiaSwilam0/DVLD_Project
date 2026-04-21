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
    public partial class frmTakeTest : Form
    {
        private clsTest _Test;
        private int _testID = -1;
        private clsTestAppointments _testAppointment;

        public clsTestType.enTestType _TestTypeID = clsTestType.enTestType.VisionTest;

        int _TestAppointmentID = 0;
        public frmTakeTest(int testAppointmentiD,clsTestType.enTestType testType)
        {
            InitializeComponent();
            _TestTypeID = testType;
            _TestAppointmentID = testAppointmentiD;

        }
        void _InitializeNewTest()
        {
            _Test = new clsTest();
        }
        void _LoadData()
        {
            ctrlTakeTest1.TestTypeID = _TestTypeID;
            ctrlTakeTest1.LoadInfo(_TestAppointmentID);

            if(ctrlTakeTest1.TestAppointmentID == -1)
                btnSave.Enabled = false;
            else
                btnSave.Enabled = true;

            _testID = ctrlTakeTest1.TestID;

            if (_testID != -1)
            {
                _Test = clsTest.Find(_testID);

                if (_Test.TestResult)
                    rbPass.Checked = true;
                else
                    rbFail.Checked = true;
                txtNotes.Text = _Test.Notes.Trim();

                lblUserMessage.Visible = true;
                rbPass.Enabled = false;
                rbFail.Enabled = false;
                btnSave.Enabled = false;
            }
            else
                _InitializeNewTest();
        }
       
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to save? After that you cannot change the Pass/Fail results after you save?.",
                      "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;


            _Test.TestAppointmentID = _TestAppointmentID;
            _Test.TestResult = rbPass.Checked ?true : false;
            _Test.Notes = txtNotes.Text.Trim();
            _Test.CreatedByUserID = clsGlobal.CurrentUser.UserID;

            if(_Test.Save())
            {
                // ctrlTakeTest1.SetTestIDResult(_Test.TestID);

                MessageBox.Show("Test Result Saved Successfully", "Done", MessageBoxButtons.OK);
                ctrlTakeTest1.TestIDValue(_Test.TestID);
                btnSave.Enabled = false;
                return;
            }
            else
            {
                MessageBox.Show("Test Result not Saved Successfully", "Error", MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }

        }
        private void frmTakeTest_Load(object sender, EventArgs e)
        {
            _LoadData();
        }
       
    }
}
