using System;
using System.Drawing;
using System.Windows.Forms;

namespace DVLD.People
{
    public partial class frmFindPerson : Form
    {
        public delegate void DataBackEventHandler(object sender, int PersonID);

        public event DataBackEventHandler DataBack;
            
        public frmFindPerson()
        {
            InitializeComponent();
            ApplyStyling();
        }

        

        private void ApplyStyling()
        {
            // Close button hover effect
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
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            DataBack?.Invoke(this, ctrlPersonCardWithFilter1.PersonID);
        }
    }
}