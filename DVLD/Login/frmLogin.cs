using DVLD.Classes;
using DVLD_BusinessLayer;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace DVLD
{
    public partial class frmLogin : Form
    {
        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;

        public frmLogin()
        {
            InitializeComponent();
            ApplyStyling();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            string UserName = "";
            string Password = "";
            if(clsGlobal.GetStoredCredential(ref UserName,ref Password))
            {
                txtUserName.Text = UserName;
                txtPassword.Text = Password;
                chkRememberMe.Checked = true;
            }
            else
            {
                chkRememberMe.Checked = false;
            }
        }

        private void ApplyStyling()
        {
            // Paint the welcome panel with professional gradient background
            pnlWelcome.Paint += PnlWelcome_Paint;

            // Close button hover effect
            btnClose.MouseEnter += (s, e) =>
            {
                btnClose.BackColor = Color.FromArgb(231, 76, 60);
                btnClose.ForeColor = Color.White;
            };
            btnClose.MouseLeave += (s, e) =>
            {
                btnClose.BackColor = Color.Transparent;
                btnClose.ForeColor = Color.FromArgb(180, 180, 185);
            };

            // Close button click
            btnClose.Click += (s, e) =>
            {
                Application.Exit();
            };

            // Login button hover effect
            btnLogin.MouseEnter += (s, e) =>
            {
                btnLogin.BackColor = Color.FromArgb(39, 174, 96);
            };
            btnLogin.MouseLeave += (s, e) =>
            {
                btnLogin.BackColor = Color.FromArgb(46, 204, 113);
            };

            // TextBox focus effects
            ApplyTextBoxEffects();

            // Form dragging support (since FormBorderStyle is None)
            this.MouseDown += Form_MouseDown;
            this.MouseMove += Form_MouseMove;
            this.MouseUp += Form_MouseUp;
        }

        private void PnlWelcome_Paint(object sender, PaintEventArgs e)
        {
            Panel panel = (Panel)sender;
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // Professional dark gradient background
            using (LinearGradientBrush gradient = new LinearGradientBrush(
                panel.ClientRectangle,
                Color.FromArgb(25, 35, 55),
                Color.FromArgb(15, 25, 45),
                LinearGradientMode.ForwardDiagonal))
            {
                g.FillRectangle(gradient, panel.ClientRectangle);
            }

            // Subtle decorative elements

            // Top-right accent circle
            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddEllipse(300, -100, 300, 300);
                using (PathGradientBrush brush = new PathGradientBrush(path))
                {
                    brush.CenterColor = Color.FromArgb(20, 52, 152, 219);
                    brush.SurroundColors = new[] { Color.FromArgb(0, 52, 152, 219) };
                    g.FillEllipse(brush, 300, -100, 300, 300);
                }
            }

            // Bottom-left accent circle
            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddEllipse(-80, 400, 250, 250);
                using (PathGradientBrush brush = new PathGradientBrush(path))
                {
                    brush.CenterColor = Color.FromArgb(15, 46, 204, 113);
                    brush.SurroundColors = new[] { Color.FromArgb(0, 46, 204, 113) };
                    g.FillEllipse(brush, -80, 400, 250, 250);
                }
            }

            // Small decorative dots
            using (SolidBrush dotBrush = new SolidBrush(Color.FromArgb(25, 255, 255, 255)))
            {
                g.FillEllipse(dotBrush, 60, 80, 6, 6);
                g.FillEllipse(dotBrush, 100, 120, 4, 4);
                g.FillEllipse(dotBrush, 420, 60, 5, 5);
                g.FillEllipse(dotBrush, 380, 110, 4, 4);
                g.FillEllipse(dotBrush, 80, 500, 5, 5);
                g.FillEllipse(dotBrush, 440, 520, 6, 6);
                g.FillEllipse(dotBrush, 150, 540, 4, 4);
            }

            // Subtle grid lines
            using (Pen gridPen = new Pen(Color.FromArgb(8, 255, 255, 255), 1))
            {
                for (int i = 0; i < panel.Width; i += 80)
                {
                    g.DrawLine(gridPen, i, 0, i, panel.Height);
                }
                for (int j = 0; j < panel.Height; j += 80)
                {
                    g.DrawLine(gridPen, 0, j, panel.Width, j);
                }
            }
        }

        private void ApplyTextBoxEffects()
        {
            TextBox[] textBoxes = { txtUserName, txtPassword };

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

        // Form dragging support
        private void Form_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = this.Location;
        }

        private void Form_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point diff = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(diff));
            }
        }

        private void Form_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            clsUser User = clsUser.FindByUserNameAndPassword(txtUserName.Text.Trim(),clsGlobal.HashPassword(txtPassword.Text.Trim()));
            if(User != null)
            {
                if (chkRememberMe.Checked)
                {
                    clsGlobal.RememberUsernameAndPassword(txtUserName.Text.Trim(), txtPassword.Text.Trim());

                }
                else
                {
                    clsGlobal.RememberUsernameAndPassword("", "");
                }

                if (!User.isActive)
                {
                    txtUserName.Focus();
                    MessageBox.Show("Your Account is not active,Contact Admin", "Deactivated");
                    return;
                }
                clsGlobal.CurrentUser = User;
                this.Hide();
                frmMain Frm = new frmMain(this);
                Frm.ShowDialog();
            }
            else
            {
                txtUserName.Focus();
                MessageBox.Show("Invalid UserName/Password", "Wrong credentiels");
            }
        }

        
    }
}