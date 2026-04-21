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
    public partial class frmListTestTypes : Form
    {
        DataTable _dtTestTypes;
        public frmListTestTypes()
        {
            InitializeComponent();
        }
        private void _SetupTestTypesGridColumns()
        {
            dgvTestTypes.Columns[0].HeaderText = "ID";
            dgvTestTypes.Columns[0].Width = 120;

            dgvTestTypes.Columns[1].HeaderText = "Title";
            dgvTestTypes.Columns[1].Width = 200;

            dgvTestTypes.Columns[2].HeaderText = "Description";
            dgvTestTypes.Columns[2].Width = 400;

            dgvTestTypes.Columns[3].HeaderText = "Fees";
            dgvTestTypes.Columns[3].Width = 100;
        }
        private void _LoadData()
        {
            _dtTestTypes = clsTestType.GetAllTestTypes();
           
            if (_dtTestTypes.Rows.Count > 0)
            {
                dgvTestTypes.DataSource = _dtTestTypes;
                _SetupTestTypesGridColumns();
                _RecordsResults();
            }
            else
            {
                MessageBox.Show("No test types found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }
        private void _RecordsResults()
        {
            lblRecordsResult.Text = dgvTestTypes.Rows.Count.ToString();
        }
        private void frmManageTestTypes_Load(object sender, EventArgs e)
        {
            _LoadData();
        }

        private void toolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
            frmEditTestTypes editTestTypes = new frmEditTestTypes((clsTestType.enTestType)dgvTestTypes.CurrentRow.Cells[0].Value);
            editTestTypes.ShowDialog();
            _LoadData();
        }
    }
}
