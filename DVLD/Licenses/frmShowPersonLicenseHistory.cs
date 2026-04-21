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
    public partial class frmShowPersonLicenseHistory : Form
    {
        int _PersonID = -1;

        public frmShowPersonLicenseHistory()
        {
            InitializeComponent();
        }
        public frmShowPersonLicenseHistory( int PersonID)
        {
            InitializeComponent();
            _PersonID = PersonID;
        }
        void _LoadData()
        {
            if(_PersonID == -1)
            {
                ctrlCardPersonInfoWithFilter1.FilterEnabled = true;
                ctrlCardPersonInfoWithFilter1.FilterFocus();
                
                return;
            }

            ctrlCardPersonInfoWithFilter1.LoadPersonInfo(_PersonID);
            ctrlCardPersonInfoWithFilter1.FilterEnabled = false;
            ctrlDriverLicensescs1.LoadLicensesByPersonID(_PersonID);
        }
        private void frmShowPersonLicenseHistory_Load(object sender, EventArgs e)
        {
            _LoadData();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

      

        private void ctrlCardPersonInfoWithFilter1_OnPersonSelected(int obj)
        {
            
        }
    }
}
