using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessLayer_DVLD;

namespace DVLD
{
    public partial class frmManageDrivers : Form
    {
        DataTable _dtAllDriverLicenses;
        public frmManageDrivers()
        {
            InitializeComponent();
        }
        private void _SetupGridColumns()
        {
            dgvManageDrivers.Columns["DriverID"].Width = 70;
            dgvManageDrivers.Columns["PersonID"].Width = 70;
            dgvManageDrivers.Columns["NationalNo"].Width = 100;
            dgvManageDrivers.Columns["FullName"].Width = 250;
            dgvManageDrivers.Columns["CreatedDate"].Width = 150;
            dgvManageDrivers.Columns["NumberOfActiveLicenses"].Width = 140;
        }
        void _LoadData()
        {
            _dtAllDriverLicenses = clsDriver.GetAllDrivers();
            if(_dtAllDriverLicenses == null || _dtAllDriverLicenses.Rows.Count ==0)
            {
                _dtAllDriverLicenses = new DataTable();
                _dtAllDriverLicenses.Columns.Add("DriverID");
                _dtAllDriverLicenses.Columns.Add("PersonID");
                _dtAllDriverLicenses.Columns.Add("NationalNo");
                _dtAllDriverLicenses.Columns.Add("FullName");
                _dtAllDriverLicenses.Columns.Add("CreatedDate");
                _dtAllDriverLicenses.Columns.Add("NumberOfActiveLicenses");

                dgvManageDrivers.DataSource = _dtAllDriverLicenses;

                _SetupGridColumns();


                return;

            }
            _dtAllDriverLicenses = _dtAllDriverLicenses.DefaultView.ToTable(false, "DriverID", "PersonID", "NationalNo", "FullName", "CreatedDate", "NumberOfActiveLicenses");
            dgvManageDrivers.DataSource = _dtAllDriverLicenses;

            _SetupGridColumns();
            _RecordsResults();
        }
        private void _RecordsResults()
        {
            lblRecordsResult.Text = dgvManageDrivers.Rows.Count.ToString();
        }
        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Visible = (cbFilterBy.Text != "None");
            txtFilterValue.Text = "";
        }
        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.Text == "Driver ID" || cbFilterBy.Text == "Person ID" || cbFilterBy.Text == "Active Licenses")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterCoulmn = "";
            switch (cbFilterBy.Text)
            {
                case "Driver ID":
                    FilterCoulmn = "DriverID";
                    break;
                case "Person ID":
                    FilterCoulmn = "PersonID";
                    break;
                case "Natinal No.":
                    FilterCoulmn = "NationalNo";
                    break;
                case "FullName":
                    FilterCoulmn = "FullName";
                    break;
                case "Date":
                    FilterCoulmn = "CreatedDate";
                    break;
                case "Active Licenses":
                    FilterCoulmn = "NumberOfActiveLicenses";
                    break;
            }
            if (txtFilterValue.Text.Trim() == "" || cbFilterBy.Text == "None")
            {
                _dtAllDriverLicenses.DefaultView.RowFilter = "";
                _RecordsResults();
                return;
            }

            if (FilterCoulmn == "DriverID" || FilterCoulmn == "PersonID" || FilterCoulmn == "NumberOfActiveLicenses")

                _dtAllDriverLicenses.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterCoulmn, txtFilterValue.Text.Trim());
            else
                _dtAllDriverLicenses.DefaultView.RowFilter = string.Format("[{0}] LIKE '%{1}%'", FilterCoulmn, txtFilterValue.Text.Trim());
            _RecordsResults();
            return;
        }
        private void frmManageDrivers_Load(object sender, EventArgs e)
        {
            _LoadData();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCardPersonInfo personInfo = new frmCardPersonInfo((int)dgvManageDrivers.CurrentRow.Cells[1].Value);
            personInfo.ShowDialog();
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmShowPersonLicenseHistory showPersonLicenseHistory = new frmShowPersonLicenseHistory((int)dgvManageDrivers.CurrentRow.Cells[1].Value);
            showPersonLicenseHistory.ShowDialog();
        }
    }
}
