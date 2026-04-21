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
    public partial class frmListApplicationTypes : Form
    {
        DataTable _dtApplicationTypes;
        DataTable _dtAppTypes;
        public frmListApplicationTypes()
        {
            InitializeComponent();
        }

        private void _SetupApplicationTypesGridColumns()
        {
            dgvApplicationTypes.Columns[0].HeaderText = "ID";
            dgvApplicationTypes.Columns[0].Width = 110;

            dgvApplicationTypes.Columns[1].HeaderText = "Title";
            dgvApplicationTypes.Columns[1].Width = 400;

            dgvApplicationTypes.Columns[2].HeaderText = "Fees";
            dgvApplicationTypes.Columns[2].Width = 100;
        }
        private void _LoadData()
        {
            _dtApplicationTypes = clsApplicationTypes.GetAllApplicatoinTypes();

            if( _dtApplicationTypes.Rows.Count < 0 )
            {
                _SetupApplicationTypesGridColumns();

                return;
            }
           
            dgvApplicationTypes.DataSource = _dtApplicationTypes;
            _SetupApplicationTypesGridColumns();
            _RecordsResults();
        }
        private void _RecordsResults()
        {
            lblRecordsResult.Text = dgvApplicationTypes.Rows.Count.ToString();
        }

        private void frmApplicationTypes_Load(object sender, EventArgs e)
        {
            _LoadData();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmEditApplicationTypes editApplicationTypes = new frmEditApplicationTypes((int)dgvApplicationTypes.CurrentRow.Cells[0].Value);
            editApplicationTypes.ShowDialog();
            _LoadData();
        }
    }
}
