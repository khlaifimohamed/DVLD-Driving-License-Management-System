using System;
using System.Drawing;
using System.Windows.Forms;

namespace DVLD.People
{
    public partial class frmShowPersonInfo : Form
    {
        public frmShowPersonInfo(int PersonID)
        {
            InitializeComponent();
            ApplyStyling();
            ctrlPersonCard1.LoadPersonInfo(PersonID);
        }
        public frmShowPersonInfo(string NationalNo)
        {
            InitializeComponent();
            ApplyStyling();
            ctrlPersonCard1.LoadPersonInfo(NationalNo);
        }

        

        private void ApplyStyling()
        {
            // Hover effects for Close button (button1)
            button1.MouseEnter += (s, e) => {
                button1.BackColor = Color.FromArgb(231, 76, 60);
                button1.Font = new Font("Segoe UI", 11.5F, FontStyle.Bold);
            };
            button1.MouseLeave += (s, e) => {
                button1.BackColor = Color.FromArgb(85, 85, 90);
                button1.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            };
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmShowPersonInfo_Load(object sender, EventArgs e)
        {

        }
    }
}