using DVLD_BusinessLayer;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace DVLD.Users
{
    public partial class ctrlUserCard : UserControl
    {

        private clsUser _User;
        private int _UserID;

        public int UserID
        {
            get { return _UserID; }
        }

        public void LoadUserInfo(int UserID)
        {
            _UserID = UserID;
            _User = clsUser.FindByUserID(UserID);
            if(_User == null)
            {
                _ResetPersonInfo();
                MessageBox.Show("No User with UserID = " + UserID.ToString(), "ERROR");
                return;
            }
            _FillUserInfo();
        }



        private void _ResetPersonInfo()
        {
            ctrlPersonCard1.ResetPersonInfo();
            lblUserIDValue.Text = "?????";
            lblUserNameValue.Text = "??????";
            lblIsActiveValue.Text = "??????";
        }
        private void _FillUserInfo()
        {
            ctrlPersonCard1.LoadPersonInfo(_User.PersonID);
            lblUserIDValue.Text = _User.UserID.ToString();
            lblUserNameValue.Text = _User.UserName;

            if (_User.isActive)
            {
                lblIsActiveValue.Text = "YES";
            }
            else
            {
                lblIsActiveValue.Text = "NO";
            }
        } 
        public ctrlUserCard()
        {
            InitializeComponent();
            ApplyStyling();
        }

        private void ApplyStyling()
        {
            // Add rounded corners to the login info panel
            ApplyRoundedCorners(panelLoginInfo, 10);
        }

        private void ApplyRoundedCorners(Control control, int radius)
        {
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            path.AddArc(0, 0, radius * 2, radius * 2, 180, 90);
            path.AddArc(control.Width - radius * 2, 0, radius * 2, radius * 2, 270, 90);
            path.AddArc(control.Width - radius * 2, control.Height - radius * 2, radius * 2, radius * 2, 0, 90);
            path.AddArc(0, control.Height - radius * 2, radius * 2, radius * 2, 90, 90);
            path.CloseFigure();
            control.Region = new Region(path);
        }


    }
}