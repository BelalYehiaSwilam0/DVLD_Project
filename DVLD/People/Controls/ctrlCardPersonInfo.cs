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
using System.IO;
using DVLD.Properties;

namespace DVLD
{
    public partial class ctrlCardPersonInfo : UserControl
    {
       
        private int _PersonID = -1;
        public int PersonID
        {
            get { return _PersonID; }
        }

        private clsPerson _PersonInfo;
        public clsPerson SelectedPersonInfo
        {
            get { return _PersonInfo; }
        }

        public ctrlCardPersonInfo()
        {
            InitializeComponent();
        }
        private void _LoadPersonImage()
        {
            if (_PersonInfo.Gender == 0)
                pbPersonImage.Image = Resources.Male_512;
            else
                pbPersonImage.Image = Resources.Female_512;

            string ImagePath = _PersonInfo.ImagePath;

            if (ImagePath != "")
                if (File.Exists(ImagePath))
                    pbPersonImage.ImageLocation = ImagePath;
                else
                    MessageBox.Show("Could not find this image: = " + ImagePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void _FillPersonInfo()
        {
            _PersonID = _PersonInfo.PersonID;
            lblPersonIDResult.Text = _PersonInfo.PersonID.ToString();
            lblNameResult.Text = _PersonInfo.FullName;
            lblNationalNoResult.Text = _PersonInfo.NationalNo.ToString();
            lblGenderResult.Text = _PersonInfo.Gender == 0 ? "Male" : "Female";
            lblEmailResult.Text = string.IsNullOrEmpty(_PersonInfo.Email) ? "Is Empty" : _PersonInfo.Email;
            lblAddressResult.Text = _PersonInfo.Address;
            lblDateOfBirthResult.Text = _PersonInfo.DateOfBirth.ToString();
            lblPhoneReslult.Text = _PersonInfo.Phone;
            lblCountryResult.Text = clsCountry.Find(_PersonInfo.NationalityCountryID).CountryName;
            _LoadPersonImage();


        }
        public void _LoadPersonInfoByNationalNo(string NationalNo)
        {
            if(NationalNo != string.Empty)
            {
                _PersonInfo = clsPerson.Find(NationalNo);
                if (_PersonInfo == null)
                {
                    _ResetPersonInfo();
                    MessageBox.Show("No Person with National No. = " + NationalNo.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                
            }
            _FillPersonInfo();
        }
        public void _LoadPersonInfoByPersonID(int PersonID)
        {
            _PersonInfo = clsPerson.Find(PersonID);
            if (_PersonInfo == null)
            {
                _ResetPersonInfo();
                MessageBox.Show("No Person with PersonID = " + PersonID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _FillPersonInfo();
        }
        public void _ResetPersonInfo()
        {
            lblPersonIDResult.Text = string.Empty;
            lblNameResult.Text = string.Empty;
            lblNationalNoResult.Text = string.Empty;
            lblGenderResult.Text = string.Empty;
            lblEmailResult.Text = string.Empty;
            lblAddressResult.Text = string.Empty;
            lblDateOfBirthResult.Text = string.Empty;
            lblPhoneReslult.Text = string.Empty;
            lblCountryResult.Text = string.Empty;
            pbPersonImage.Image = Properties.Resources.Male_512;

        }
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmAddEditPersonInfo frm = new frmAddEditPersonInfo(_PersonID);
            frm.ShowDialog();

            //refresh
            _LoadPersonInfoByPersonID(_PersonID);
        }
    
    }
}
