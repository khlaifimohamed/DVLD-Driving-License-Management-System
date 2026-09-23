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
using System.IO;

namespace DVLD.Licenses.Local_Licenses.Controls
{
    public partial class ctrlDriverLicenseInfo : UserControl
    {
        private int _LicenseID;
        private clsLicense _License;

        public int LicenseID
        {
            get
            {
                return _LicenseID;
            }
        }

        public clsLicense SelectedLicenseInfo
        {
            get { return _License; }
        }
        public ctrlDriverLicenseInfo()
        {
            InitializeComponent();
        }

        public void LoadInfo(int LicenseID)
        {
            _LicenseID = LicenseID;
            _License = clsLicense.Find(_LicenseID);
            if( _License == null )
            {
                MessageBox.Show("There is no License With this ID","ERROR");
                _LicenseID = -1;
                return;
            }
            lblLicenseID.Text = _License.LicenseID.ToString();
            lblIsActive.Text = _License.IsActive ? "Yes" : "NO";
            lblIsDetained.Text = _License.IsDetained ? "Yes" : "NO";
            lblClass.Text = _License.LicenseClassInfo.ClassName;
            lblFullName.Text = _License.DriverInfo.PersonInfo.FullName;
            lblNationalNo.Text = _License.DriverInfo.PersonInfo.NationalNo;
            lblGender.Text = _License.DriverInfo.PersonInfo.Gender == 0 ? "Male":"Female";
            lblDateOfBirth.Text = clsFormat.DateToShort(_License.DriverInfo.PersonInfo.DateOfBirth);
            lblDriverID.Text = _License.DriverID.ToString();
            lblIssueDate.Text = clsFormat.DateToShort( _License.IssueDate);
            lblExpirationDate.Text = clsFormat.DateToShort(_License.ExpirationDate);
            lblIssueReason.Text = _License.IssueReasonText;
            lblNotes.Text = _License.Notes == "" ? "No Notes" : _License.Notes;
            _LoadPersonImage();
        }
        private void _LoadPersonImage()
        {
            if(_License.DriverInfo.PersonInfo.Gender == 0)
            {
                DisplayMaleImage();
            }
            else
            {
                DisplayFemaleImage();
            }
            string ImagePath = _License.DriverInfo.PersonInfo.ImagePath;
            if(ImagePath != "")
            {
                if(File.Exists(ImagePath))
                {
                    RemoveGenderImage();
                    pbPersonImage.Load(ImagePath);
                }
                else
                {
                    MessageBox.Show("Could not load the image", "ERROR");
                }
            }
        }

        #region Image Display Functions

        /// <summary>
        /// Displays female avatar symbol in the license picture box
        /// </summary>
        public void DisplayFemaleImage()
        {
            // Exact dimensions of pictureBox1 (229, 243)
            Bitmap femaleImage = new Bitmap(229, 243);

            using (Graphics g = Graphics.FromImage(femaleImage))
            {
                // Smooth edges for professional render
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

                g.Clear(Color.FromArgb(45, 45, 48));

                // Draw circle background
                using (SolidBrush circleBrush = new SolidBrush(Color.FromArgb(155, 89, 182)))
                {
                    g.FillEllipse(circleBrush, 34, 20, 160, 160);
                }

                // Draw female symbol
                using (Font symbolFont = new Font("Segoe UI", 68, FontStyle.Bold))
                using (SolidBrush textBrush = new SolidBrush(Color.White))
                {
                    StringFormat sf = new StringFormat();
                    sf.Alignment = StringAlignment.Center;
                    sf.LineAlignment = StringAlignment.Center;

                    RectangleF textRect = new RectangleF(34, 15, 160, 160);
                    g.DrawString("♀", symbolFont, textBrush, textRect, sf);
                }

                // Draw label "Female"
                using (Font labelFont = new Font("Segoe UI", 13, FontStyle.Bold))
                using (SolidBrush labelBrush = new SolidBrush(Color.FromArgb(200, 200, 205)))
                {
                    StringFormat sf = new StringFormat();
                    sf.Alignment = StringAlignment.Center;

                    RectangleF labelRect = new RectangleF(0, 195, 229, 35);
                    g.DrawString("Female", labelFont, labelBrush, labelRect, sf);
                }

                // Draw decorative circle border
                using (Pen borderPen = new Pen(Color.FromArgb(190, 130, 210), 3))
                {
                    g.DrawEllipse(borderPen, 34, 20, 160, 160);
                }
            }

            if (pbPersonImage.Image != null)
                pbPersonImage.Image.Dispose();

            pbPersonImage.Image = femaleImage;
        }

