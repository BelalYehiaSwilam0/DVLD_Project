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
    public partial class ctrlDriverLicenseHistory : UserControl
    {
        DataTable _dtAllLocalLicenses;
        DataTable _dtAllInternationalLicenses;
        private clsDriver _Driver;

        int _DriverID = -1;
        int _personID = -1;
        public ctrlDriverLicenseHistory()
        {
            InitializeComponent();
        }
        public void LoadLicensesByDriverID(int driverID)
        {
            _DriverID = driverID;
            if (_DriverID == -1)
                return;
            _Driver = clsDriver.FindDriverInfoByID(_DriverID);
            _LoadData();
        }

        public void LoadLicensesByPersonID(int personID)
        {
            _personID = personID;
            if (_personID == -1)
                return;
            _Driver = clsDriver.FindDriverInfoByPersonID(_personID);
            _LoadData();
        }
        void _LoadDataForLocalLicenses()
        {
            _dtAllLocalLicenses = clsLicense.GetAllLicensesByDriverID(_Driver.DriverID);
            dgvLocalDrivingLicenses.DataSource = _dtAllLocalLicenses;

            if (_dtAllLocalLicenses.Rows.Count > 0)
            {
                dgvLocalDrivingLicenses.Columns[0].HeaderText = "Lic.ID";
                dgvLocalDrivingLicenses.Columns[0].Width = 110;

                dgvLocalDrivingLicenses.Columns[1].HeaderText = "App.ID";
                dgvLocalDrivingLicenses.Columns[1].Width = 110;

                dgvLocalDrivingLicenses.Columns[2].HeaderText = "Class Name";
                dgvLocalDrivingLicenses.Columns[2].Width = 270;

                dgvLocalDrivingLicenses.Columns[3].HeaderText = "Issue Date";
                dgvLocalDrivingLicenses.Columns[3].Width = 170;

                dgvLocalDrivingLicenses.Columns[4].HeaderText = "Expiration Date";
                dgvLocalDrivingLicenses.Columns[4].Width = 170;

                dgvLocalDrivingLicenses.Columns[5].HeaderText = "Is Active";
                dgvLocalDrivingLicenses.Columns[5].Width = 110;
                _RecordsResults();

            }

        }
     
        void _LoadDataForInternationalLicenses()
        {
            _dtAllInternationalLicenses = clsInternationalLicense.InternationalLicenses(_Driver.DriverID);
            dgvInternationalLicenses.DataSource = _dtAllInternationalLicenses;
            if (dgvInternationalLicenses.Rows.Count > 0)
            {
                dgvInternationalLicenses.Columns[0].HeaderText = "Int.License ID";
                dgvInternationalLicenses.Columns[0].Width = 160;

                dgvInternationalLicenses.Columns[1].HeaderText = "Application ID";
                dgvInternationalLicenses.Columns[1].Width = 130;

                dgvInternationalLicenses.Columns[2].HeaderText = "L.License ID";
                dgvInternationalLicenses.Columns[2].Width = 130;

                dgvInternationalLicenses.Columns[3].HeaderText = "Issue Date";
                dgvInternationalLicenses.Columns[3].Width = 180;

                dgvInternationalLicenses.Columns[4].HeaderText = "Expiration Date";
                dgvInternationalLicenses.Columns[4].Width = 180;

                dgvInternationalLicenses.Columns[5].HeaderText = "Is Active";
                dgvInternationalLicenses.Columns[5].Width = 120;

                _RecordsResultsForInternational();

            }
        }
        void _LoadData()
        {
            
            if(_Driver != null)
            {
                _LoadDataForLocalLicenses();
                _LoadDataForInternationalLicenses();
            }
        }

        public void Clear()
        {
            _DriverID = -1;
            _personID = -1;
            _Driver = null;
            dgvLocalDrivingLicenses.DataSource = null;
            dgvInternationalLicenses.DataSource = null;
            lblRecordsForLocalResult.Text = "0";
            lblRecordsForInternationalResult.Text = "0";
        }
        private void _RecordsResults()
        {
            lblRecordsForLocalResult.Text = dgvLocalDrivingLicenses.Rows.Count.ToString();
        }
        private void _RecordsResultsForInternational()
        {
            lblRecordsForInternationalResult.Text = dgvInternationalLicenses.Rows.Count.ToString();
        }
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmShowLicense showLicense = new frmShowLicense((int)dgvLocalDrivingLicenses.CurrentRow.Cells[0].Value);
            showLicense.ShowDialog();
        }
        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            frmShowInternationalDriverLicenseInfo ShowInternationalDriverLicenseInfo = new frmShowInternationalDriverLicenseInfo((int)dgvInternationalLicenses.CurrentRow.Cells[0].Value);
            ShowInternationalDriverLicenseInfo.ShowDialog();
        }
    }
}
