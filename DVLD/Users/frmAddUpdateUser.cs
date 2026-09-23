using DVLD.Classes;
using DVLD_BusinessLayer;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace DVLD.Users
{
    public partial class frmAddUpdateUser : Form
    {

        public enum enMode { AddNew = 0 , Update = 1 };

        private enMode _Mode;

        private int _UserID = -1;

        clsUser _User;
        public frmAddUpdateUser()
        {
            InitializeComponent();
            ApplyStyling();
            _Mode = enMode.AddNew;
        }

        private void ApplyStyling()
        {
            tcUserInfo.DrawMode = TabDrawMode.OwnerDrawFixed;
            tcUserInfo.Appearance = TabAppearance.Normal;
            tcUserInfo.SizeMode = TabSizeMode.Fixed;
            tcUserInfo.ItemSize = new Size(180, 40);

            tcUserInfo.DrawItem -= tabControl1_DrawItem;
            tcUserInfo.DrawItem += tabControl1_DrawItem;

            tcUserInfo.Invalidate();

            ApplyButtonEffects();
            ApplyTextBoxEffects();
            ApplyCheckBoxEffects();
        }

        private void ApplyButtonEffects()
        {
            btnPersonInfoNext.MouseEnter += (s, e) =>
            {
                btnPersonInfoNext.BackColor = Color.FromArgb(41, 128, 185);
                btnPersonInfoNext.Font = new Font("Segoe UI", 11.5F, FontStyle.Bold);
            };

            btnPersonInfoNext.MouseLeave += (s, e) =>
            {
                btnPersonInfoNext.BackColor = Color.FromArgb(52, 152, 219);
                btnPersonInfoNext.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            };

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

            button3.MouseEnter += (s, e) =>
            {
                button3.BackColor = Color.FromArgb(231, 76, 60);
                button3.Font = new Font("Segoe UI", 12.5F, FontStyle.Bold);
            };

            button3.MouseLeave += (s, e) =>
            {
                button3.BackColor = Color.FromArgb(85, 85, 90);
                button3.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            };
        }

        private void ApplyTextBoxEffects()
        {
            TextBox[] textBoxes = { txtUserName, txtPassword, txtConfirmPassword };

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

        private void ApplyCheckBoxEffects()
        {
            chkIsActive.MouseEnter += (s, e) =>
            {
                chkIsActive.ForeColor = Color.FromArgb(80, 230, 140);
            };

            chkIsActive.MouseLeave += (s, e) =>
            {
                chkIsActive.ForeColor = Color.FromArgb(46, 204, 113);
            };
        }

        private void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0)
                return;

            Graphics g = e.Graphics;
            Rectangle rect = e.Bounds;

            bool selected = (e.State & DrawItemState.Selected) == DrawItemState.Selected;

            Color backColor = selected
                ? Color.FromArgb(46, 204, 113)
                : Color.FromArgb(60, 60, 65);

            Color textColor = Color.White;

            using (SolidBrush brush = new SolidBrush(backColor))
            {
                g.FillRectangle(brush, rect);
            }

            if (selected)
            {
                using (Pen pen = new Pen(Color.White, 2))
                {
                    g.DrawRectangle(pen, rect.X, rect.Y, rect.Width - 1, rect.Height - 1);
                }
            }

            TextRenderer.DrawText(
                g,
                tcUserInfo.TabPages[e.Index].Text,
                new Font("Segoe UI", 11F, FontStyle.Bold),
                rect,
                textColor,
                TextFormatFlags.HorizontalCenter |
                TextFormatFlags.VerticalCenter |
                TextFormatFlags.SingleLine);

            e.DrawFocusRectangle();
        }

        public frmAddUpdateUser(int UserID)
        {
            InitializeComponent();
            ApplyStyling();
            _Mode = enMode.Update;
            _UserID = UserID;
        }
        private void _ResetDefaultValues()
        {
            if(_Mode == enMode.AddNew)
            {
                lblTitle.Text = "👤 Add New User";
                this.Text = "Add New User";
                _User = new clsUser();

                tpLoginInfo.Enabled = false;

            }
            else
            {
                lblTitle.Text = "👤 Update User";
                this.Text = "Update User";
                tpLoginInfo.Enabled = true;
                btnSave.Enabled = true;
            }

            txtUserName.Text = "";
            txtPassword.Text = "";
            txtConfirmPassword.Text = "";
            chkIsActive.Checked = true;
        }

        private void _LoadData()
        {
            _User = clsUser.FindByUserID(_UserID);
            ctrlPersonCardWithFilter1.FilterEnable = false;

            if(_User == null)
            {
                MessageBox.Show("No User with ID = " + _UserID, "User Not Found");
                this.Close();
                return;
            }
            lblUserID.Text = _User.UserID.ToString();
            txtUserName.Text = _User.UserName;
            txtPassword.Text = _User.Password;
            txtConfirmPassword.Text = _User.Password;
            chkIsActive.Checked = _User.isActive;
            ctrlPersonCardWithFilter1.LoadPersonInfo(_User.PersonID);

        }
        private void frmAddUpdateUser_Load(object sender, EventArgs e)
        {
            _ResetDefaultValues();
            if(_Mode == enMode.Update)
            {
                _LoadData();
            }
        }

        private void btnPersonInfoNext_Click(object sender, EventArgs e)
        {
            if(_Mode == enMode.Update)
            {
                btnSave.Enabled = true;
                tpLoginInfo.Enabled = true;
                tcUserInfo.SelectedTab = tcUserInfo.TabPages["tpLoginInfo"];
                return;
            }

            if(ctrlPersonCardWithFilter1.PersonID != -1)
            {
                if (clsUser.isUserExistForPersonID(ctrlPersonCardWithFilter1.PersonID))
                {
                    MessageBox.Show("Selected Person already has a user choose another one", "ERROR");
                }
                else
                {
                    btnSave.Enabled = true;
                    tpLoginInfo.Enabled = true;
                    tcUserInfo.SelectedTab = tcUserInfo.TabPages["tpLoginInfo"];
                }
            }
            else
            {
                MessageBox.Show("Please Select a person", "Select a person");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fields are not valid,put the mouse over the red icon", "Validation ERROR");
                return;
            }
            _User.PersonID = ctrlPersonCardWithFilter1.PersonID;
            _User.UserName = txtUserName.Text.Trim();
            _User.Password = clsGlobal.HashPassword(txtPassword.Text.Trim());
            _User.isActive = chkIsActive.Checked;
            if (_User.Save())
            {
                lblUserID.Text = _User.UserID.ToString();
                _Mode = enMode.Update;
                lblTitle.Text = "👤 Update User";
                this.Text = "Update User";
                MessageBox.Show("Data Saved Succesfully.", "Saved");
            }
            else
            {
                MessageBox.Show("ERROR : Data is not Saved Succesfully.", "Failed");
            }

        }

        private void txtConfirmPassword_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (txtConfirmPassword.Text.Trim() != txtPassword.Text.Trim())
            {
                e.Cancel = true;
                errorProvider1.SetError(txtConfirmPassword, "Password Confirmation does not match Password!");
            }
            else
            {
                errorProvider1.SetError(txtConfirmPassword, null);
            }
            ;
        }

        private void txtPassword_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtPassword.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtPassword, "Password cannot be blank");
            }
            else
            {
                errorProvider1.SetError(txtPassword, null);
            }
            ;
        }

        private void txtUserName_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtUserName.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtUserName, "Username cannot be blank");
                return;
            }
            else
            {
                errorProvider1.SetError(txtUserName, null);
            }
            ;
        }

        
    }
}