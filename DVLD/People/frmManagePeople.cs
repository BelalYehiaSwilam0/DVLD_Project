using System;
using System.Data;
using System.Security.Policy;
using System.Windows.Forms;
using BusinessLayer_DVLD;

namespace DVLD
{
    public partial class frmManagePeople : Form
    {
        
        DataTable _dtAllPeople;
      
        public frmManagePeople()
        {
            InitializeComponent(); 
        }

       
        void _loadData()
        {
            _dtAllPeople = clsPerson.GetAllPeople();
            dgvPeople.DataSource = _dtAllPeople;
            if ( _dtAllPeople.Rows.Count >  0)
            {
                dgvPeople.Columns[0].HeaderText = "Person ID";
                dgvPeople.Columns[0].Width = 110;

                dgvPeople.Columns[1].HeaderText = "National No.";
                dgvPeople.Columns[1].Width = 120;


                dgvPeople.Columns[2].HeaderText = "First Name";
                dgvPeople.Columns[2].Width = 120;

                dgvPeople.Columns[3].HeaderText = "Second Name";
                dgvPeople.Columns[3].Width = 140;


                dgvPeople.Columns[4].HeaderText = "Third Name";
                dgvPeople.Columns[4].Width = 120;

                dgvPeople.Columns[5].HeaderText = "Last Name";
                dgvPeople.Columns[5].Width = 120;

                dgvPeople.Columns[6].HeaderText = "Gendor";
                dgvPeople.Columns[6].Width = 120;

                dgvPeople.Columns[7].HeaderText = "Date Of Birth";
                dgvPeople.Columns[7].Width = 140;

                dgvPeople.Columns[8].HeaderText = "Nationality";
                dgvPeople.Columns[8].Width = 120;


                dgvPeople.Columns[9].HeaderText = "Phone";
                dgvPeople.Columns[9].Width = 120;


                dgvPeople.Columns[10].HeaderText = "Email";
                dgvPeople.Columns[10].Width = 170;
                _RecordsResults();

                return;
            }
          
            
        }
        private void _RecordsResults()
        {
            lblRecordsResult.Text = dgvPeople.Rows.Count.ToString(); 
        }
        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Visible = (cbFilterBy.Text != "None");
            txtFilterValue.Text = "";
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(cbFilterBy.Text == "Person ID")
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void frmManagePeople_Load(object sender, EventArgs e)
        {
            _loadData();
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterCoulmn = "";
            switch (cbFilterBy.Text)
            {
                case "Person ID":
                    FilterCoulmn = "PersonID";
                     break;

                case "National NO.":
                    FilterCoulmn = "NationalNO";
                    break;

                case "FirstName":
                    FilterCoulmn = "FirstName";
                    break;

                case "SecondName":
                    FilterCoulmn = "SecondName";
                    break;

                case "Third Name":
                    FilterCoulmn = "ThirdName";
                    break;

                case "LastName":
                    FilterCoulmn = "LastName";
                    break;

                case "Nationality":
                    FilterCoulmn = "Nationality";
                    break;

                case "Gender":
                    FilterCoulmn = "Gender";
                    break;

                case "Phone":
                    FilterCoulmn = "Phone";
                    break;

                case "Email":
                    FilterCoulmn = "Email";
                    break;


            }

            if(txtFilterValue.Text.Trim() == "" || cbFilterBy.Text == "None")
            {
                _dtAllPeople.DefaultView.RowFilter = "";
                _RecordsResults();
                return;
            }

            if (FilterCoulmn == "PersonID")

                _dtAllPeople.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterCoulmn, txtFilterValue.Text.Trim());
            else

                _dtAllPeople.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterCoulmn, txtFilterValue.Text.Trim());

            _RecordsResults();
            return;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //int PersonID = Convert.ToInt32()
            frmAddEditPersonInfo frm = new frmAddEditPersonInfo((int)dgvPeople.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            _loadData();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Are you sure you want to delete this person info [" + dgvPeople.CurrentRow.Cells[0].Value + "]", "Confirm Delete", MessageBoxButtons.OKCancel) == DialogResult.OK)

            {

                //Perform Delele and refresh
                if (clsPerson.DeletePersonByID((int)dgvPeople.CurrentRow.Cells[0].Value))
                {
                    dgvPeople.Rows.Remove(dgvPeople.CurrentRow);
                    MessageBox.Show("Person info Deleted Successfully.");
                    //_RefreshContactsList();
                }

                else
                    MessageBox.Show("Contact is not deleted.");

                

            }
        }

        private void showDetilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCardPersonInfo showPersonDetails = new frmCardPersonInfo((int)dgvPeople.CurrentRow.Cells[0].Value);
            showPersonDetails.ShowDialog();
        }

        private void btnAddPerson_Click(object sender, EventArgs e)
        {
            //Form frm = new frmAdd_EditPersonInfo(-1);
            frmAddEditPersonInfo frm = new frmAddEditPersonInfo();
            frm.ShowDialog();
            _loadData();
        }

        private void tsmShowPersonLicenseHistory_Click(object sender, EventArgs e)
        {
            frmShowPersonLicenseHistory personLicenseHistory = new frmShowPersonLicenseHistory((int)dgvPeople.CurrentRow.Cells[0].Value);
            personLicenseHistory.ShowDialog();
        }
    }
}
