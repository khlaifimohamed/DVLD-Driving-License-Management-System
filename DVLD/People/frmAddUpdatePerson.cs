using DVLD_BusinessLayer;
using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.ComponentModel;
using DVLD.Global_Classes;
using System.IO;
namespace DVLD.People
{
    public partial class frmAddUpdatePerson : Form
    {
        public delegate void DataBackEventHandler(object sender, int PersonID);


        public event DataBackEventHandler DataBack;
        public enum enMode { AddNew = 0,Update=1 };
        public enum enGender { Male = 0,Female = 1 };
        private enMode _Mode;
        private int _PersonID = -1;
        private clsPerson _Person;

        private void _FillCountriesinComboBox()
        {
            DataTable dtCountries = clsCountry.GetAllCountries();
            foreach(DataRow row in dtCountries.Rows)
            {
                cbCountry.Items.Add(row["CountryName"]);
            }
        }
        private void _ResetDefaultValues()
        {
            _FillCountriesinComboBox();

            if(_Mode == enMode.AddNew)
            {
                lblTitle.Text = "Add New Person";
                _Person = new clsPerson();
            }
            else
            {
                lblTitle.Text = "Update Person";
            }
            if (rbMale.Checked)
            {
                DisplayMaleImage();
            }
            else
            {
                DisplayFemaleImage();
            }

            btnRemoveImage.Visible = (pbPersonImage.ImageLocation != null);

            dtpDateOfBirth.MaxDate = DateTime.Now.AddYears(-18);
            dtpDateOfBirth.MinDate = DateTime.Now.AddYears(-100);
            cbCountry.SelectedIndex = cbCountry.FindString("tunisia");


            txtFirstName.Text = "";
            txtSecondName.Text = "";
            txtThirdName.Text = "";
            txtLastName.Text = "";
            txtNationalNo.Text = "";
            txtPhone.Text = "";
            txtEmail.Text = "";
            txtAddress.Text = "";

        }


