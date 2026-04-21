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
    public partial class frmManageUsers : Form
    {
        DataTable _dtAllUsers;
        public frmManageUsers()
        {
            InitializeComponent();
        }

        private void _SetupListUsersGridColumns()
        {
            dgvGetAllUsers.Columns[0].HeaderText = "User ID";
            dgvGetAllUsers.Columns[0].Width = 110;

            dgvGetAllUsers.Columns[1].HeaderText = "Person ID";
            dgvGetAllUsers.Columns[1].Width = 120;

            dgvGetAllUsers.Columns[2].HeaderText = "Full Name";
            dgvGetAllUsers.Columns[2].Width = 350;

            dgvGetAllUsers.Columns[3].HeaderText = "UserName";
            dgvGetAllUsers.Columns[3].Width = 120;

            dgvGetAllUsers.Columns[4].HeaderText = "Is Active";
            dgvGetAllUsers.Columns[4].Width = 120;
        }
        void _loadData()
        {

            _dtAllUsers = clsUsers.GetAllUsers();
            cbFilterBy.SelectedIndex = 0;

            if (_dtAllUsers == null || _dtAllUsers.Rows.Count == 0)
            {
                _dtAllUsers = new DataTable();
                _dtAllUsers.Columns.Add("UserID");
                _dtAllUsers.Columns.Add("PersonID");
                _dtAllUsers.Columns.Add("FullName");
                _dtAllUsers.Columns.Add("UserName");
                _dtAllUsers.Columns.Add("IsActive");

                dgvGetAllUsers.DataSource = _dtAllUsers;
                _SetupListUsersGridColumns();
                _RecordsResults();
                return;
            }
            dgvGetAllUsers.DataSource = _dtAllUsers;
            _SetupListUsersGridColumns();
            _RecordsResults();


        }
        private void _RecordsResults()
        {
            lblRecordsResult.Text = dgvGetAllUsers.Rows.Count.ToString();
        }
        private void frmManageUsers_Load(object sender, EventArgs e)
        {
            _loadData();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {   

            if(cbFilterBy.Text == "IsActive")
            {
                txtFilterValue.Visible = false;
                cbIsActive.Visible = true;
                cbIsActive.Focus();
                cbIsActive.SelectedIndex = 0;
            }
            else
            {
                txtFilterValue.Visible = (cbFilterBy.Text == "None");
                cbIsActive.Visible= false;
                if(cbFilterBy.Text== "None")
                {
                    txtFilterValue.Visible = false;
                }
                else
                {
                    txtFilterValue.Visible = true;
                }

                txtFilterValue.Text = "";
                txtFilterValue.Focus();
            }
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.Text == "Person ID" || cbFilterBy.Text == "User ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {

          string FilterCoulmn = "";
            switch(cbFilterBy.Text)
            {
                case  "User ID":
                    FilterCoulmn = "UserID";
                    break;
                case "UserName":
                    FilterCoulmn = "UserName";
                    break;
                case "Person ID":
                    FilterCoulmn = "PersonID";
                    break;
                case "FullName":
                    FilterCoulmn = "FullName";
                    break;
                default:
                    FilterCoulmn = "None";
                    break;
            }
            if(txtFilterValue.Text.Trim() == "" || txtFilterValue.Text == "None")
            {
                _dtAllUsers.DefaultView.RowFilter = "";
                _RecordsResults();
                return;
            }

            if (FilterCoulmn == "PersonID" || FilterCoulmn == "UserID")

                _dtAllUsers.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterCoulmn, txtFilterValue.Text.Trim());
            else
                _dtAllUsers.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterCoulmn, txtFilterValue.Text.Trim());

            _RecordsResults();
            return;
        }

        private void cbIsActive_SelectedIndexChanged(object sender, EventArgs e)
        {
            string FilterCoulmn = "IsActive";
            string FilterValue = cbIsActive.Text;
            switch(FilterValue)
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
                _dtAllUsers.DefaultView.RowFilter = "";
            else
                _dtAllUsers.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterCoulmn, FilterValue);

            _RecordsResults();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete this User info [" + dgvGetAllUsers.CurrentRow.Cells[0].Value + "]", "Confirm Delete", MessageBoxButtons.OKCancel) == DialogResult.OK)

            {

                //Perform Delele and refresh
                if (clsUsers.DeletePersonByID((int)dgvGetAllUsers.CurrentRow.Cells[0].Value))
                {
                    MessageBox.Show("User has been deleted successfully", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    frmManageUsers_Load(null, null);
                }

                else
                    MessageBox.Show("User is not delted due to data connected to it.", "Faild", MessageBoxButtons.OK, MessageBoxIcon.Error);



            }
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdateUser AddNewUser = new frmAddUpdateUser((int)dgvGetAllUsers.CurrentRow.Cells[0].Value);
            AddNewUser.ShowDialog();
            frmManageUsers_Load(null, null);
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
            frmChangePassword changePassword = new frmChangePassword((int)dgvGetAllUsers.CurrentRow.Cells[0].Value);
            changePassword.ShowDialog();
        }

        private void sendToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Let's make it happen!", "Action Mode", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Let's make it happen!", "Action Mode", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void showDetilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUserInfo frmUserInfo = new frmUserInfo((int)dgvGetAllUsers.CurrentRow.Cells[0].Value);
            frmUserInfo.ShowDialog();
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            frmAddUpdateUser AddNewUser = new frmAddUpdateUser();
            AddNewUser.ShowDialog();
            frmManageUsers_Load(null, null);
        }
    }
}
