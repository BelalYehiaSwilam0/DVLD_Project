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
    public partial class frmTest : Form
    {
        
        
        public frmTest()
        {
               InitializeComponent();
            ctrlScheduleTest1.TestTypeID = clsTestType.enTestType.WrittenTest;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmListApplicationTypes applicationTypes = new frmListApplicationTypes();
            applicationTypes.ShowDialog();
        }

        void LoadDate()
        {
            //ctrlDrivingLicenseApplicationInfo2.LocalDrivingLicenseAppInfo(30);
            //ctrlApplicationBasicInfo1.InitializeApplicationInfo("N1", DateTime.Parse("2023-09-23 20:17:32.483"));
            //frmVisionTest visionTest = new frmVisionTest("N1", DateTime.Parse("2023-09-23 20:17:32.483"));
            //visionTest.ShowDialog();
            //clsApplication.DeleteApplicationIDByID(clsApplication.FindApplicationByApplicationDate(1041,Convert.ToDateTime("2025-05-04 15:50:45.933"))._ApplicationID);

            //frmVisionTest visionTest = new frmVisionTest(30,1);
            //visionTest.ShowDialog();
            // ctrlTest1.LoadTestInfo(Resources.driving_test_512);
            
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

          
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //private void frmTest_Load(object sender, EventArgs e)
        //{
        //    LoadDate();
        //}
    }
}
