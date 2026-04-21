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
    public partial class frmShowLicense : Form
    {
        private int _localLiceseID =0;
        public frmShowLicense(int localLiceseID)
        {
            InitializeComponent();
           _localLiceseID = localLiceseID;
        }
        private void frmShowLicense_Load(object sender, EventArgs e)
        {
            ctrlDriverLicenseInfo1.GetLicenseInfoByID(_localLiceseID);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
