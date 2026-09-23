using DVLD.Classes;
using DVLD_BusinessLayer;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace DVLD.Tests.Controls
{
    public partial class ctrlScheduleTest : UserControl
    {
        public enum enMode { AddNew = 0, Update = 1 }

        private enMode _Mode = enMode.AddNew;

        public enum enCreationMode { FirstTimeSchedule = 0, RetakeTestSchedule = 1 }

        private enCreationMode _CreationMode = enCreationMode.FirstTimeSchedule;

        private clsTestType.enTestType _TestTypeID = clsTestType.enTestType.VisionTest;

        private clsLocalDrivingLicenseApplication _LocalDrivingLicenseApplication;

        private int _LocalDrivingLicenseApplicationID;

        private clsTestAppointment _TestAppointment;

        private int _TestAppointmentID = -1;

        public clsTestType.enTestType TestTypeID
        {
            get
            {
                return _TestTypeID;
            }
            set
            {
                _TestTypeID = value;
                switch (_TestTypeID)
                {
                    case clsTestType.enTestType.VisionTest:
                        SetVisionTestIcon();
                        break;
                    case clsTestType.enTestType.WrittenTest:
                        SetWrittenTestIcon();
                        break;
                    case clsTestType.enTestType.StreetTest:
                        SetStreetTestIcon();
                        break;
                }
            }
        }


        public void LoadInfo(int LocalDrivingLicenseApplicationID,int AppointmentID = -1)
        {
            if(AppointmentID == -1)
            {
                _Mode = enMode.AddNew;
            }
            else
            {
                _Mode = enMode.Update;
            }

            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            _TestAppointmentID = AppointmentID;
            _LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(_LocalDrivingLicenseApplicationID);
            if(_LocalDrivingLicenseApplication == null)
            {
                MessageBox.Show("Error: No Local Driving License Application with this ID", "ERROR");
                btnSave.Enabled = false;
                return;
            }

            if (_LocalDrivingLicenseApplication.DoesAttendTestType(_TestTypeID))
            {
                _CreationMode = enCreationMode.RetakeTestSchedule;
            }
            else
            {
                _CreationMode = enCreationMode.FirstTimeSchedule;
            }

            if(_CreationMode == enCreationMode.RetakeTestSchedule)
            {
                lblRetakeTestAppFees.Text = clsApplicationType.Find((int)clsApplication.enApplicationType.RetakeTest).Fees.ToString();
                gbRetakeTestInfo.Enabled = true;
                lblTitle.Text += " (Retake)";
                lblRetakeTestAppID.Text = "0";
            }
            else
            {
                lblRetakeTestAppFees.Text = "0";
                gbRetakeTestInfo.Enabled = false;
                lblRetakeTestAppID.Text = "N/A";
            }
            lblLocalDrivingLicenseApplicationID.Text = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();
            lblDrivingClass.Text = _LocalDrivingLicenseApplication.LicenseClassInfo.ClassName;
            lblFullName.Text = _LocalDrivingLicenseApplication.FullName;
            lblTrialText.Text = _LocalDrivingLicenseApplication.TotalTrialsPerTest(_TestTypeID).ToString();

            if(_Mode == enMode.AddNew)
            {
                lblFees.Text = clsTestType.Find(_TestTypeID).Fees.ToString();
                dtpTestDate.MinDate = DateTime.Now;
                lblRetakeTestAppID.Text = "N/A";
                _TestAppointment = new clsTestAppointment();
            }
            else
            {
                if (!_LoadTestAppointmentData())
                {
                    return;
                }
            }

            lblTotalFees.Text = (Convert.ToSingle(lblRetakeTestAppFees.Text) + Convert.ToSingle(lblFees.Text)).ToString();

            if (!_HandleActiveTestAppointmentConstraint())
            {
                return;
            }

            if (!_HandleAppointmentLockedConstraint())
            {
                return;
            }

            if (!_HandlePreviousTestConstraint())
            {
                return;
            }


        }

        private bool _HandlePreviousTestConstraint()
        {
            switch (TestTypeID)
            {
                case clsTestType.enTestType.VisionTest:
                    lblUserMessage.Visible = false;
                    return true;
                case clsTestType.enTestType.WrittenTest:
                    if (!_LocalDrivingLicenseApplication.DoesPassTestType(clsTestType.enTestType.VisionTest))
                    {
                        lblUserMessage.Text = "⚠ Cannot schedule: Vision Test must be passed first.";
                        lblUserMessage.Visible = true;
                        btnSave.Enabled = false;
                        dtpTestDate.Enabled = false;
                        return false;
                    }
                    else
                    {
                        lblUserMessage.Visible = false;
                        btnSave.Enabled = true;
                        dtpTestDate.Enabled = true;
                    }
                    return true;
                case clsTestType.enTestType.StreetTest:
                    if (!_LocalDrivingLicenseApplication.DoesPassTestType(clsTestType.enTestType.WrittenTest))
                    {
                        lblUserMessage.Text = "⚠ Cannot schedule: Written Test must be passed first.";
                        lblUserMessage.Visible = true;
                        btnSave.Enabled = false;
                        dtpTestDate.Enabled = false;
                        return false;
                    }
                    else
                    {
                        lblUserMessage.Visible = false;
                        btnSave.Enabled = true;
                        dtpTestDate.Enabled = true;
                    }
                    return true;
            }


            return true;
        }
        private bool _HandleAppointmentLockedConstraint()
        {

            if (_TestAppointment.IsLocked)
            {
                lblUserMessage.Text = "⚠ Person already sat for the test , Appointment locked ";
                dtpTestDate.Enabled = false;
                lblUserMessage.Visible = true;
                btnSave.Enabled = false;
                return false;
            }
            else
            {
                lblUserMessage.Visible = false;
            }
            return true;
        }

        private bool _HandleActiveTestAppointmentConstraint()
        {
            if(_Mode == enMode.AddNew && clsLocalDrivingLicenseApplication.IsThereAnActiveScheduledTest(_LocalDrivingLicenseApplicationID,TestTypeID))
            {
                lblUserMessage.Text = "⚠ Person  already have an active appointment for this test ";
                btnSave.Enabled = false;
                dtpTestDate.Enabled = false;
                return false;
            }
            return true;
        }
        private bool _LoadTestAppointmentData()
        {

            _TestAppointment = clsTestAppointment.Find(_TestAppointmentID);
            if(_TestAppointment == null)
            {
                MessageBox.Show("ERROR no Appointment with this ID ","ERROR");
                btnSave.Enabled = false;
                return false;
            }
            lblFees.Text = _TestAppointment.PaidFees.ToString();

            if(DateTime.Compare(DateTime.Now,_TestAppointment.AppointmentDate) < 0)
            {
                dtpTestDate.MinDate = DateTime.Now;
            }
            else
            {
                dtpTestDate.MinDate = _TestAppointment.AppointmentDate;
            }
            dtpTestDate.Value = _TestAppointment.AppointmentDate;
            
            if(_TestAppointment.RetakeTestApplicationID == -1)
            {
                lblRetakeTestAppFees.Text = "0";
                lblRetakeTestAppID.Text = "N/A";
            }
            else
            {
                lblRetakeTestAppFees.Text = _TestAppointment.RetakeTestAppInfo.PaidFees.ToString();
                gbRetakeTestInfo.Enabled = true;
                lblTitle.Text += " (Retake)";
                lblRetakeTestAppID.Text = _TestAppointment.RetakeTestApplicationID.ToString();
            }

            return true;
        }

        public ctrlScheduleTest()
        {
            InitializeComponent();
        }

        
        private void SetVisionTestIcon()
        {
            label1.Text = "👁️";
            label1.ForeColor = Color.FromArgb(86, 192, 255);
            label1.AccessibleName = "Vision Test Icon";
            lblTitle.Text = "📅 Schedule Vision Test";
        }

        
        private void SetWrittenTestIcon()
        {
            label1.Text = "📝";
            label1.ForeColor = Color.FromArgb(255, 196, 92);
            label1.AccessibleName = "Written Test Icon";

            lblTitle.Text = "📅 Schedule Written Test";
        }

        
        private void SetStreetTestIcon()
        {
            label1.Text = "🚗";
            label1.ForeColor = Color.FromArgb(104, 211, 145);
            label1.AccessibleName = "Street Test Icon";

            lblTitle.Text = "📅 Schedule Street Test";
        }

        private bool _HandleRetakeApplication()
        {
            if(_Mode == enMode.AddNew && _CreationMode == enCreationMode.RetakeTestSchedule)
            {
                clsApplication Application = new clsApplication();
                Application.ApplicantPersonID = _LocalDrivingLicenseApplication.ApplicantPersonID;
                Application.ApplicationDate = DateTime.Now;
                Application.ApplicationTypeID = (int) clsApplication.enApplicationType.RetakeTest;
                Application.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
                Application.LastStatusDate = DateTime.Now;
                Application.PaidFees = clsApplicationType.Find((int)clsApplication.enApplicationType.RetakeTest).Fees;
                Application.CreatedByUserID = clsGlobal.CurrentUser.UserID;
                if (!Application.Save())
                {
                    _TestAppointment.RetakeTestApplicationID = -1;
                    MessageBox.Show("Failed to Create Application", "Failed");
                    return false;
                }
                _TestAppointment.RetakeTestApplicationID = Application.ApplicationID;
            }



            return true;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!_HandleRetakeApplication())
            {
                return;
            }

            _TestAppointment.TestTypeID = _TestTypeID;
            _TestAppointment.LocalDrivingLicenseApplicationID = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID;
            _TestAppointment.AppointmentDate = dtpTestDate.Value;
            _TestAppointment.PaidFees = Convert.ToSingle(lblFees.Text);
            _TestAppointment.CreatedByUserID = clsGlobal.CurrentUser.UserID;
            if(!_TestAppointment.Save())
            {
                MessageBox.Show("Test Appointment is not saved", "ERROR");
            }else {
                _Mode = enMode.Update;
                MessageBox.Show("Test Appointment  saved succesfully", "Saved");
            }
        }

        
    }
}