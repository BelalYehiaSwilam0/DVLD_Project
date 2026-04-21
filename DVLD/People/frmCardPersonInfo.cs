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
    public partial class frmCardPersonInfo : Form
    {
        int _PersonID = 0;
        public frmCardPersonInfo(int PersonID)
        {
            InitializeComponent();
            _PersonID = PersonID;
        }

        void _LoadData()
        {
            ctrlCardPersonInfo1._LoadPersonInfoByPersonID(_PersonID);
        }

        private void frmCardPersonInfo_Load(object sender, EventArgs e)
        {
            _LoadData();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
