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

namespace DVLD.Tests.Controls
{
    public partial class ctrlScheduledTest : UserControl
    {
        private clsTestType.enTestType _TestTypeID = clsTestType.enTestType.VisionTest;

        private int _LocalDrivingLicenseApplicationID;

        private clsLocalDrivingLicenseApplication _LocalDrivingLicenseApplication;

        private int _TestAppointmentID = -1;

        public int TestAppointmentID
        {
            get { return _TestAppointmentID; }
        }

        private int _TestID = -1;

        public int TestID
        {
            get { return _TestID; }
        }
        public clsTestType.enTestType TestTypeID
        {
            get { return _TestTypeID; }
            set {
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
        private void SetVisionTestIcon()
        {
            label1.Text = "👁️";
            label1.ForeColor = Color.FromArgb(86, 192, 255); // Light blue
            label2.Text = "👁️ Scheduled Vision Test";
        }

        
        private void SetWrittenTestIcon()
        {
            label1.Text = "📝";
            label1.ForeColor = Color.FromArgb(255, 196, 92); // Warm orange
            label2.Text = "📝 Scheduled Written Test";
        }

        
        private void SetStreetTestIcon()
        {
            label1.Text = "🚗";
            label1.ForeColor = Color.FromArgb(104, 211, 145); // Fresh green
            label2.Text = "🚗 Scheduled Street Test";
        }
    
        public ctrlScheduledTest()
        {
            InitializeComponent();
        }

        public void LoadInfo(int TestAppointmentID)
        {
            _TestAppointmentID = TestAppointmentID;
            clsTestAppointment testAppointment = clsTestAppointment.Find(TestAppointmentID);
            if (testAppointment == null)
            {
                MessageBox.Show("There is no Test Appointment with this ID");
                return;
            }
            _TestID = testAppointment.TestID;
            _LocalDrivingLicenseApplicationID = testAppointment.LocalDrivingLicenseApplicationID;
            _LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(_LocalDrivingLicenseApplicationID);
            lblLocalDrivingLicenseApplicationID.Text = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();
             lblLicenseClass.Text = _LocalDrivingLicenseApplication.LicenseClassInfo.ClassName;
            lblFullName.Text = _LocalDrivingLicenseApplication.FullName;
            lblTrial.Text = _LocalDrivingLicenseApplication.TotalTrialsPerTest(_TestTypeID).ToString();
            lblDate.Text = clsFormat.DateToShort(testAppointment.AppointmentDate);
            lblFees.Text = testAppointment.PaidFees.ToString();
        }
        private void ctrlScheduledTest_Load(object sender, EventArgs e)
        {

        }
    }
}
