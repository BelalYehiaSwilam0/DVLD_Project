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
    public partial class ctrlUserCard : UserControl
    {
        private clsUsers _User;
        private int _UserID = -1;

        public int UserID
        {
            get { return _UserID; }
        }

        public ctrlUserCard()
        {
            InitializeComponent();
        }

        private void ResetPersonInfo()
        {
            ctrlCardPersonInfo1._ResetPersonInfo();
            lblUserIDResult.Text = "[???]";
            lblUserNameResult.Text = "[???]"; 
            lblISActiveResult.Text  ="[???]";
        }
        public void LoadUserInfo(int UserID)
        {
            _UserID = UserID;

            _User = clsUsers.FindUserByID(UserID);
            if( _User == null )
            {
                ResetPersonInfo();
                MessageBox.Show("No User with UserID = " + UserID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _FillUserInfo();
        }

        private void _FillUserInfo()
        {
            ctrlCardPersonInfo1._LoadPersonInfoByPersonID(_User.PersonID);
            lblUserNameResult.Text = _User.UserName;
            lblUserIDResult.Text = _User.UserID.ToString();

            lblISActiveResult.Text = (_User.IsActive) ? "Yes" : "No";
        }
    }
}
