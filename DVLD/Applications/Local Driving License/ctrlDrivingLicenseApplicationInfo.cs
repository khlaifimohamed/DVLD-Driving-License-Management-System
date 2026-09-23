using DVLD.Classes;
using DVLD.Global_Classes;
using DVLD.People;
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
using static System.Net.Mime.MediaTypeNames;

namespace DVLD.Applications.Local_Driving_License
{
    public partial class ctrlDrivingLicenseApplicationInfo : UserControl
    {

        private clsLocalDrivingLicenseApplication _LocalDrivingLicenseApplication;

        private int _LocalDrivingLicenseApplicationID;

        private int _LicenseID;

        public clsLocalDrivingLicenseApplication LocalDrivingLicenseApplication
        {
            get
            {
                return _LocalDrivingLicenseApplication;
            }
        }

        public ctrlDrivingLicenseApplicationInfo()
        {
            InitializeComponent();
        }

        private void _ResetLocalDrivingLicenseApplicationInfo()
        {
            _LocalDrivingLicenseApplicationID = -1;
            lblApplicationID.Text = "?????";
            lblApplicationStatus.Text = "?????";
            lblApplicationFees.Text = "?????";
            lblApplicationType.Text = "?????";
            lblApplicationApplicant.Text = "?????";
            lblApplicationDate.Text = "?????";
            lblApplicationStatusDate.Text = "?????";
            lblApplicationCreatedByUser.Text = "?????";
            lblLocalDrivingLicenseApplicationID.Text = "?????";
            lblAppliedFor.Text = "????";
            lblPassedTests.Text = "?????";
        }

        private void _FillApplicationBasicInfo(int ApplicationID)
        {
            clsApplication Application = clsApplication.FindBaseApplication(ApplicationID);
            if (Application == null)
            {
                MessageBox.Show("No Application with ApplicationID = " + ApplicationID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                lblApplicationID.Text = ApplicationID.ToString();
                lblApplicationStatus.Text = Application.StatusText;
                lblApplicationFees.Text = Application.PaidFees.ToString();
                lblApplicationType.Text = Application.ApplicationTypeInfo.Title;
                lblApplicationApplicant.Text = Application.ApplicantFullName;
                lblApplicationDate.Text = clsFormat.DateToShort(Application.ApplicationDate);
                lblApplicationStatusDate.Text = clsFormat.DateToShort(Application.LastStatusDate);
                lblApplicationCreatedByUser.Text = clsGlobal.CurrentUser.UserName;
            }
        }

        private void _FillLocalDrivingLicenseApplicationInfo()
        {
             _LicenseID = _LocalDrivingLicenseApplication.GetActiveLicenseID();
             btnShowLicenseInfo.Enabled = (_LicenseID != -1);
            lblLocalDrivingLicenseApplicationID.Text = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();
           lblAppliedFor.Text = clsLicenseClass.Find(_LocalDrivingLicenseApplication.LicenseClassID).ClassName;
            lblPassedTests.Text = _LocalDrivingLicenseApplication.GetPassedTestCount().ToString();
            _FillApplicationBasicInfo(_LocalDrivingLicenseApplication.ApplicationID);

        }

        public void LoadApplicationInfoByLocalDrivingAppID(int LocalDrivingLicenseApplicationID)
        {
            _LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(LocalDrivingLicenseApplicationID);
            if(_LocalDrivingLicenseApplication == null)
            {
                _ResetLocalDrivingLicenseApplicationInfo();
                MessageBox.Show("No Application with this ID", "ERROR");
                return;
            }
            _FillLocalDrivingLicenseApplicationInfo();
        }
        

        private void btnShowPersonInfo_Click(object sender, EventArgs e)
        {
            int PersonID = clsApplication.FindBaseApplication(_LocalDrivingLicenseApplication.ApplicationID).ApplicantPersonID;
            frmShowPersonInfo frm = new frmShowPersonInfo(PersonID);
            frm.ShowDialog();
        }

        

        private void btnShowLicenseInfo_Click(object sender, EventArgs e)
        {
            
        }

        private void ctrlDrivingLicenseApplicationInfo_Load(object sender, EventArgs e)
        {

        }
    }
}
