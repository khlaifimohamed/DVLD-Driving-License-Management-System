using DVLD.Classes;
using DVLD.Global_Classes;
using DVLD_BusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Applications.Replace_lost_or_damaged_License
{
    
    public partial class frmReplaceLostOrDamagedLicenseApplication : Form
    {

        private int _NewLicenseID = -1;


        public frmReplaceLostOrDamagedLicenseApplication()
        {
            InitializeComponent();
        }

        private void frmReplaceLostOrDamagedLicenseApplication_Load(object sender, EventArgs e)
        {
            lblApplicationDate.Text = clsFormat.DateToShort(DateTime.Now);
            lblCreatedBy.Text = clsGlobal.CurrentUser.UserName;
            rbDamagedLicense.Checked = true;
            btnIssueReplacement.Enabled = false;
            btnShowLicenseHistory.Enabled = false;
            btnShowNewLicenseInfo.Enabled = false;
        }

        private int _GetApplicationTypeID()
        {
            if (rbDamagedLicense.Checked)
            {
                return (int)clsApplication.enApplicationType.ReplaceDamagedDrivingLicense;
            }
            else
            {
                return (int)clsApplication.enApplicationType.ReplaceLostDrivingLicense;
            }
        }
        private void rbDamagedLicense_CheckedChanged(object sender, EventArgs e)
        {
            lblApplicationFees.Text = clsApplicationType.Find(_GetApplicationTypeID()).Fees.ToString();
        }

        private void rbLostLicense_CheckedChanged(object sender, EventArgs e)
        {
            lblApplicationFees.Text = clsApplicationType.Find(_GetApplicationTypeID()).Fees.ToString();

        }

        private void ctrlDriverLicenseInfoWithFilter1_OnLicenseSelected(int obj)
        {
            int SelectedLicenseID = obj;
            lblOldLicenseID.Text = SelectedLicenseID.ToString();
            btnShowLicenseHistory.Enabled = (SelectedLicenseID != -1);

            if(SelectedLicenseID == -1)
            {
                return;
            }

            if (!ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.IsActive)
            {
                MessageBox.Show("Selected License is not Active", "Not Allowed");
                btnIssueReplacement.Enabled = false;
                return;
            }

            btnIssueReplacement.Enabled = true;
        }
       
        private clsLicense.enIssueReason _GetIssueReason()
        {
            if (rbDamagedLicense.Checked)
            {
                
                return clsLicense.enIssueReason.DamagedReplacement;
            }
            else
            {
                return clsLicense.enIssueReason.LostReplacement;
            }
        }
        private void btnIssueReplacement_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are You sure you want to Issue a replacement for the license", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            clsLicense NewLicense = ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.Replace(_GetIssueReason(), clsGlobal.CurrentUser.UserID);

            if(NewLicense == null)
            {
                MessageBox.Show("Failed To Issue a Replacement for the license ", "ERROR");
                return;
            }

            lblApplicationID.Text = NewLicense.ApplicationID.ToString();
            _NewLicenseID = NewLicense.LicenseID;

            lblNewLicenseID.Text = _NewLicenseID.ToString();

            MessageBox.Show("License Replaced Succesfully", "SAVED");

            btnIssueReplacement.Enabled = false;
            gbReplacementFor.Enabled = false;
            ctrlDriverLicenseInfoWithFilter1.FilterEnabled = false;
            btnShowNewLicenseInfo.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

}
