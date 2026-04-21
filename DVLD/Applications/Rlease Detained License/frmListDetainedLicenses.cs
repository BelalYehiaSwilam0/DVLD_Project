using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Lifetime;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessLayer_DVLD;

namespace DVLD
{
    public partial class frmListDetainedLicenses : Form
    {
        DataTable _dtAllDetainedLicenses;
        public frmListDetainedLicenses()
        {
            InitializeComponent();
        }
        void _LoadDataForDetainedLicenses()
        {
            _dtAllDetainedLicenses = clsDetainedLicenses.GetAllDetainedLicenses();
             dgvDetainedLicenses.DataSource = _dtAllDetainedLicenses;
            if (_dtAllDetainedLicenses.Rows.Count > 0)
            {
                dgvDetainedLicenses.Columns[0].HeaderText = "D.ID";
                dgvDetainedLicenses.Columns[0].Width = 70;

                dgvDetainedLicenses.Columns[1].HeaderText = "L.ID";
                dgvDetainedLicenses.Columns[1].Width = 70;

                dgvDetainedLicenses.Columns[2].HeaderText = "D.Date";
                dgvDetainedLicenses.Columns[2].Width = 150;

                dgvDetainedLicenses.Columns[3].HeaderText = "IsReleased";
                dgvDetainedLicenses.Columns[3].Width = 100;

                dgvDetainedLicenses.Columns[4].HeaderText = "FineFees";
                dgvDetainedLicenses.Columns[4].Width = 100;

                dgvDetainedLicenses.Columns[5].HeaderText = "ReleaseDate";
                dgvDetainedLicenses.Columns[5].Width = 150;

                dgvDetainedLicenses.Columns[6].HeaderText = "N.No";
                dgvDetainedLicenses.Columns[6].Width = 70;

                dgvDetainedLicenses.Columns[7].HeaderText = "FullName";
                dgvDetainedLicenses.Columns[7].Width = 200;

                dgvDetainedLicenses.Columns[8].HeaderText = "Rlease App.ID";
                dgvDetainedLicenses.Columns[8].Width = 100;

                _RecordsResults();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbFilterBy.Text == "IsReleased")
            {
                txtFilterValue.Visible = false;
                cbIsReleased.Visible = true;
                cbIsReleased.Focus();
                cbIsReleased.SelectedIndex = 0;
            }
            else
            {
                txtFilterValue.Visible = (cbFilterBy.Text != "None");
            }

            if (cbFilterBy.Text == "None")
            {
                txtFilterValue.Enabled = false;
            }
            else
                txtFilterValue.Enabled = false;

            txtFilterValue.Text = "";
            txtFilterValue.Focus();
        }
        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(cbFilterBy.Text != "FullName" && cbFilterBy.Text != "N.No")
                 e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {

            string FilterCoulmn = "";
            switch (cbFilterBy.Text)
            {
                case "D.ID":
                    FilterCoulmn = "D.ID";
                    break;

                case "L.ID":
                    FilterCoulmn = "L.ID";
                    break;

                case "IsReleased":
                    FilterCoulmn = "IsReleased";
                    break;

                case "FineFees":
                    FilterCoulmn = "FineFees";
                    break;

                case "N.No":
                    FilterCoulmn = "N.No";
                    break;

                case "FullName":
                    FilterCoulmn = "FullName";
                    break;

                case "Rlease App.ID":
                    FilterCoulmn = "Rlease App.ID";
                    break;


            }
            if (txtFilterValue.Text.Trim() == "" || cbFilterBy.Text == "None")
            {
                _dtAllDetainedLicenses.DefaultView.RowFilter = "";
                _RecordsResults();
                return;
            }

            if (FilterCoulmn == "FullName" || FilterCoulmn == "N.No")
                _dtAllDetainedLicenses.DefaultView.RowFilter = string.Format("[{0}] LIKE '%{1}%'", FilterCoulmn, txtFilterValue.Text.Trim());
            else
                _dtAllDetainedLicenses.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterCoulmn, txtFilterValue.Text.Trim());


            _RecordsResults();
            return;
        }
        private void _RecordsResults()
        {
            lblRecordsResult.Text = dgvDetainedLicenses.Rows.Count.ToString();
        }
        private void frmManageDetainedLicenses_Load(object sender, EventArgs e)
        {
            _LoadDataForDetainedLicenses();
        }
      
        private void showDetilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = (int)dgvDetainedLicenses.CurrentRow.Cells[1].Value;
            int PersonID = clsLicense.FindLicenseByID(LicenseID).DriverInfo.PersonID;

            frmCardPersonInfo showPersonDetails = new frmCardPersonInfo(PersonID);
            showPersonDetails.ShowDialog();
        }
        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = (int)dgvDetainedLicenses.CurrentRow.Cells[1].Value;
            frmShowLicense showLicense = new frmShowLicense(LicenseID);
            showLicense.ShowDialog();
        }
      
        private void pbAddNew_Click(object sender, EventArgs e)
        {
            frmDetainLicense detainLicense = new frmDetainLicense();
            detainLicense.ShowDialog();
            frmManageDetainedLicenses_Load(null, null);
        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            frmRelaseDetainedLicense relaseDetainedLicense = new frmRelaseDetainedLicense();
            relaseDetainedLicense.ShowDialog();
            frmManageDetainedLicenses_Load(null, null);
        }
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            int LicenseID = (int)dgvDetainedLicenses.CurrentRow.Cells[1].Value;
            frmRelaseDetainedLicense relaseDetainedLicense = new frmRelaseDetainedLicense(LicenseID);
            relaseDetainedLicense.ShowDialog();
           frmManageDetainedLicenses_Load(null,null);
        }
        private void cmsManage_Opening(object sender, CancelEventArgs e)
        {  
            toolReleaseDetainedLicense.Enabled = !(bool)dgvDetainedLicenses.CurrentRow.Cells[3].Value;
        }

        private void ShowPersonHistory_Click(object sender, EventArgs e)
        {
            clsLicense _licenseInfo = clsLicense.FindLicenseByID((int)dgvDetainedLicenses.CurrentRow.Cells[1].Value);
            if (_licenseInfo == null)
                return;

            frmShowPersonLicenseHistory showPersonLicenseHistory = new frmShowPersonLicenseHistory(_licenseInfo.DriverInfo.PersonID);
            showPersonLicenseHistory.ShowDialog();
        }

        private void cbIsReleased_SelectedIndexChanged(object sender, EventArgs e)
        {
            string FilterColumn = "IsReleased";
            string FilterValue = cbIsReleased.Text;

            switch(FilterValue)
            {
                case "All":
                    break;
                case "Yes":
                    break;
                case "No":
                    FilterValue = "0";
                    break;
            }

            if (FilterValue == "All")
                _dtAllDetainedLicenses.DefaultView.RowFilter = "";
            else
                //in this case we deal with numbers not string.
                _dtAllDetainedLicenses.DefaultView.RowFilter = string.Format("[{0}] = {1}",FilterColumn,FilterValue);

            _RecordsResults();
        }
    }
}
