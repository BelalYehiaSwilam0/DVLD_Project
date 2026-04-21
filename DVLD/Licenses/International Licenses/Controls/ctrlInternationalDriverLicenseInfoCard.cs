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

namespace DVLD
{
    public partial class ctrlInternationalDriverLicenseInfoCard : UserControl
    {
        clsInternationalLicense _internationalLicenseInfo;
        clsPerson _personInfo;
        clsDriver _driverInfo;
        public ctrlInternationalDriverLicenseInfoCard()
        {
            InitializeComponent();
        }

        public void ShowInternationalLicenseInfo(int internationalLicenseID)
        {
            if (!_FindInternationalLicenseByID(internationalLicenseID))
                _Reset();

        }
        bool _FindInternationalLicenseByID(int internationalLicenseID)
        {
            _internationalLicenseInfo = clsInternationalLicense.FindInternationalLicenseByID(internationalLicenseID);

            if (_internationalLicenseInfo == null)
            {
                MessageBox.Show($"No international license found with ID: {internationalLicenseID}",
                    "License Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return _FindDriverInfo(_internationalLicenseInfo.DriverID);
        }
        bool _FindDriverInfo(int driverID)
        {
            _driverInfo = clsDriver.FindDriverInfoByID(driverID);

            if (_driverInfo == null)
            {
                MessageBox.Show($"No driver found with ID: {driverID}",
                    "Driver Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!_LoadInternationalLicenseInfo())
            {
                MessageBox.Show("Failed to load international license info.",
                    "Data Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return _FindPersonInfoByPersonID(_driverInfo.PersonID);
        }
        bool _FindPersonInfoByPersonID(int personID)
        {
            _personInfo = clsPerson.Find(personID);

            if (_personInfo == null)
            {
                MessageBox.Show($"No person found with ID: {personID}",
                    "Person Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            _LoadPersonInfo();
            return true;
        }

        private void _Reset()
        {
            lblNameResult.Text = "[???]";
            lblNationalNoResult.Text = "[???]";
            lblGenderResult.Text = "[???]";
            lblDateOfBirthResult.Text = "[???]";
            lblIntLicenseIDResult.Text = "[???]";
            lblLicenseIDResult.Text = "[???]";
            lblIssueDateResult.Text = "[???]";
            lblIsActiveResult.Text = "[???]";
            lblApplicationIDResult.Text = "[???]";
            lblDriverIDResult.Text = "[???]";
            lblExpirationDateResult.Text = "[???]";
            pbPerson.Image = null;
        }

        private bool _LoadInternationalLicenseInfo()
        {
            if (_internationalLicenseInfo == null)
                return false;

            lblIntLicenseIDResult.Text = _internationalLicenseInfo.InternationalLicenseID.ToString();
            lblLicenseIDResult.Text = _internationalLicenseInfo.IssuedUsingLocalLicenseID.ToString();
            lblIssueDateResult.Text = _internationalLicenseInfo.IssueDate.ToString("dd/MMM/yyyy");
            lblApplicationIDResult.Text = _internationalLicenseInfo.ApplicationID.ToString();
            lblIsActiveResult.Text = _internationalLicenseInfo.IsActive ? "Yes" : "No";
            lblDriverIDResult.Text = _internationalLicenseInfo.DriverID.ToString();
            lblExpirationDateResult.Text = _internationalLicenseInfo.ExpirationDate.ToString("dd/MMM/yyyy");
            return true;

        }

        private void _LoadPersonInfo()
        {
            lblNameResult.Text = _personInfo.FullName;
            lblNationalNoResult.Text = _personInfo.NationalNo.ToString();
            lblGenderResult.Text = _personInfo.Gender == 0 ? "Male" : "Female";
            lblDateOfBirthResult.Text = _personInfo.DateOfBirth.ToString("dd/MMM/yyyy");

            if (!string.IsNullOrEmpty(_personInfo.ImagePath))
                pbPerson.ImageLocation = _personInfo.ImagePath;
            else
                pbPerson.Image = Properties.Resources.Male_512;
        }

    }
}
