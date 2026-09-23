using DVLD.Classes;
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
    public partial class frmTakeTest : Form
    {
        private int _AppointmentID;
        private clsTestType.enTestType _TestTypeID = clsTestType.enTestType.VisionTest;

        private int _TestID = -1;
        private clsTest _Test;
        public frmTakeTest(int AppointmentID,clsTestType.enTestType TestTypeID)
        {
            InitializeComponent();
            _AppointmentID = AppointmentID;
            _TestTypeID = TestTypeID;
        }

        private void frmTakeTest_Load(object sender, EventArgs e)
        {
            ctrlScheduledTest1.TestTypeID = _TestTypeID;
            ctrlScheduledTest1.LoadInfo(_AppointmentID);
            if(ctrlScheduledTest1.TestAppointmentID == -1)
            {
                btnSave.Enabled = false;
            }
            else
            {
                btnSave.Enabled = true;
            }

            _TestID = ctrlScheduledTest1.TestID;

            if(_TestID != -1)
            {
                _Test = clsTest.Find(_TestID);
                if (_Test.TestResult)
                {
                    rbPass.Checked = true;
                }
                else
                {
                    rbPass.Checked = false;
                }
                txtNotes.Text = _Test.Notes;
                rbFail.Enabled = false;
                rbPass.Enabled = false;

            }
            else
            {
                _Test = new clsTest();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to save? After that you cannot change the Pass/Fail results after you save?.",
                      "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No
             )
            {
                return;
            }

            _Test.TestAppointmentID = _AppointmentID;
            _Test.TestResult = rbPass.Checked;
            _Test.Notes = txtNotes.Text.Trim();
            _Test.CreatedByUserID = clsGlobal.CurrentUser.UserID;
            if (_Test.Save())
            {
                MessageBox.Show("Data Saved succesfully", "Saved");
                btnSave.Enabled = false;
            }
            else
            {
                MessageBox.Show("Data is not Saved succesfully", "Failed");
                
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
