using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessLayer_DVLD;
using DVLD.Global_Classes;
using DVLD.Properties;

namespace DVLD
{
    public partial class ctrlDriverLicenseInfoCard : UserControl
    {

        private clsLicense _License;
        private int _licenseID = -1;

        public int LicenseID
        {
            get { return _licenseID;  }
        }
        public clsLicense SelectedLicenseInfo
        {
            get { return _License; }
        }
        public ctrlDriverLicenseInfoCard()
        {
            InitializeComponent();
        }
       
        public void GetLicenseInfoByID(int licenseID)
        {
            if (licenseID == 0)
            {
                _Reset();
                return;
            }

            _License = clsLicense.FindLicenseByID(licenseID);
            if (_License == null)
                return;

            _LoadInfo();
           
        }
       
      
        private void _Reset()
        {
            lblClassNameResult.Text = "[???]";
            lblNameResult.Text = "[???]";
            lblNationalNoResult.Text = "[???]";
            lblGenderResult.Text = "[???]";
            lblDateOfBirthResult.Text = "[???]";
            lblIsDetainedReslult.Text = "[???]";
            lblLicenseIDResult.Text = "[???]";
            lblIssueDateResult.Text = "[???]";
            lblIssueReasonResult.Text = "[???]";
            lblNoteResult.Text = "[???]";
            lblIsActiveResult.Text = "[???]";
            lblDriverIDResult.Text = "[???]";
            lblExpirationDateResult.Text = "[???]";
            pbPersonImage.Image = null;
        }

        private void _LoadPersonImage()
        {
            if(_License.DriverInfo.PersonInfo.Gender == 0)
                pbPersonImage.Image = Resources.Male_512;
            else
                pbPersonImage.Image = Resources.Female_512;

            string ImagePath = _License.DriverInfo.PersonInfo.ImagePath;

          if(ImagePath != "")
            {
                if(File.Exists(ImagePath))
                    pbPersonImage.Load(ImagePath);
                else
                    MessageBox.Show("Could not find this image: = " + ImagePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void _LoadInfo()
        {
            lblClassNameResult.Text = _License.LicenseClassInfo.ClassName;
            lblLicenseIDResult.Text = _License.LicenseID.ToString();
            lblNationalNoResult.Text = _License.DriverInfo.PersonInfo.NationalNo;
            lblNameResult.Text = _License.DriverInfo.PersonInfo.FullName;
            lblGenderResult.Text = _License.DriverInfo.PersonInfo.Gender == 0 ? "Male" : "Female";
            lblDateOfBirthResult.Text = clsFormat.DateToShort(_License.DriverInfo.PersonInfo.DateOfBirth);
            lblIssueDateResult.Text = clsFormat.DateToShort(_License.IssueDate);
            lblIssueReasonResult.Text = _License.IssueReasonText;
            lblNoteResult.Text = _License.Notes;
            lblIsActiveResult.Text = _License.IsActive ? "Yes" : "No";
            lblDriverIDResult.Text = _License.DriverID.ToString();
            lblExpirationDateResult.Text = clsFormat.DateToShort(_License.ExpirationDate);
            lblIsDetainedReslult.Text = _License.IsDetained ? "Yes" : "No";

            _LoadPersonImage();
        }
        
    }
}
