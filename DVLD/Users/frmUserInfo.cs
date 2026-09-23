using System;
using System.Drawing;
using System.Windows.Forms;

namespace DVLD.Users
{
    public partial class frmUserInfo : Form
    {
        private int _UserID;
        public frmUserInfo(int UserID)
        {
            InitializeComponent();
            ApplyStyling();
            _UserID = UserID;
        }

        private void ApplyStyling()
        {
            // Close button hover effects
            btnClose.MouseEnter += (s, e) =>
            {
                btnClose.BackColor = Color.FromArgb(231, 76, 60);
                btnClose.Font = new Font("Segoe UI", 12.5F, FontStyle.Bold);
            };

            btnClose.MouseLeave += (s, e) =>
            {
                btnClose.BackColor = Color.FromArgb(85, 85, 90);
                btnClose.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            };

            // Close button click event
            btnClose.Click += (s, e) =>
            {
                this.Close();
            };
        }

        private void frmUserInfo_Load(object sender, EventArgs e)
        {
            ctrlUserCard1.LoadUserInfo(_UserID);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}