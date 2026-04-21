using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD
{
    public partial class frmShowInternationalDriverLicenseInfo : Form
    {
        int _internationalLicenseID = 0;
        public frmShowInternationalDriverLicenseInfo(int internationalLicenseID)
        {
            InitializeComponent();
            _internationalLicenseID = internationalLicenseID;
        }
        void _LoadInternationalLicenseInfo()
        {
            ctrlInternationalDriverLicenseInfoCard1.ShowInternationalLicenseInfo(_internationalLicenseID);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void frmInternationalDriverLicenseInfo_Load(object sender, EventArgs e)
        {
            _LoadInternationalLicenseInfo();
        }
    }
}
