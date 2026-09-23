using DVLD.Licenses.Local_Licenses;
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

namespace DVLD.Licenses.Controls
{
    public partial class ctrlDriverLicenses : UserControl
    {
        private int _DriverID;
        private clsDriver _Driver;
        private DataTable _dtAllDriverLocalLicenses;
        private DataTable _dtAllDriverInternationalLicenses;
        public ctrlDriverLicenses()
        {
            InitializeComponent();
        }

        private void _LoadLocalLicensesInfo()
        {
            _dtAllDriverLocalLicenses = clsDriver.GetLicenses(_DriverID);
            dgvLocalLicensesHistory.DataSource = _dtAllDriverLocalLicenses;
            lblRecordsCount.Text = dgvLocalLicensesHistory.Rows.Count.ToString();

            if(dgvLocalLicensesHistory.Rows.Count > 0)
            {
                dgvLocalLicensesHistory.Columns[0].HeaderText = "Lic.ID";
                dgvLocalLicensesHistory.Columns[0].Width = 110;

                dgvLocalLicensesHistory.Columns[1].HeaderText = "App.ID";
                dgvLocalLicensesHistory.Columns[1].Width = 110;

                dgvLocalLicensesHistory.Columns[2].HeaderText = "Class Name";
                dgvLocalLicensesHistory.Columns[2].Width = 270;

                dgvLocalLicensesHistory.Columns[3].HeaderText = "Issue Date";
                dgvLocalLicensesHistory.Columns[3].Width = 170;

                dgvLocalLicensesHistory.Columns[4].HeaderText = "Expiration Date";
                dgvLocalLicensesHistory.Columns[4].Width = 170;

                dgvLocalLicensesHistory.Columns[5].HeaderText = "Is Active";
                dgvLocalLicensesHistory.Columns[5].Width = 110;
            }


        }

        private void _LoadInternationalLicensesInfo()
        {
            _dtAllDriverInternationalLicenses = clsDriver.GetInternationalLicenses(_DriverID);
            dgvInternationalLicensesHistory.DataSource = _dtAllDriverInternationalLicenses;
            lblRecordsCount.Text = dgvInternationalLicensesHistory.Rows.Count.ToString();
            if (dgvInternationalLicensesHistory.Rows.Count > 0)
            {
                dgvInternationalLicensesHistory.Columns[0].HeaderText = "Int.License ID";
                dgvInternationalLicensesHistory.Columns[0].Width = 160;

                dgvInternationalLicensesHistory.Columns[1].HeaderText = "Application ID";
                dgvInternationalLicensesHistory.Columns[1].Width = 130;

                dgvInternationalLicensesHistory.Columns[2].HeaderText = "L.License ID";
                dgvInternationalLicensesHistory.Columns[2].Width = 130;

                dgvInternationalLicensesHistory.Columns[3].HeaderText = "Issue Date";
                dgvInternationalLicensesHistory.Columns[3].Width = 180;

                dgvInternationalLicensesHistory.Columns[4].HeaderText = "Expiration Date";
                dgvInternationalLicensesHistory.Columns[4].Width = 180;

                dgvInternationalLicensesHistory.Columns[5].HeaderText = "Is Active";
                dgvInternationalLicensesHistory.Columns[5].Width = 120;
            }
        }

        public void LoadInfo(int DriverID)
        {
            _DriverID = DriverID;
            _Driver = clsDriver.FindByDriverID(DriverID);

            if(_Driver == null)
            {
                MessageBox.Show("No Driver with this ID", "ERROR");
                return;
            }

            _LoadLocalLicensesInfo();
            _LoadInternationalLicensesInfo();
        }
        public void LoadInfoByPersonID(int PersonID)
        {
            _Driver = clsDriver.FindByPersonID(PersonID);

            if(_Driver == null)
            {
                MessageBox.Show("No Driver linked with this person ID", "ERROR");
                return;
            }

            _DriverID = _Driver.DriverID;
            _LoadLocalLicensesInfo();
            _LoadInternationalLicensesInfo();
        }
        

        public void Clear()
        {
            _dtAllDriverLocalLicenses.Clear();
            _dtAllDriverInternationalLicenses.Clear();
        }

        private void showLicenseInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = (int)dgvLocalLicensesHistory.CurrentRow.Cells[0].Value;
            frmShowLicenseInfo frm = new frmShowLicenseInfo(LicenseID);
            frm.ShowDialog();
        }
    }
}
