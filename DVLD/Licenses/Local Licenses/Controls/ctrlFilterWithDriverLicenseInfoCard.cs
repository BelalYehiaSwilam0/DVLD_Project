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

namespace DVLD
{
    public partial class ctrlFilterWithDriverLicenseInfoCard : UserControl
    {
        private int _LicenseID = -1;

        private bool _FilterEnabled = true;
        public bool FilterEnabled
        {
            get { 
                return _FilterEnabled; 
            }

            set {
                _FilterEnabled = value;
                gpFilter.Enabled = _FilterEnabled;
                }
        }

        public clsLicense SelectedLicenseInfo
        {
            get { return ctrlDriverLicenseInfoCard1.SelectedLicenseInfo; }
        }

        public event Action<int> OnLicenseSelected;
        protected virtual void LicenseSelected(int licenseID)
        {
            OnLicenseSelected?.Invoke(licenseID);
        }
        public ctrlFilterWithDriverLicenseInfoCard()
        {
            InitializeComponent();
        }

        public void LoadLiceseInfo(int licenseID)
        {
            txtFilterValue.Text = licenseID.ToString();
            ctrlDriverLicenseInfoCard1.GetLicenseInfoByID(licenseID);
            _LicenseID = ctrlDriverLicenseInfoCard1.LicenseID;


            if(OnLicenseSelected != null && FilterEnabled)
                OnLicenseSelected(licenseID);
        }
        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);

            if(e.KeyChar == (char)13)
                btnFind.PerformClick();
        }

        public void TxtLicenseIDFocus()
        {
            txtFilterValue.Focus();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            if(!ValidateChildren())
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtFilterValue.Focus();
                return;
            }

            _LicenseID = int.Parse(txtFilterValue.Text);
            LoadLiceseInfo(_LicenseID);
        }

        private void btnFind_Validating(object sender, CancelEventArgs e)
        {
            if(string.IsNullOrEmpty(txtFilterValue.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFilterValue, "This field is required!");

            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtFilterValue, null); 
            }
        }
    }
}
