using DVLD_BusinessLayer;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using DVLD.Global_Classes;
namespace DVLD.Applications.Application_Types
{
    public partial class frmEditApplicationTypes : Form
    {
        private int _ApplicationTypeID = -1;

        private clsApplicationType _ApplicationType;
        public frmEditApplicationTypes(int ApplicationTypeID)
        {
            InitializeComponent();
            ApplyStyling();
            _ApplicationTypeID = ApplicationTypeID;
        }

        private void frmEditApplicationTypes_Load(object sender, EventArgs e)
        {
            lblApplicationTypeID.Text = _ApplicationTypeID.ToString();

            _ApplicationType = clsApplicationType.Find(_ApplicationTypeID);

            if(_ApplicationType != null)
            {
                txtTitle.Text = _ApplicationType.Title;
                txtFees.Text = _ApplicationType.Fees.ToString();
            }
        }

        private void ApplyStyling()
        {
            // Save button hover effects
            btnSave.MouseEnter += (s, e) =>
            {
                btnSave.BackColor = Color.FromArgb(39, 174, 96);
                btnSave.Font = new Font("Segoe UI", 11.5F, FontStyle.Bold);
            };
            btnSave.MouseLeave += (s, e) =>
            {
                btnSave.BackColor = Color.FromArgb(46, 204, 113);
                btnSave.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            };

            // Close button hover effects
            btnClose.MouseEnter += (s, e) =>
            {
                btnClose.BackColor = Color.FromArgb(231, 76, 60);
                btnClose.Font = new Font("Segoe UI", 11.5F, FontStyle.Bold);
            };
            btnClose.MouseLeave += (s, e) =>
            {
                btnClose.BackColor = Color.FromArgb(85, 85, 90);
                btnClose.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            };

            // Close button click
            btnClose.Click += (s, e) =>
            {
                this.Close();
            };

            // TextBox focus effects
            ApplyTextBoxEffects();
        }

        private void ApplyTextBoxEffects()
        {
            TextBox[] textBoxes = { txtTitle, txtFees };

            foreach (TextBox textBox in textBoxes)
            {
                textBox.Enter += (s, e) =>
                {
                    textBox.BackColor = Color.FromArgb(70, 70, 75);
                };
                textBox.Leave += (s, e) =>
                {
                    textBox.BackColor = Color.FromArgb(60, 60, 65);
                };
                textBox.MouseEnter += (s, e) =>
                {
                    if (!textBox.Focused)
                        textBox.BackColor = Color.FromArgb(65, 65, 70);
                };
                textBox.MouseLeave += (s, e) =>
                {
                    if (!textBox.Focused)
                        textBox.BackColor = Color.FromArgb(60, 60, 65);
                };
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fields are not valid .Check the red icon", "ERROR");
                return;
            }


            _ApplicationType.Title = txtTitle.Text;
            _ApplicationType.Fees = Convert.ToSingle(txtFees.Text);

            if (_ApplicationType.Save())
            {
                MessageBox.Show("Data Saved Successfully", "Saved");
            }
            else
            {
                MessageBox.Show("ERROR: Data is not saved successfully", "ERROR");
            }
        }

        private void txtTitle_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtTitle.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtTitle, "Title can not be empty");
            }
            else
            {
                errorProvider1.SetError(txtTitle, null);
            }
        }

        private void txtFees_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFees.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFees, "Title can not be empty");
            }
            else
            {
                errorProvider1.SetError(txtFees, null);
            }

            if (!clsValidation.IsNumber(txtFees.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFees, "Invalid Number");
            }
            else
            {
                errorProvider1.SetError(txtFees,null);

            }
        }
    }
}