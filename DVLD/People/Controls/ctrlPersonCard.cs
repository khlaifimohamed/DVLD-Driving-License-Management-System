using DVLD_BusinessLayer;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace DVLD.People.Controls
{
    public partial class ctrlPersonCard : UserControl
    {

        private clsPerson _Person;

        private int _PersonID = -1;

        public int PersonID
        {
            get { return _PersonID; }
        }

        public clsPerson SelectedPersonInfo
        {
            get { return _Person; }
        }


        public void LoadPersonInfo(int PersonID)
        {
            _Person = clsPerson.Find(PersonID);
            if (_Person == null)
            {
                MessageBox.Show("No Person with this ID", "ERROR");
                return;
            }

            _FillPersonInfo();
        }
        public void LoadPersonInfo(string NationalNo)
        {
            _Person = clsPerson.Find(NationalNo);
            if (_Person == null)
            {
                MessageBox.Show("No Person with this ID", "ERROR");
                return;
            }

            _FillPersonInfo();
        }

        public void ResetPersonInfo()
        {
            _PersonID = -1;
            lblPersonIDValue.Text = "[????]";
            lblNameValue.Text = "[????]";
            lblNationalNoValue.Text = "[????]";
            lblGenderValue.Text = "[????]";
            lblEmailValue.Text = "[????]";
            lblAddressValue.Text = "[????]";
            lblDateOfBirthValue.Text = "[????]";
            lblCountryValue.Text = "[????]";
            lblPhoneValue.Text = "[????]";
            DisplayMaleImage();
        }
        
        private void _FillPersonInfo()
        {
            _PersonID = _Person.PersonID;
            lblPersonIDValue.Text = _Person.PersonID.ToString();
            lblNameValue.Text = _Person.FullName;
            lblNationalNoValue.Text = _Person.NationalNo;
            lblGenderValue.Text = _Person.Gender == 0 ? "Male" : "Female";
            lblEmailValue.Text = _Person.Email;
            lblAddressValue.Text = _Person.Address;
            lblDateOfBirthValue.Text = _Person.DateOfBirth.ToShortDateString();
            lblCountryValue.Text = clsCountry.Find(_Person.NationalCountryID).CountryName;
            lblPhoneValue.Text = _Person.Phone;
            LoadPersonImage();
        }

        public void LoadPersonImage()
        {
            if(_Person.Gender == 0)
            {
                DisplayMaleImage();
            }
            else
            {
                DisplayFemaleImage();
            }
            string ImagePath = _Person.ImagePath;
            if(ImagePath != "")
            {
                if (File.Exists(ImagePath))
                {
                    pbPersonImage.ImageLocation = ImagePath;
                }
                else
                {
                    MessageBox.Show("Could not load the Image", "ERROR");
                }
            }
        }

        
        public ctrlPersonCard()
        {
            InitializeComponent();
            ApplyStyling();
            DisplayDefaultImage();
        }

        private void ApplyStyling()
        {
            // Hover effects for Edit button
            

            // Hover effects for button1 (extra edit button if needed)
            btnEditPersonInfo.MouseEnter += (s, e) => {
                btnEditPersonInfo.BackColor = Color.FromArgb(41, 128, 185);
                btnEditPersonInfo.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            };
            btnEditPersonInfo.MouseLeave += (s, e) => {
                btnEditPersonInfo.BackColor = Color.FromArgb(52, 152, 219);
                btnEditPersonInfo.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            };

            
            
        }

        #region Image Display Functions

        /// <summary>
        /// Displays female avatar symbol in the picture box
        /// </summary>
        public void DisplayFemaleImage()
        {
            Bitmap femaleImage = new Bitmap(180, 190);

            using (Graphics g = Graphics.FromImage(femaleImage))
            {
                g.Clear(Color.FromArgb(50, 50, 54));

                // Draw circle background
                using (SolidBrush circleBrush = new SolidBrush(Color.FromArgb(155, 89, 182)))
                {
                    g.FillEllipse(circleBrush, 25, 20, 130, 130);
                }

                // Draw female symbol
                using (Font symbolFont = new Font("Segoe UI", 55, FontStyle.Bold))
                using (SolidBrush textBrush = new SolidBrush(Color.White))
                {
                    StringFormat sf = new StringFormat();
                    sf.Alignment = StringAlignment.Center;
                    sf.LineAlignment = StringAlignment.Center;

                    RectangleF textRect = new RectangleF(25, 20, 130, 130);
                    g.DrawString("♀", symbolFont, textBrush, textRect, sf);
                }

                // Draw label "Female"
                using (Font labelFont = new Font("Segoe UI", 12, FontStyle.Bold))
                using (SolidBrush labelBrush = new SolidBrush(Color.FromArgb(200, 200, 205)))
                {
                    StringFormat sf = new StringFormat();
                    sf.Alignment = StringAlignment.Center;

                    RectangleF labelRect = new RectangleF(0, 155, 180, 30);
                    g.DrawString("Female", labelFont, labelBrush, labelRect, sf);
                }

                // Draw decorative circle border
                using (Pen borderPen = new Pen(Color.FromArgb(190, 130, 210), 3))
                {
                    g.DrawEllipse(borderPen, 25, 20, 130, 130);
                }
            }

            if (pbPersonImage.Image != null)
                pbPersonImage.Image.Dispose();

            pbPersonImage.Image = femaleImage;
        }

        /// <summary>
        /// Displays male avatar symbol in the picture box
        /// </summary>
        public void DisplayMaleImage()
        {
            Bitmap maleImage = new Bitmap(180, 190);

            using (Graphics g = Graphics.FromImage(maleImage))
            {
                g.Clear(Color.FromArgb(50, 50, 54));

                // Draw circle background
                using (SolidBrush circleBrush = new SolidBrush(Color.FromArgb(52, 152, 219)))
                {
                    g.FillEllipse(circleBrush, 25, 20, 130, 130);
                }

                // Draw male symbol
                using (Font symbolFont = new Font("Segoe UI", 55, FontStyle.Bold))
                using (SolidBrush textBrush = new SolidBrush(Color.White))
                {
                    StringFormat sf = new StringFormat();
                    sf.Alignment = StringAlignment.Center;
                    sf.LineAlignment = StringAlignment.Center;

                    RectangleF textRect = new RectangleF(25, 20, 130, 130);
                    g.DrawString("♂", symbolFont, textBrush, textRect, sf);
                }

                // Draw label "Male"
                using (Font labelFont = new Font("Segoe UI", 12, FontStyle.Bold))
                using (SolidBrush labelBrush = new SolidBrush(Color.FromArgb(200, 200, 205)))
                {
                    StringFormat sf = new StringFormat();
                    sf.Alignment = StringAlignment.Center;

                    RectangleF labelRect = new RectangleF(0, 155, 180, 30);
                    g.DrawString("Male", labelFont, labelBrush, labelRect, sf);
                }

                // Draw decorative circle border
                using (Pen borderPen = new Pen(Color.FromArgb(80, 180, 240), 3))
                {
                    g.DrawEllipse(borderPen, 25, 20, 130, 130);
                }
            }

            if (pbPersonImage.Image != null)
                pbPersonImage.Image.Dispose();

            pbPersonImage.Image = maleImage;
        }

        /// <summary>
        /// Removes the current gender display image and restores the default placeholder
        /// </summary>
        public void RemoveGenderImage()
        {
            if (pbPersonImage.Image != null)
            {
                pbPersonImage.Image.Dispose();
                pbPersonImage.Image = null;
            }

            DisplayDefaultImage();
        }

        /// <summary>
        /// Displays the default placeholder image
        /// </summary>
        public void DisplayDefaultImage()
        {
            Bitmap defaultImage = new Bitmap(180, 190);

            using (Graphics g = Graphics.FromImage(defaultImage))
            {
                g.Clear(Color.FromArgb(50, 50, 54));

                // Draw circle background
                using (SolidBrush circleBrush = new SolidBrush(Color.FromArgb(70, 70, 75)))
                {
                    g.FillEllipse(circleBrush, 25, 20, 130, 130);
                }

                // Draw person icon
                using (Font iconFont = new Font("Segoe UI", 55, FontStyle.Bold))
                using (SolidBrush textBrush = new SolidBrush(Color.FromArgb(150, 150, 155)))
                {
                    StringFormat sf = new StringFormat();
                    sf.Alignment = StringAlignment.Center;
                    sf.LineAlignment = StringAlignment.Center;

                    RectangleF textRect = new RectangleF(25, 20, 130, 130);
                    g.DrawString("👤", iconFont, textBrush, textRect, sf);
                }

                // Draw label "No Photo"
                using (Font labelFont = new Font("Segoe UI", 11, FontStyle.Italic))
                using (SolidBrush labelBrush = new SolidBrush(Color.FromArgb(130, 130, 135)))
                {
                    StringFormat sf = new StringFormat();
                    sf.Alignment = StringAlignment.Center;

                    RectangleF labelRect = new RectangleF(0, 155, 180, 30);
                    g.DrawString("No Photo", labelFont, labelBrush, labelRect, sf);
                }

                // Draw dashed circle border
                using (Pen borderPen = new Pen(Color.FromArgb(90, 90, 95), 2))
                {
                    borderPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                    g.DrawEllipse(borderPen, 25, 20, 130, 130);
                }
            }

            if (pbPersonImage.Image != null)
                pbPersonImage.Image.Dispose();

            pbPersonImage.Image = defaultImage;
        }

        /// <summary>
        /// Displays a real image from file path in the picture box
        /// </summary>
        /// <param name="imagePath">Full path to the image file</param>
        public void DisplayRealImage(string imagePath)
        {
            try
            {
                if (!string.IsNullOrEmpty(imagePath) && System.IO.File.Exists(imagePath))
                {
                    Image realImage = Image.FromFile(imagePath);

                    if (pbPersonImage.Image != null)
                        pbPersonImage.Image.Dispose();

                    pbPersonImage.Image = realImage;
                }
                else
                {
                    DisplayDefaultImage();
                }
            }
            catch (Exception)
            {
                DisplayDefaultImage();
            }
        }

        #endregion

        private void btnEditPersonInfo_Click(object sender, EventArgs e)
        {
            frmAddUpdatePerson frm = new frmAddUpdatePerson(_PersonID);
            frm.ShowDialog();
            LoadPersonInfo(_PersonID);
        }

        
    }
}