using DVLD.Applications.Application_Types;
using DVLD.Applications.International_License;
using DVLD.Applications.Local_Driving_License;
using DVLD.Applications.Release_Detained_License;
using DVLD.Applications.Renew_Local_License;
using DVLD.Applications.Replace_lost_or_damaged_License;
using DVLD.Classes;
using DVLD.Drivers;
using DVLD.Licenses.Detain_Licenses;
using DVLD.People;
using DVLD.People.Controls;
using DVLD.Tests.Test_Types;
using DVLD.Users;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace DVLD
{
    public partial class frmMain : Form
    {
        private frmLogin _frmLogin;
        public frmMain(frmLogin frm)
        {
            InitializeComponent();
            ApplyProfessionalRenderer();
            this.Load += Form1_Load;
            _frmLogin = frm;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           // Optional: Add a centered welcome message or any initialization logic
        }

        private void ApplyProfessionalRenderer()
        {
            // Create a completely custom renderer for the MenuStrip
            menuStrip1.Renderer = new DarkMenuRenderer();

            // Apply the same renderer to all dropdown menus
            foreach (ToolStripMenuItem item in menuStrip1.Items)
            {
                ApplyRendererRecursively(item);
            }
        }

        private void ApplyRendererRecursively(ToolStripMenuItem item)
        {
            item.DropDown.Renderer = new DarkMenuRenderer();
            foreach (ToolStripItem subItem in item.DropDownItems)
            {
                if (subItem is ToolStripMenuItem subMenuItem)
                {
                    ApplyRendererRecursively(subMenuItem);
                }
            }
        }

        /// <summary>
        /// Custom renderer for dark professional menu styling
        /// </summary>
        private class DarkMenuRenderer : ToolStripProfessionalRenderer
        {
            public DarkMenuRenderer() : base(new DarkProfessionalColors())
            {
                this.RoundedEdges = false;
            }

            protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
            {
                if (!e.Item.Selected)
                {
                    base.OnRenderMenuItemBackground(e);
                }
                else
                {
                    Rectangle rc = new Rectangle(Point.Empty, e.Item.Size);
                    Color menuItemColor = e.Item.IsOnDropDown
                        ? Color.FromArgb(68, 68, 72)
                        : Color.FromArgb(58, 58, 62);

                    using (SolidBrush brush = new SolidBrush(menuItemColor))
                    {
                        e.Graphics.FillRectangle(brush, rc);
                    }

                    // Add a left accent border for dropdown items
                    if (e.Item.IsOnDropDown)
                    {
                        Rectangle accentRect = new Rectangle(0, 0, 4, rc.Height);
                        using (SolidBrush accentBrush = new SolidBrush(Color.FromArgb(52, 152, 219)))
                        {
                            e.Graphics.FillRectangle(accentBrush, accentRect);
                        }
                    }
                }
            }

            protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
            {
                e.TextColor = e.Item.Selected
                    ? Color.FromArgb(255, 255, 255)
                    : ((ToolStripMenuItem)e.Item).ForeColor;

                base.OnRenderItemText(e);
            }

            protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e)
            {
                e.Graphics.FillRectangle(
                    new SolidBrush(Color.FromArgb(45, 45, 48)),
                    e.Item.ContentRectangle);

                int y = e.Item.Height / 2;
                using (Pen pen = new Pen(Color.FromArgb(68, 68, 72)))
                {
                    e.Graphics.DrawLine(pen,
                        e.Item.ContentRectangle.Left + 8,
                        y,
                        e.Item.ContentRectangle.Right - 8,
                        y);
                }
            }

            protected override void OnRenderArrow(ToolStripArrowRenderEventArgs e)
            {
                e.ArrowColor = e.Item.Selected
                    ? Color.White
                    : Color.FromArgb(180, 180, 180);
                base.OnRenderArrow(e);
            }

            protected override void OnRenderImageMargin(ToolStripRenderEventArgs e)
            {
                using (SolidBrush brush = new SolidBrush(Color.FromArgb(40, 40, 44)))
                {
                    e.Graphics.FillRectangle(brush, e.AffectedBounds);
                }
            }

            protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
            {
                if (e.ToolStrip.IsDropDown)
                {
                    using (Pen pen = new Pen(Color.FromArgb(68, 68, 72)))
                    {
                        e.Graphics.DrawRectangle(pen,
                            0, 0,
                            e.ToolStrip.Width - 1,
                            e.ToolStrip.Height - 1);
                    }
                }
            }
        }

        /// <summary>
        /// Custom color table for the dark professional theme
        /// </summary>
        private class DarkProfessionalColors : ProfessionalColorTable
        {
            public override Color MenuStripGradientBegin => Color.FromArgb(30, 30, 30);
            public override Color MenuStripGradientEnd => Color.FromArgb(30, 30, 30);

            public override Color ToolStripDropDownBackground => Color.FromArgb(45, 45, 48);
            public override Color MenuBorder => Color.FromArgb(68, 68, 72);

            public override Color MenuItemSelected => Color.FromArgb(58, 58, 62);
            public override Color MenuItemSelectedGradientBegin => Color.FromArgb(58, 58, 62);
            public override Color MenuItemSelectedGradientEnd => Color.FromArgb(58, 58, 62);

            public override Color MenuItemBorder => Color.FromArgb(68, 68, 72);
            public override Color MenuItemPressedGradientBegin => Color.FromArgb(55, 55, 60);
            public override Color MenuItemPressedGradientEnd => Color.FromArgb(55, 55, 60);

            public override Color ImageMarginGradientBegin => Color.FromArgb(40, 40, 44);
            public override Color ImageMarginGradientMiddle => Color.FromArgb(40, 40, 44);
            public override Color ImageMarginGradientEnd => Color.FromArgb(40, 40, 44);

            public override Color SeparatorDark => Color.FromArgb(68, 68, 72);
            public override Color SeparatorLight => Color.FromArgb(68, 68, 72);

            public override Color ButtonPressedBorder => Color.FromArgb(68, 68, 72);
            public override Color ButtonSelectedBorder => Color.FromArgb(68, 68, 72);
        }

        private void peopleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            frmListPeople Form1 = new frmListPeople();
           
            Form1.ShowDialog();
        }

        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListUsers Form1 = new frmListUsers(); ;
            Form1.ShowDialog();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {

        }

        private void signOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsGlobal.CurrentUser = null;
            _frmLogin.Show();
            this.Close();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmChangePassword frm = new frmChangePassword(clsGlobal.CurrentUser.UserID);
            frm.ShowDialog();
        }

        private void currentUserINFOToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUserInfo frm = new frmUserInfo(clsGlobal.CurrentUser.UserID);
            frm.ShowDialog();
        }

        private void manageApplicationsTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListApplicationTypes frm = new frmListApplicationTypes();
            frm.ShowDialog();
        }

        private void manageTestTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListTestTypes frm = new frmListTestTypes();
            frm.ShowDialog();
        }

        private void localDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListLocalDrivingLicenseApplications frm = new frmListLocalDrivingLicenseApplications();
            frm.ShowDialog();
        }

        private void localLicenceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdateLocalDrivingLicenseApplication frm = new frmAddUpdateLocalDrivingLicenseApplication();
            frm.ShowDialog();
        }

        private void driversToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListDrivers frm = new frmListDrivers(); 
            frm.ShowDialog();
        }

        private void releaseDetainedLicenseToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedLicenseApplication frm = new frmReleaseDetainedLicenseApplication();
            frm.ShowDialog();
        }

        private void detainLicenseToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmDetainLicenseApplication frm = new frmDetainLicenseApplication();
            frm.ShowDialog();
        }

        private void manageDetainedLicensesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListDetainedLicenses frm = new frmListDetainedLicenses();
            frm.ShowDialog();
        }

        private void internationDrivingLicenseApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListInternationalLicenseApplications frm = new frmListInternationalLicenseApplications();
            frm.ShowDialog();
        }

        private void retakeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListLocalDrivingLicenseApplications frm = new frmListLocalDrivingLicenseApplications();
            frm.ShowDialog();
        }

        private void releaseDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedLicenseApplication frm = new frmReleaseDetainedLicenseApplication();
            frm.ShowDialog();
        }

        private void replacementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReplaceLostOrDamagedLicenseApplication frm = new frmReplaceLostOrDamagedLicenseApplication();
            frm.ShowDialog();
        }

        private void renewDrivingLicenceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRenewLocalDrivingLicenseApplication frm = new frmRenewLocalDrivingLicenseApplication();
            frm.ShowDialog();
        }

        private void internationalLicenceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmNewInternationalLicenseApplication frm = new frmNewInternationalLicenseApplication();
            frm.ShowDialog();
        }
    }
}