        /// <summary>
        /// Displays male avatar symbol in the license picture box
        /// </summary>
        public void DisplayMaleImage()
        {
            // Exact dimensions of pictureBox1 (229, 243)
            Bitmap maleImage = new Bitmap(229, 243);

            using (Graphics g = Graphics.FromImage(maleImage))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

                g.Clear(Color.FromArgb(45, 45, 48));

                // Draw circle background
                using (SolidBrush circleBrush = new SolidBrush(Color.FromArgb(52, 152, 219)))
                {
                    g.FillEllipse(circleBrush, 34, 20, 160, 160);
                }

                // Draw male symbol
                using (Font symbolFont = new Font("Segoe UI", 68, FontStyle.Bold))
                using (SolidBrush textBrush = new SolidBrush(Color.White))
                {
                    StringFormat sf = new StringFormat();
                    sf.Alignment = StringAlignment.Center;
                    sf.LineAlignment = StringAlignment.Center;

                    RectangleF textRect = new RectangleF(34, 15, 160, 160);
                    g.DrawString("♂", symbolFont, textBrush, textRect, sf);
                }

                // Draw label "Male"
                using (Font labelFont = new Font("Segoe UI", 13, FontStyle.Bold))
                using (SolidBrush labelBrush = new SolidBrush(Color.FromArgb(200, 200, 205)))
                {
                    StringFormat sf = new StringFormat();
                    sf.Alignment = StringAlignment.Center;

                    RectangleF labelRect = new RectangleF(0, 195, 229, 35);
                    g.DrawString("Male", labelFont, labelBrush, labelRect, sf);
                }

                // Draw decorative circle border
                using (Pen borderPen = new Pen(Color.FromArgb(80, 180, 240), 3))
                {
                    g.DrawEllipse(borderPen, 34, 20, 160, 160);
                }
            }

            if (pbPersonImage.Image != null)
                pbPersonImage.Image.Dispose();

            pbPersonImage.Image = maleImage;
        }

        /// <summary>
        /// Removes the current display image and restores the default license placeholder
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
        /// Displays the default placeholder image inside the license picture box
        /// </summary>
        public void DisplayDefaultImage()
        {
            // Exact dimensions of pictureBox1 (229, 243)
            Bitmap defaultImage = new Bitmap(229, 243);

            using (Graphics g = Graphics.FromImage(defaultImage))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

                g.Clear(Color.FromArgb(45, 45, 48));

                // Draw circle background
                using (SolidBrush circleBrush = new SolidBrush(Color.FromArgb(70, 70, 75)))
                {
                    g.FillEllipse(circleBrush, 34, 20, 160, 160);
                }

                // Draw person icon
                using (Font iconFont = new Font("Segoe UI", 68, FontStyle.Bold))
                using (SolidBrush textBrush = new SolidBrush(Color.FromArgb(150, 150, 155)))
                {
                    StringFormat sf = new StringFormat();
                    sf.Alignment = StringAlignment.Center;
                    sf.LineAlignment = StringAlignment.Center;

                    RectangleF textRect = new RectangleF(34, 15, 160, 160);
                    g.DrawString("👤", iconFont, textBrush, textRect, sf);
                }

                // Draw label "No Photo"
                using (Font labelFont = new Font("Segoe UI", 12, FontStyle.Italic))
                using (SolidBrush labelBrush = new SolidBrush(Color.FromArgb(130, 130, 135)))
                {
                    StringFormat sf = new StringFormat();
                    sf.Alignment = StringAlignment.Center;

                    RectangleF labelRect = new RectangleF(0, 195, 229, 35);
                    g.DrawString("No Photo", labelFont, labelBrush, labelRect, sf);
                }

                // Draw dashed circle border
                using (Pen borderPen = new Pen(Color.FromArgb(90, 90, 95), 2))
                {
                    borderPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                    g.DrawEllipse(borderPen, 34, 20, 160, 160);
                }
            }

            if (pbPersonImage.Image != null)
                pbPersonImage.Image.Dispose();

            pbPersonImage.Image = defaultImage;
        }

        /// <summary>
        /// Displays a real driver image from the file system path in the picture box
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
    }
}
