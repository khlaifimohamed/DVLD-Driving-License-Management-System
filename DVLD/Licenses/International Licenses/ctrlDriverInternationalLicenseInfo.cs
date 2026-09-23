using DVLD.Properties;
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
using System.IO;
using DVLD.Global_Classes;
namespace DVLD.Licenses.International_Licenses
{
    public partial class ctrlDriverInternationalLicenseInfo : UserControl
    {
        private int _InternationalLicenseID;
        private clsInternationalLicense _InternationalLicense;

        public int InterationalLicenseID
        {
            get
            {
            return _InternationalLicenseID; 
            } 
        }
        private void _LoadPersonImage()
        {
            if (_InternationalLicense.DriverInfo.PersonInfo.Gender == 0)
            {
                DisplayMaleImage();
            }
            else
            {
                DisplayFemaleImage();
            }
                
           
                

            string ImagePath = _InternationalLicense.DriverInfo.PersonInfo.ImagePath;

            if (ImagePath != "")
                if (File.Exists(ImagePath))
                {
                    RemoveImage();
                    pbPersonImage.Load(ImagePath);
                }
                else
                {
                    MessageBox.Show("Could not find this image: = " + ImagePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

        }
        public void LoadInfo(int InternationalLicenseID)
        {
            _InternationalLicenseID = InternationalLicenseID;
            _InternationalLicense = clsInternationalLicense.Find(InternationalLicenseID);
            if (_InternationalLicense == null)
            {
                MessageBox.Show("Could not find Internationa License ID = " + _InternationalLicenseID.ToString(),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _InternationalLicenseID = -1;
                return;
            }
            lblInternationalID.Text = _InternationalLicenseID.ToString();
            lblApplicationID.Text = _InternationalLicense.ApplicationID.ToString();
            lblIsActive.Text = _InternationalLicense.IsActive ? "Yes" : "No";
            lblLicenseID.Text = _InternationalLicense.IssuedUsingLocalLicenseID.ToString();
            lblName.Text = _InternationalLicense.DriverInfo.PersonInfo.FullName;
            lblNationalNo.Text = _InternationalLicense.DriverInfo.PersonInfo.NationalNo;
            lblGender.Text = _InternationalLicense.DriverInfo.PersonInfo.Gender == 0 ? "Male" : "Female";
            lblDateOfBirth.Text = clsFormat.DateToShort(_InternationalLicense.DriverInfo.PersonInfo.DateOfBirth);
            lblDriverID.Text = _InternationalLicense.DriverID.ToString();
            lblIssueDate.Text = clsFormat.DateToShort(_InternationalLicense.IssueDate);
            lblExpirationDate.Text = clsFormat.DateToShort(_InternationalLicense.ExpirationDate);

            _LoadPersonImage();


        }
        public ctrlDriverInternationalLicenseInfo()
        {
            InitializeComponent();
        }



        #region Image Display Functions

        /// <summary>
        /// Displays a male avatar symbol in the license picture box
        /// </summary>
        public void DisplayMaleImage()
        {
            // Exact dimensions of pictureBox1 (215, 240)
            Bitmap maleImage = new Bitmap(215, 240);

            using (Graphics g = Graphics.FromImage(maleImage))
            {
                // Smooth rendering for crisp graphics
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

                // Dark background matching control theme
                g.Clear(Color.FromArgb(45, 45, 48));

                // Draw circle background
                using (SolidBrush circleBrush = new SolidBrush(Color.FromArgb(52, 152, 219)))
                {
                    g.FillEllipse(circleBrush, 27, 15, 160, 160);
                }

                // Draw male symbol
                using (Font symbolFont = new Font("Segoe UI", 65, FontStyle.Bold))
                using (SolidBrush textBrush = new SolidBrush(Color.White))
                {
                    StringFormat sf = new StringFormat();
                    sf.Alignment = StringAlignment.Center;
                    sf.LineAlignment = StringAlignment.Center;

                    RectangleF textRect = new RectangleF(27, 10, 160, 160);
                    g.DrawString("♂", symbolFont, textBrush, textRect, sf);
                }

                // Draw label "Male"
                using (Font labelFont = new Font("Segoe UI", 12, FontStyle.Bold))
                using (SolidBrush labelBrush = new SolidBrush(Color.FromArgb(200, 200, 205)))
                {
                    StringFormat sf = new StringFormat();
                    sf.Alignment = StringAlignment.Center;

                    RectangleF labelRect = new RectangleF(0, 190, 215, 35);
                    g.DrawString("Male", labelFont, labelBrush, labelRect, sf);
                }

                // Draw decorative circle border
                using (Pen borderPen = new Pen(Color.FromArgb(80, 180, 240), 3))
                {
                    g.DrawEllipse(borderPen, 27, 15, 160, 160);
                }
            }

            if (pbPersonImage.Image != null)
                pbPersonImage.Image.Dispose();

            pbPersonImage.Image = maleImage;
        }

        /// <summary>
        /// Displays a female avatar symbol in the license picture box
        /// </summary>
        public void DisplayFemaleImage()
        {
            // Exact dimensions of pictureBox1 (215, 240)
            Bitmap femaleImage = new Bitmap(215, 240);

            using (Graphics g = Graphics.FromImage(femaleImage))
            {
                // Smooth rendering for crisp graphics
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

                // Dark background matching control theme
                g.Clear(Color.FromArgb(45, 45, 48));

                // Draw circle background
                using (SolidBrush circleBrush = new SolidBrush(Color.FromArgb(155, 89, 182)))
                {
                    g.FillEllipse(circleBrush, 27, 15, 160, 160);
                }

                // Draw female symbol
                using (Font symbolFont = new Font("Segoe UI", 65, FontStyle.Bold))
                using (SolidBrush textBrush = new SolidBrush(Color.White))
                {
                    StringFormat sf = new StringFormat();
                    sf.Alignment = StringAlignment.Center;
                    sf.LineAlignment = StringAlignment.Center;

                    RectangleF textRect = new RectangleF(27, 10, 160, 160);
                    g.DrawString("♀", symbolFont, textBrush, textRect, sf);
                }

                // Draw label "Female"
                using (Font labelFont = new Font("Segoe UI", 12, FontStyle.Bold))
                using (SolidBrush labelBrush = new SolidBrush(Color.FromArgb(200, 200, 205)))
                {
                    StringFormat sf = new StringFormat();
                    sf.Alignment = StringAlignment.Center;

                    RectangleF labelRect = new RectangleF(0, 190, 215, 35);
                    g.DrawString("Female", labelFont, labelBrush, labelRect, sf);
                }

                // Draw decorative circle border
                using (Pen borderPen = new Pen(Color.FromArgb(190, 130, 210), 3))
                {
                    g.DrawEllipse(borderPen, 27, 15, 160, 160);
                }
            }

            if (pbPersonImage.Image != null)
                pbPersonImage.Image.Dispose();

            pbPersonImage.Image = femaleImage;
        }

        /// <summary>
        /// Removes the current image from the license picture box
        /// </summary>
        public void RemoveImage()
        {
            if (pbPersonImage.Image != null)
            {
                pbPersonImage.Image.Dispose();
                pbPersonImage.Image = null;
            }
        }




        #endregion

        
    }
}
