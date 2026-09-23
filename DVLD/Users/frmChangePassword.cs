using DVLD.Classes;
using DVLD_BusinessLayer;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace DVLD.Users
{
    public partial class frmChangePassword : Form
    {

        private int _UserID;
        private clsUser _User;
        public frmChangePassword(int UserID)
        {
            InitializeComponent();
            ApplyStyling();
            _UserID = UserID;
        }

        private void ApplyStyling()
        {
            // Apply rounded corners to password section panel
            ApplyRoundedCorners(pnlPasswordSection, 10);

            // Close button hover effects
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

            // Save button hover effects
            btnSave.MouseEnter += (s, e) =>
            {
                btnSave.BackColor = Color.FromArgb(39, 174, 96);
                btnSave.Font = new Font("Segoe UI", 12.5F, FontStyle.Bold);
            };

            btnSave.MouseLeave += (s, e) =>
            {
                btnSave.BackColor = Color.FromArgb(46, 204, 113);
                btnSave.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            };

            // TextBox focus effects
            ApplyTextBoxEffects();

            // Close button click
            btnClose.Click += (s, e) =>
            {
                this.Close();
            };
        }

        private void ApplyTextBoxEffects()
        {
            TextBox[] passwordBoxes = { txtCurrentPassword, txtNewPassword, txtConfirmPassword };

            foreach (TextBox textBox in passwordBoxes)
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

        private void ApplyRoundedCorners(Control control, int radius)
        {
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            path.AddArc(0, 0, radius * 2, radius * 2, 180, 90);
            path.AddArc(control.Width - radius * 2, 0, radius * 2, radius * 2, 270, 90);
            path.AddArc(control.Width - radius * 2, control.Height - radius * 2, radius * 2, radius * 2, 0, 90);
            path.AddArc(0, control.Height - radius * 2, radius * 2, radius * 2, 90, 90);
            path.CloseFigure();
            control.Region = new Region(path);
        }


        private void _ResetDefaultValues()
        {
            txtCurrentPassword.Text = "";
            txtNewPassword.Text = "";
            txtConfirmPassword.Text = "";
        }
        private void frmChangePassword_Load(object sender, EventArgs e)
        {
            _ResetDefaultValues();
            _User = clsUser.FindByUserID(_UserID);

            if(_User == null)
            {
                MessageBox.Show("Could not Find User with id = " + _UserID.ToString(),"ERROR"
                    ,MessageBoxButtons.OK,MessageBoxIcon.Error);
                this.Close();
                return;
            }

            ctrlUserCard1.LoadUserInfo(_UserID);
        }


        private void txtCurrentPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtCurrentPassword.Text.Trim())){
                e.Cancel = true;
                errorProvider1.SetError(txtCurrentPassword, "Current Password cannot be blank");
                return;
            }
            else
            {
                errorProvider1.SetError(txtCurrentPassword, null);
            }
            if (_User.Password != clsGlobal.HashPassword(txtCurrentPassword.Text.ToString()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtCurrentPassword, "Current Password is wrong");
                return;
            }
            else
            {
                errorProvider1.SetError(txtCurrentPassword, null);
            }
        }

        private void txtNewPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtNewPassword.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtNewPassword, "New Password cannot be blank");
                return;
            }
            else
            {
                errorProvider1.SetError(txtNewPassword, null);
            }
        }

        private void txtConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            if (txtConfirmPassword.Text.Trim() != txtNewPassword.Text.Trim())
            {
                e.Cancel = true;
                errorProvider1.SetError(txtNewPassword, "Password confirmation does not match New Password");
                return;
            }
            else
            {
                errorProvider1.SetError(txtNewPassword, null);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some field are not valid see the red icons", "Validation ERROR"
                    ,MessageBoxButtons.OK,MessageBoxIcon.Information);
                return;
            }
            _User.Password = clsGlobal.HashPassword(txtNewPassword.Text);

            if (_User.Save())
            {
                MessageBox.Show("Password changed Succesfully.","Saved",MessageBoxButtons.OK,MessageBoxIcon.Information);
                _ResetDefaultValues();
            }
            else
            {
                MessageBox.Show("An Error occured Password did not change ", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}