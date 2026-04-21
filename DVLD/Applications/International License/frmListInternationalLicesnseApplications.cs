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
using static System.Net.Mime.MediaTypeNames;

namespace DVLD
{
    public partial class frmListInternationalLicesnseApplications : Form
    {
        DataTable _dtAllInternationalLicenses;

        public frmListInternationalLicesnseApplications()
        {
            InitializeComponent();
        }
        void _LoadDataForInternationalLicenses()
        {
            _dtAllInternationalLicenses = clsInternationalLicense.GetAllInternationalLicenses();
             dgvInternationalLicenses.DataSource = _dtAllInternationalLicenses;
            if (_dtAllInternationalLicenses.Rows.Count > 0)
            {
                dgvInternationalLicenses.Columns[0].HeaderText = "Int.License ID";
                dgvInternationalLicenses.Columns[0].Width = 115;

                dgvInternationalLicenses.Columns[1].HeaderText = "ApplicationID";
                dgvInternationalLicenses.Columns[1].Width = 115;

                dgvInternationalLicenses.Columns[2].HeaderText = "DriverID";
                dgvInternationalLicenses.Columns[2].Width = 115;

                dgvInternationalLicenses.Columns[3].HeaderText = "L.License ID";
                dgvInternationalLicenses.Columns[3].Width = 115;

                dgvInternationalLicenses.Columns[4].HeaderText = "IssueDate";
                dgvInternationalLicenses.Columns[4].Width = 160;

                dgvInternationalLicenses.Columns[5].HeaderText = "ExpirationDate";
                dgvInternationalLicenses.Columns[5].Width = 160;

                dgvInternationalLicenses.Columns[6].HeaderText = "IsActive";
                dgvInternationalLicenses.Columns[6].Width = 130;

                _RecordsResultsForInternational();

            }
        }
        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
           if(cbFilterBy.Text == "Is Active")
            {
                txtFilterValue.Visible = false;
                cbIsActive.Visible = true;
                cbIsActive.Focus();
                cbIsActive.SelectedIndex = 0;
            }
           else
            {
                txtFilterValue.Visible = (cbFilterBy.Text != "None");
                cbIsActive.Visible = false;

                if (cbFilterBy.Text == "None")
                {
                    txtFilterValue.Enabled = false;
                }
                else
                txtFilterValue.Enabled = true;
                txtFilterValue.Text = "";
                txtFilterValue.Focus();
            }
        }
        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {

            string FilterColumn = "";
            switch (cbFilterBy.Text)
            {
                case "International License ID":
                    FilterColumn = "InternationalLicenseID";
                    break;

                case "ApplicationID":
                    FilterColumn = "ApplicationID";
                    break;

                case "Driver ID":
                    FilterColumn = "DriverID";
                    break;

                case "Local License ID":
                    FilterColumn = "IssuedUsingLocalLicenseID";
                    break;

                case "Is Active":
                    FilterColumn = "IsActive";
                    break;

                default:
                    FilterColumn = "None";
                    break;


            }
            if (txtFilterValue.Text.Trim() == "" || cbFilterBy.Text == "None")
            {
                _dtAllInternationalLicenses.DefaultView.RowFilter = "";
                _RecordsResultsForInternational();
                return;
            }
          
             _dtAllInternationalLicenses.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterValue.Text.Trim());
            _RecordsResultsForInternational();
        }
        private void _RecordsResultsForInternational()
        {
            lblRecordsResult.Text = dgvInternationalLicenses.Rows.Count.ToString();
        }
        private void frmManageInternationalLicenseApplications_Load(object sender, EventArgs e)
        {
            cbFilterBy.SelectedIndex = 0;
            _LoadDataForInternationalLicenses();
        }
        private void pbAddNew_Click(object sender, EventArgs e)
        {
            frmInternationalLicenses internationalLicenses = new frmInternationalLicenses();
            internationalLicenses.ShowDialog();
            frmManageInternationalLicenseApplications_Load(null, null);
        }

        private void cbIsActive_SelectedIndexChanged(object sender, EventArgs e)
        {
            string FilterCoulmn = "IsActive";
            string FilterValue = cbIsActive.Text;
            switch (FilterValue)
            {
                case "All":
                    break;
                case "Yes":
                    FilterValue = "1";
                    break;
                case "No":
                    FilterValue = "0";
                    break;
            }

            if (FilterValue == "All")
                _dtAllInternationalLicenses.DefaultView.RowFilter = "";
            else
                _dtAllInternationalLicenses.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterCoulmn, FilterValue);
            _RecordsResultsForInternational();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        int GetPersonID()
        {
            clsDriver Driverinfo = clsDriver.FindDriverInfoByID((int)dgvInternationalLicenses.CurrentRow.Cells[2].Value);
            if (Driverinfo == null)
                return 0;
            return Driverinfo.PersonID;
        }
      
        private void tsmShowInternationalLicense_Click(object sender, EventArgs e)
        {
            frmShowInternationalDriverLicenseInfo showInternationalDriverLicenseInfo = new frmShowInternationalDriverLicenseInfo((int)dgvInternationalLicenses.CurrentRow.Cells[0].Value);
            showInternationalDriverLicenseInfo.ShowDialog();

        }

        private void tsmShowPersonLicenseHistory_Click(object sender, EventArgs e)
        {
            int PersonID = GetPersonID();

            if (PersonID == 0)
                return;

            frmShowPersonLicenseHistory showPersonLicenseHistory = new frmShowPersonLicenseHistory(PersonID);
            showPersonLicenseHistory.ShowDialog();
        }

        private void tsmShowPersonDetails_Click(object sender, EventArgs e)
        {
            int PersonID = GetPersonID();

            if (PersonID == 0)
                return;

            frmCardPersonInfo showPersonDetails = new frmCardPersonInfo(PersonID);
            showPersonDetails.ShowDialog();

        }
    }
}