        private void _LoadData()
        {
            _Person = clsPerson.Find(_PersonID);
            if(_Person == null)
            {
                MessageBox.Show("this Person with ID : " + _PersonID + "DOES NOT EXIST", "Person Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();
                return;
            }


            txtPersonID.Text = _PersonID.ToString();
            txtFirstName.Text = _Person.FirstName;
            txtSecondName.Text = _Person.SecondName;
            txtThirdName.Text = _Person.ThirdName;
            txtLastName.Text = _Person.LastName;
            txtNationalNo.Text = _Person.NationalNo;
            dtpDateOfBirth.Value = _Person.DateOfBirth;
            if(_Person.Gender == 0)
            {
                rbMale.Checked = true;
            }
            else
            {
                rbFemale.Checked = true;
            }
            txtAddress.Text = _Person.Address;
            txtPhone.Text = _Person.Phone;
            txtEmail.Text = _Person.Email;
            cbCountry.SelectedIndex = cbCountry.FindString(_Person.CountryInfo.CountryName);
            if(_Person.ImagePath != "")
            {
                _RemoveGenderImage();
                pbPersonImage.ImageLocation = _Person.ImagePath;
            }


            btnRemoveImage.Visible = (_Person.ImagePath != "");



        }
        private void _RemoveGenderImage()
        {
            // Dispose the current image (male/female symbol)
            if (pbPersonImage.Image != null)
            {
                pbPersonImage.Image.Dispose();
                pbPersonImage.Image = null;
            }

            // Create a default placeholder bitmap
            Bitmap defaultImage = new Bitmap(200, 200);

            using (Graphics g = Graphics.FromImage(defaultImage))
            {
                // Clear background
                g.Clear(Color.FromArgb(60, 60, 65));

                // Draw circle background
                using (SolidBrush circleBrush = new SolidBrush(Color.FromArgb(80, 80, 85)))
                {
                    g.FillEllipse(circleBrush, 30, 20, 140, 140);
                }

                // Draw person icon
                using (Font iconFont = new Font("Segoe UI", 60, FontStyle.Bold))
                using (SolidBrush textBrush = new SolidBrush(Color.FromArgb(160, 160, 165)))
                {
                    StringFormat sf = new StringFormat();
                    sf.Alignment = StringAlignment.Center;
                    sf.LineAlignment = StringAlignment.Center;

                    RectangleF textRect = new RectangleF(30, 20, 140, 140);
                    g.DrawString("👤", iconFont, textBrush, textRect, sf);
                }

                // Draw label "No Photo"
                using (Font labelFont = new Font("Segoe UI", 12, FontStyle.Italic))
                using (SolidBrush labelBrush = new SolidBrush(Color.FromArgb(140, 140, 145)))
                {
                    StringFormat sf = new StringFormat();
                    sf.Alignment = StringAlignment.Center;

                    RectangleF labelRect = new RectangleF(0, 165, 200, 35);
                    g.DrawString("No Photo", labelFont, labelBrush, labelRect, sf);
                }

                // Draw dashed circle border
                using (Pen borderPen = new Pen(Color.FromArgb(100, 100, 105), 2))
                {
                    borderPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                    g.DrawEllipse(borderPen, 30, 20, 140, 140);
                }
            }

            // Set the default image
            pbPersonImage.Image = defaultImage;
        }
        private void frmAddUpdatePerson_Load(object sender, EventArgs e)
        {
            _ResetDefaultValues();
            if (_Mode == enMode.Update)
            {
                _LoadData();
            }
        }

        public frmAddUpdatePerson()
        {
            InitializeComponent();
            ApplyAdvancedStyling();
            _Mode = enMode.AddNew;
        }
        public frmAddUpdatePerson(int PersonID)
        {
            InitializeComponent();
            ApplyAdvancedStyling();
            _Mode = enMode.Update;
            _PersonID = PersonID;
        }

        private void ApplyAdvancedStyling()
        {
            this.AutoValidate = AutoValidate.EnableAllowFocusChange;

            // 2. Wire up Validation Events to TextBoxes
            txtFirstName.Validating += ValidateEmptyTextBox;
            txtLastName.Validating += ValidateEmptyTextBox;
            txtNationalNo.Validating += txtNationalNo_Validating;
            txtEmail.Validating += txtEmail_Validating;
            // Apply rounded corners to panels
            ApplyRoundedCorners(pnlImageSection, 12);
            ApplyRoundedCorners(pnlPersonInfo, 12);
            ApplyRoundedCorners(pnlContactInfo, 12);

            // Style ComboBox
            cbCountry.DrawMode = DrawMode.OwnerDrawFixed;
            cbCountry.DrawItem += ComboBox_DrawItem;

            // Add hover effects to image section panel
            pnlImageSection.MouseEnter += (s, e) => {
                pnlImageSection.BackColor = Color.FromArgb(55, 55, 60);
            };
            pnlImageSection.MouseLeave += (s, e) => {
                pnlImageSection.BackColor = Color.FromArgb(50, 50, 54);
            };

            // Button hover effects
            ApplyButtonEffects();

            // TextBox focus effects
            ApplyTextBoxEffects();

            // Add shadow effect to image panel
            pnlImageSection.Paint += PnlImageSection_Paint;
        }

        private void ApplyButtonEffects()
        {
            // Set Image button
            btnSetImage.MouseEnter += (s, e) => {
                btnSetImage.BackColor = Color.FromArgb(41, 128, 185);
            };
            btnSetImage.MouseLeave += (s, e) => {
                btnSetImage.BackColor = Color.FromArgb(52, 152, 219);
            };

            // Remove Image button
            btnRemoveImage.MouseEnter += (s, e) => {
                btnRemoveImage.BackColor = Color.FromArgb(231, 76, 60);
            };
            btnRemoveImage.MouseLeave += (s, e) => {
                btnRemoveImage.BackColor = Color.FromArgb(85, 85, 90);
            };

            // Save button
            btnSave.MouseEnter += (s, e) => {
                btnSave.BackColor = Color.FromArgb(39, 174, 96);
            };
            btnSave.MouseLeave += (s, e) => {
                btnSave.BackColor = Color.FromArgb(46, 204, 113);
            };

            // Close button
            btnClose.MouseEnter += (s, e) => {
                btnClose.BackColor = Color.FromArgb(231, 76, 60);
            };
            btnClose.MouseLeave += (s, e) => {
                btnClose.BackColor = Color.FromArgb(85, 85, 90);
            };
        }

        private void ApplyTextBoxEffects()
        {
            foreach (Control control in this.Controls)
            {
                ApplyTextBoxEffectsRecursive(control);
            }
        }

        private void ApplyTextBoxEffectsRecursive(Control parent)
        {
            foreach (Control control in parent.Controls)
            {
                if (control is TextBox textBox)
                {
                    textBox.Enter += (s, e) => {
                        textBox.BackColor = Color.FromArgb(70, 70, 75);
                    };
                    textBox.Leave += (s, e) => {
                        textBox.BackColor = Color.FromArgb(60, 60, 65);
                    };
                    textBox.MouseEnter += (s, e) => {
                        if (!textBox.Focused)
                            textBox.BackColor = Color.FromArgb(65, 65, 70);
                    };
                    textBox.MouseLeave += (s, e) => {
                        if (!textBox.Focused)
                            textBox.BackColor = Color.FromArgb(60, 60, 65);
                    };
                }
                else if (control.HasChildren)
                {
                    ApplyTextBoxEffectsRecursive(control);
                }
            }
        }

        private void ComboBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;

            e.DrawBackground();

            bool isSelected = (e.State & DrawItemState.Selected) == DrawItemState.Selected;

            Color backgroundColor = isSelected
                ? Color.FromArgb(52, 152, 219)
                : Color.FromArgb(60, 60, 65);

            Color textColor = isSelected
                ? Color.White
                : Color.FromArgb(220, 220, 225);

            using (SolidBrush backgroundBrush = new SolidBrush(backgroundColor))
            using (SolidBrush textBrush = new SolidBrush(textColor))
            {
                e.Graphics.FillRectangle(backgroundBrush, e.Bounds);

                Rectangle textRect = new Rectangle(
                    e.Bounds.X + 8,
                    e.Bounds.Y + 4,
                    e.Bounds.Width - 16,
                    e.Bounds.Height - 8);

                e.Graphics.DrawString(
                    cbCountry.Items[e.Index].ToString(),
                    new Font("Segoe UI", 10F),
                    textBrush,
                    textRect);
            }
        }

