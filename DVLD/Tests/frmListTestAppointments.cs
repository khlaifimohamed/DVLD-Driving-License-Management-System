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

namespace DVLD.Tests
{
    public partial class frmListTestAppointments : Form
    {
        private DataTable _dtAllTestAppointments;
        private int _LocalDrivingLicenseApplicationID = -1;
        private clsTestType.enTestType _TestTypeID = clsTestType.enTestType.VisionTest;
        public frmListTestAppointments(int LocalDrivingLicenseApplicationID,clsTestType.enTestType TestTypeID)
        {
            InitializeComponent();
            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            _TestTypeID = TestTypeID;
        }


        private void _LoadTestTypeAndTitle()
        {
            switch (_TestTypeID)
            {
                case clsTestType.enTestType.VisionTest:
                    SetToVisionTest();
                    break;
                case clsTestType.enTestType.WrittenTest:
                    SetToWrittenTest(); 
                    break;
                case clsTestType.enTestType.StreetTest: 
                    SetToStreetTest();
                    break;
            }
        }

        #region Test Type Setup Functions

        /// <summary>
        /// Configures the form for Vision Test Appointments
        /// </summary>
        private void SetToVisionTest()
        {
            label1.Text = "👁️";
            label2.Text = "👁️ Vision Test Appointments";
            this.Text = "👁️ Vision Test Appointments";
        }

        /// <summary>
        /// Configures the form for Written (Theory) Test Appointments
        /// </summary>
        private void SetToWrittenTest()
        {
            label1.Text = "📝";
            label2.Text = "📝 Written Test Appointments";
            this.Text = "📝 Written Test Appointments";
        }

        /// <summary>
        /// Configures the form for Street (Practical/Driving) Test Appointments
        /// </summary>
        private void SetToStreetTest()
        {
            label1.Text = "🚗"; // You can also use "🛣️" for a road/street icon
            label2.Text = "🚗 Street Test Appointments";
            this.Text = "🚗 Street Test Appointments";
        }

        #endregion
        private void frmListTestAppointments_Load(object sender, EventArgs e)
        {
            _LoadTestTypeAndTitle();
            ctrlDrivingLicenseApplicationInfo1.LoadApplicationInfoByLocalDrivingAppID(_LocalDrivingLicenseApplicationID);
            _dtAllTestAppointments = clsTestAppointment.GetApplicationTestAppointmentsPerTestType(_LocalDrivingLicenseApplicationID, _TestTypeID);
            lblRecordsCount.Text = _dtAllTestAppointments.Rows.Count.ToString();
            dgvLicenseTestAppointments.DataSource = _dtAllTestAppointments;
            if(dgvLicenseTestAppointments.Rows.Count > 0)
            {
                dgvLicenseTestAppointments.Columns[0].HeaderText = "Appointment ID";
                dgvLicenseTestAppointments.Columns[0].Width = 150;

                dgvLicenseTestAppointments.Columns[1].HeaderText = "Appointment Date";
                dgvLicenseTestAppointments.Columns[1].Width = 150;

                dgvLicenseTestAppointments.Columns[2].HeaderText = "Paid Fees";
                dgvLicenseTestAppointments.Columns[2].Width = 150;

                dgvLicenseTestAppointments.Columns[3].HeaderText = "Is locked";
                dgvLicenseTestAppointments.Columns[3].Width = 150;

            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddNewAppointment_Click(object sender, EventArgs e)
        {
            clsLocalDrivingLicenseApplication localDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(_LocalDrivingLicenseApplicationID);

            if (localDrivingLicenseApplication.IsThereAnActiveScheduledTest(_TestTypeID))
            {
                MessageBox.Show("Person Already have an active appointment for this test ", "ERROR");
                return;
            }

            clsTest LastTest = localDrivingLicenseApplication.GetLastTestPerTestType(_TestTypeID);

            if (LastTest == null)
            {
                frmScheduleTest frm1 = new frmScheduleTest(_LocalDrivingLicenseApplicationID, _TestTypeID);
                frm1.ShowDialog();
                frmListTestAppointments_Load(null, null);
                return;
            }


            if (LastTest.TestResult == true)
            {
                MessageBox.Show("This Person Already Passed this Test", "ERROR");
                return;
            }

            frmScheduleTest frm2 = new frmScheduleTest(LastTest.TestAppointmentInfo.LocalDrivingLicenseApplicationID, _TestTypeID);

            frm2.ShowDialog();
            frmListTestAppointments_Load(null, null);



        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int TestAppointmentID = (int) dgvLicenseTestAppointments.CurrentRow.Cells[0].Value;

            frmScheduleTest frm = new frmScheduleTest(_LocalDrivingLicenseApplicationID
                , _TestTypeID, TestAppointmentID);
            frm.ShowDialog();
            frmListTestAppointments_Load(null, null);
        }

        private void takeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int TestAppointmentID = (int)dgvLicenseTestAppointments.CurrentRow.Cells[0].Value;
            frmTakeTest frm = new frmTakeTest(TestAppointmentID, _TestTypeID);
            frm.ShowDialog();
            frmListTestAppointments_Load(null, null);

        }
    }
}
