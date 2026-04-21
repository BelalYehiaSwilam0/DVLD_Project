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
    public partial class frmUserInfo : Form
    {
        int UserID;

        public frmUserInfo(int UserID)
        {
            InitializeComponent();
            this.UserID = UserID;
        }
        void LoadData()
        {
            ctrlUserCard1.LoadUserInfo(UserID);    
        }
        private void frmUserInfo_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