        private void PnlImageSection_Paint(object sender, PaintEventArgs e)
        {
            // Draw subtle border
            using (Pen borderPen = new Pen(Color.FromArgb(70, 70, 75), 2))
            {
                Rectangle rect = new Rectangle(
                    0, 0,
                    pnlImageSection.Width - 1,
                    pnlImageSection.Height - 1);
                e.Graphics.DrawRectangle(borderPen, rect);
            }

            // Draw drop shadow effect
            using (GraphicsPath shadowPath = new GraphicsPath())
            {
                shadowPath.AddRectangle(new Rectangle(2, 2, pnlImageSection.Width + 2, pnlImageSection.Height + 2));
                using (PathGradientBrush shadowBrush = new PathGradientBrush(shadowPath))
                {
                    shadowBrush.CenterColor = Color.FromArgb(20, 0, 0, 0);
                    shadowBrush.SurroundColors = new[] { Color.Transparent };
                    e.Graphics.FillPath(shadowBrush, shadowPath);
                }
            }
        }

        private void ApplyRoundedCorners(Control control, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddArc(0, 0, radius * 2, radius * 2, 180, 90);
            path.AddArc(control.Width - radius * 2, 0, radius * 2, radius * 2, 270, 90);
            path.AddArc(control.Width - radius * 2, control.Height - radius * 2, radius * 2, radius * 2, 0, 90);
            path.AddArc(0, control.Height - radius * 2, radius * 2, radius * 2, 90, 90);
            path.CloseFigure();
            control.Region = new Region(path);
        }

        private void DisplayFemaleImage()
        {
            // Create a 200x200 bitmap for female avatar
            Bitmap femaleImage = new Bitmap(200, 200);

            using (Graphics g = Graphics.FromImage(femaleImage))
            {
                // Clear background
                g.Clear(Color.FromArgb(60, 60, 65));

                // Draw circle background
                using (SolidBrush circleBrush = new SolidBrush(Color.FromArgb(155, 89, 182)))
                {
                    g.FillEllipse(circleBrush, 30, 20, 140, 140);
                }

                // Draw female symbol (♀)
                using (Font symbolFont = new Font("Segoe UI", 60, FontStyle.Bold))
                using (SolidBrush textBrush = new SolidBrush(Color.White))
                {
                    StringFormat sf = new StringFormat();
                    sf.Alignment = StringAlignment.Center;
                    sf.LineAlignment = StringAlignment.Center;

                    RectangleF textRect = new RectangleF(30, 20, 140, 140);
                    g.DrawString("♀", symbolFont, textBrush, textRect, sf);
                }

                // Draw label "Female"
                using (Font labelFont = new Font("Segoe UI", 14, FontStyle.Bold))
                using (SolidBrush labelBrush = new SolidBrush(Color.FromArgb(200, 200, 205)))
                {
                    StringFormat sf = new StringFormat();
                    sf.Alignment = StringAlignment.Center;

                    RectangleF labelRect = new RectangleF(0, 165, 200, 35);
                    g.DrawString("Female", labelFont, labelBrush, labelRect, sf);
                }

                // Draw decorative circle border
                using (Pen borderPen = new Pen(Color.FromArgb(190, 130, 210), 3))
                {
                    g.DrawEllipse(borderPen, 30, 20, 140, 140);
                }
            }

            // Set the image to picture box
            if (pbPersonImage.Image != null)
                pbPersonImage.Image.Dispose();

            pbPersonImage.Image = femaleImage;
        }

        
        private void DisplayMaleImage()
        {
            // Create a 200x200 bitmap for male avatar
            Bitmap maleImage = new Bitmap(200, 200);

            using (Graphics g = Graphics.FromImage(maleImage))
            {
                // Clear background
                g.Clear(Color.FromArgb(60, 60, 65));

                // Draw circle background
                using (SolidBrush circleBrush = new SolidBrush(Color.FromArgb(52, 152, 219)))
                {
                    g.FillEllipse(circleBrush, 30, 20, 140, 140);
                }

                // Draw male symbol (♂)
                using (Font symbolFont = new Font("Segoe UI", 60, FontStyle.Bold))
                using (SolidBrush textBrush = new SolidBrush(Color.White))
                {
                    StringFormat sf = new StringFormat();
                    sf.Alignment = StringAlignment.Center;
                    sf.LineAlignment = StringAlignment.Center;

                    RectangleF textRect = new RectangleF(30, 20, 140, 140);
                    g.DrawString("♂", symbolFont, textBrush, textRect, sf);
                }

                // Draw label "Male"
                using (Font labelFont = new Font("Segoe UI", 14, FontStyle.Bold))
                using (SolidBrush labelBrush = new SolidBrush(Color.FromArgb(200, 200, 205)))
                {
                    StringFormat sf = new StringFormat();
                    sf.Alignment = StringAlignment.Center;

                    RectangleF labelRect = new RectangleF(0, 165, 200, 35);
                    g.DrawString("Male", labelFont, labelBrush, labelRect, sf);
                }

                // Draw decorative circle border
                using (Pen borderPen = new Pen(Color.FromArgb(80, 180, 240), 3))
                {
                    g.DrawEllipse(borderPen, 30, 20, 140, 140);
                }
            }

            // Set the image to picture box
            if (pbPersonImage.Image != null)
                pbPersonImage.Image.Dispose();

            pbPersonImage.Image = maleImage;
        }

        
        
        private bool _HandlePersonImage()
        {

            if(_Person.ImagePath != pbPersonImage.ImageLocation)
            {
                if(_Person.ImagePath != "")
                {
                    try
                    {
                        File.Delete(_Person.ImagePath);
                    }catch(IOException)
                    {

                    }
                }
            }

            if(pbPersonImage.ImageLocation != null)
            {
                string SourceImageFile =  pbPersonImage.ImageLocation.ToString();

                if(clsUtil.CopyImageToProjectImagesFolder(ref SourceImageFile))
                {
                    pbPersonImage.ImageLocation = SourceImageFile;
                    pbPersonImage.ImageLocation = SourceImageFile;
                    return true;
                }
                else
                {
                    MessageBox.Show("Error Copying Image File", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some Fields are not valid, put the mouse over the red icon", "ERROR",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            if (!_HandlePersonImage())
            {
              return;
            }

            int NationalityCountryID = clsCountry.Find(cbCountry.Text).ID;

            _Person.FirstName = txtFirstName.Text.Trim();
            _Person.SecondName = txtSecondName.Text.Trim();
            _Person.ThirdName = txtThirdName.Text.Trim();
            _Person.LastName = txtLastName.Text.Trim();
            _Person.NationalNo = txtNationalNo.Text.Trim();
            _Person.Email = txtEmail.Text.Trim();
            _Person.Phone = txtPhone.Text.Trim();
            _Person.Address = txtAddress.Text.Trim();
            _Person.DateOfBirth = dtpDateOfBirth.Value;

            if (rbMale.Checked)
            {
                _Person.Gender = (short)enGender.Male;
            }
            else
            {
                _Person.Gender = (short)enGender.Female;
            }
            _Person.NationalCountryID = NationalityCountryID;

            if(pbPersonImage.ImageLocation != null)
            {
                _Person.ImagePath = pbPersonImage.ImageLocation;
            }
            else
            {
                _Person.ImagePath = "";
            }
            if (_Person.Save())
            {
                txtPersonID.Text = _Person.PersonID.ToString();

                _Mode = enMode.Update;

                lblTitle.Text = "Update Person";


                MessageBox.Show("Data Saved Successfully .","SAVED",MessageBoxButtons.OK, MessageBoxIcon.Information);

                DataBack?.Invoke(this, _Person.PersonID);

            }
            else
            {
                MessageBox.Show("ERROR: Data is not Saved Successfully ", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void ValidateEmptyTextBox(object sender, CancelEventArgs e)
        {

            // First: set AutoValidate property of your Form to EnableAllowFocusChange in designer 
            TextBox Temp = ((TextBox) sender);
            if (string.IsNullOrEmpty(Temp.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(Temp, "This field is required!");
            }
            else
            {
                //e.Cancel = false;
                errorProvider1.SetError(Temp, null);
            }

        }


        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
            //no need to validate the email incase it's empty.
            if (txtEmail.Text.Trim() == "")
                return;

            //validate email format
            if (!clsValidation.ValidateEmail(txtEmail.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtEmail, "Invalid Email Address Format!");
            }
            else
            {
                errorProvider1.SetError(txtEmail, null);
            }
            ;

        }


        private void txtNationalNo_Validating(object sender, CancelEventArgs e)
        {

            if (string.IsNullOrEmpty(txtNationalNo.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtNationalNo, "This field is required!");
                return;
            }
            else
            {
                errorProvider1.SetError(txtNationalNo, null);
            }

            //Make sure the national number is not used by another person
            if (txtNationalNo.Text.Trim() != _Person.NationalNo && clsPerson.IsPersonExist(txtNationalNo.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtNationalNo, "National Number is used for another person!");

            }
            else
            {
                errorProvider1.SetError(txtNationalNo, null);
            }
        }

        private void rbMale_CheckedChanged(object sender, EventArgs e)
        {
            if (pbPersonImage.ImageLocation == null)
            {
                DisplayMaleImage();
            }
        }

        private void rbFemale_CheckedChanged(object sender, EventArgs e)
        {
            if (pbPersonImage.ImageLocation == null)
            {
                DisplayFemaleImage();
            }
        }

        private void btnSetImage_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;
            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string selectedFilePath = openFileDialog1.FileName;
                pbPersonImage.Load(selectedFilePath);
                btnRemoveImage.Visible = true;
            }
        }

        private void btnRemoveImage_Click(object sender, EventArgs e)
        {
            pbPersonImage.ImageLocation = null;
            if (rbFemale.Checked)
            {
                DisplayFemaleImage();
            }
            else
            {
                DisplayMaleImage();
            }
            btnRemoveImage.Visible = false;
        }
    }
}