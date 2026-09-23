using DVLD_BusinessLayer;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace DVLD.Users
{
    public partial class frmListUsers : Form
    {

        private   DataTable _dtAllUsers;
        public frmListUsers()
        {
            InitializeComponent();
            ApplyStyling();
            ApplyContextMenuStyle();
        }

        private void ApplyStyling()
        {
            // Style ComboBoxes
            cbFilterBy.DrawMode = DrawMode.OwnerDrawFixed;
            cbFilterBy.DrawItem += ComboBox_DrawItem;

            cbIsActive.DrawMode = DrawMode.OwnerDrawFixed;
            cbIsActive.DrawItem += ComboBox_DrawItem;

            // Add button hover effects
            btnAddUser.MouseEnter += (s, e) =>
            {
                btnAddUser.BackColor = Color.FromArgb(39, 174, 96);
                btnAddUser.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            };
            btnAddUser.MouseLeave += (s, e) =>
            {
                btnAddUser.BackColor = Color.FromArgb(46, 204, 113);
                btnAddUser.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
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

            // DataGridView styling
            dgvUsers.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(55, 55, 60);
            dgvUsers.AlternatingRowsDefaultCellStyle.ForeColor = Color.FromArgb(200, 200, 205);
            dgvUsers.AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(52, 152, 219);
            dgvUsers.AlternatingRowsDefaultCellStyle.SelectionForeColor = Color.White;
            dgvUsers.RowTemplate.Height = 32;
            dgvUsers.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgvUsers.ColumnHeadersDefaultCellStyle.Padding = new Padding(8, 0, 0, 0);
            dgvUsers.ColumnHeadersHeight = 40;

            // TextBox focus effects
            txtFilterValue.Enter += (s, e) =>
            {
                txtFilterValue.BackColor = Color.FromArgb(65, 65, 70);
            };
            txtFilterValue.Leave += (s, e) =>
            {
                txtFilterValue.BackColor = Color.FromArgb(55, 55, 60);
            };
            txtFilterValue.MouseEnter += (s, e) =>
            {
                if (!txtFilterValue.Focused)
                    txtFilterValue.BackColor = Color.FromArgb(60, 60, 65);
            };
            txtFilterValue.MouseLeave += (s, e) =>
            {
                if (!txtFilterValue.Focused)
                    txtFilterValue.BackColor = Color.FromArgb(55, 55, 60);
            };

            // Close button click
            btnClose.Click += (s, e) =>
            {
                this.Close();
            };
        }

        private void ApplyContextMenuStyle()
        {
            contextMenuStrip1.Renderer = new DarkContextMenuRenderer();

            foreach (ToolStripItem item in contextMenuStrip1.Items)
            {
                item.BackColor = Color.FromArgb(45, 45, 48);
                item.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
                item.Padding = new Padding(10, 7, 18, 7);
                item.Margin = new Padding(0, 1, 0, 1);
            }

            showDetailsToolStripMenuItem.Text = "👁  Show Details";
            showDetailsToolStripMenuItem.ForeColor = Color.FromArgb(220, 220, 225);

            addNewUserToolStripMenuItem.Text = "➕  Add New User";
            addNewUserToolStripMenuItem.ForeColor = Color.FromArgb(46, 204, 113);

            editToolStripMenuItem.Text = "✏️  Edit";
            editToolStripMenuItem.ForeColor = Color.FromArgb(52, 152, 219);

            deleteToolStripMenuItem.Text = "🗑  Delete";
            deleteToolStripMenuItem.ForeColor = Color.FromArgb(231, 76, 60);

            changePasswordToolStripMenuItem.Text = "🔒  Change Password";
            changePasswordToolStripMenuItem.ForeColor = Color.FromArgb(230, 126, 34);

            sendEmailToolStripMenuItem.Text = "✉️  Send Email";
            sendEmailToolStripMenuItem.ForeColor = Color.FromArgb(155, 89, 182);

            sendSMSToolStripMenuItem.Text = "💬  Send SMS";
            sendSMSToolStripMenuItem.ForeColor = Color.FromArgb(241, 196, 15);
        }

        private void ComboBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;

            ComboBox comboBox = (ComboBox)sender;
            e.DrawBackground();

            bool isSelected = (e.State & DrawItemState.Selected) == DrawItemState.Selected;

            Color backgroundColor = isSelected
                ? Color.FromArgb(52, 152, 219)
                : Color.FromArgb(55, 55, 60);

            Color textColor = isSelected
                ? Color.White
                : Color.FromArgb(220, 220, 225);

            using (SolidBrush backgroundBrush = new SolidBrush(backgroundColor))
            using (SolidBrush textBrush = new SolidBrush(textColor))
            {
                e.Graphics.FillRectangle(backgroundBrush, e.Bounds);

                Rectangle textRect = new Rectangle(
                    e.Bounds.X + 8,
                    e.Bounds.Y + 3,
                    e.Bounds.Width - 16,
                    e.Bounds.Height - 6);

                e.Graphics.DrawString(
                    comboBox.Items[e.Index].ToString(),
                    new Font("Segoe UI", 10F),
                    textBrush,
                    textRect);
            }
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            frmAddUpdateUser frm = new frmAddUpdateUser();
            frm.ShowDialog();
            frmListUsers_Load(null, null);
        }

        // ===== Dark Context Menu Renderer =====
        private class DarkContextMenuRenderer : ToolStripProfessionalRenderer
        {
            public DarkContextMenuRenderer() : base(new DarkContextMenuColorTable())
            {
                RoundedEdges = false;
            }

            protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
            {
                Rectangle rect = new Rectangle(Point.Empty, e.Item.Size);

                if (e.Item.Selected && e.Item.Enabled)
                {
                    using (SolidBrush brush = new SolidBrush(Color.FromArgb(62, 62, 66)))
                    {
                        e.Graphics.FillRectangle(brush, rect);
                    }

                    Color accentColor = Color.FromArgb(52, 152, 219);

                    if (e.Item.Text.Contains("Delete"))
                        accentColor = Color.FromArgb(231, 76, 60);
                    else if (e.Item.Text.Contains("Add"))
                        accentColor = Color.FromArgb(46, 204, 113);
                    else if (e.Item.Text.Contains("Password"))
                        accentColor = Color.FromArgb(230, 126, 34);
                    else if (e.Item.Text.Contains("Email"))
                        accentColor = Color.FromArgb(155, 89, 182);
                    else if (e.Item.Text.Contains("SMS"))
                        accentColor = Color.FromArgb(241, 196, 15);

                    using (SolidBrush accentBrush = new SolidBrush(accentColor))
                    {
                        e.Graphics.FillRectangle(accentBrush, new Rectangle(0, 0, 4, rect.Height));
                    }
                }
                else
                {
                    using (SolidBrush brush = new SolidBrush(Color.FromArgb(45, 45, 48)))
                    {
                        e.Graphics.FillRectangle(brush, rect);
                    }
                }
            }

            protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
            {
                if (!e.Item.Enabled)
                {
                    e.TextColor = Color.FromArgb(120, 120, 125);
                }
                else if (e.Item.Selected)
                {
                    e.TextColor = Color.White;
                }
                else
                {
                    e.TextColor = e.Item.ForeColor;
                }

                base.OnRenderItemText(e);
            }

            protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
            {
                using (Pen pen = new Pen(Color.FromArgb(70, 70, 75)))
                {
                    e.Graphics.DrawRectangle(pen, 0, 0, e.ToolStrip.Width - 1, e.ToolStrip.Height - 1);
                }
            }

            protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e)
            {
                using (SolidBrush brush = new SolidBrush(Color.FromArgb(45, 45, 48)))
                {
                    e.Graphics.FillRectangle(brush, e.Item.ContentRectangle);
                }

                using (Pen pen = new Pen(Color.FromArgb(70, 70, 75)))
                {
                    int y = e.Item.Height / 2;
                    e.Graphics.DrawLine(pen, e.Item.ContentRectangle.Left + 8, y, e.Item.ContentRectangle.Right - 8, y);
                }
            }

            protected override void OnRenderArrow(ToolStripArrowRenderEventArgs e)
            {
                e.ArrowColor = e.Item.Selected ? Color.White : Color.FromArgb(180, 180, 185);
                base.OnRenderArrow(e);
            }
        }

        // ===== Dark Context Menu Color Table =====
        private class DarkContextMenuColorTable : ProfessionalColorTable
        {
            public override Color ToolStripDropDownBackground => Color.FromArgb(45, 45, 48);
            public override Color MenuBorder => Color.FromArgb(70, 70, 75);
            public override Color MenuItemSelected => Color.FromArgb(62, 62, 66);
            public override Color MenuItemSelectedGradientBegin => Color.FromArgb(62, 62, 66);
            public override Color MenuItemSelectedGradientEnd => Color.FromArgb(62, 62, 66);
            public override Color MenuItemBorder => Color.FromArgb(70, 70, 75);
            public override Color ImageMarginGradientBegin => Color.FromArgb(40, 40, 44);
            public override Color ImageMarginGradientMiddle => Color.FromArgb(40, 40, 44);
            public override Color ImageMarginGradientEnd => Color.FromArgb(40, 40, 44);
            public override Color SeparatorDark => Color.FromArgb(70, 70, 75);
            public override Color SeparatorLight => Color.FromArgb(70, 70, 75);
        }

        private void frmListUsers_Load(object sender, EventArgs e)
        {
            _dtAllUsers = clsUser.GetAllUsers();
            dgvUsers.DataSource = _dtAllUsers;
            cbFilterBy.SelectedIndex = 0;
            lblRecordsCount.Text = dgvUsers.Rows.Count.ToString();

            if(dgvUsers.Rows.Count > 0)
            {

                dgvUsers.Columns[0].HeaderText = "User ID";
                dgvUsers.Columns[0].Width = 120;

                dgvUsers.Columns[1].HeaderText = "Person ID";
                dgvUsers.Columns[1].Width = 230;

                dgvUsers.Columns[2].HeaderText = "Full Name";
                dgvUsers.Columns[2].Width = 270;

                dgvUsers.Columns[3].HeaderText = "User Name";
                dgvUsers.Columns[3].Width = 270;

                dgvUsers.Columns[4].HeaderText = "Is Active";
                dgvUsers.Columns[4].Width = 220;
            }
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbFilterBy.Text == "is Active")
            {
                txtFilterValue.Visible = false;
                cbIsActive.Visible = true;
                cbIsActive.Focus();
                cbIsActive.SelectedIndex = 0;
            }
            else
            {
                txtFilterValue.Visible = (cbFilterBy.Text != "NONE");
                cbIsActive.Visible = false;
                txtFilterValue.Text = "";
                txtFilterValue.Focus();

            }
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            switch (cbFilterBy.Text)
            {
                case "User ID":
                    FilterColumn = "UserID";
                    break;
                case "User Name":
                    FilterColumn = "UserName";
                    break;
                case "Person ID":
                    FilterColumn = "PersonID";
                    break;
                case "Full Name":
                    FilterColumn = "FullName";
                    break;
                default:
                    FilterColumn = "NONE";
                    break;
            }

            if(txtFilterValue.Text.Trim() == "" || FilterColumn == "NONE")
            {
                _dtAllUsers.DefaultView.RowFilter = "";
                lblRecordsCount.Text = dgvUsers.Rows.Count.ToString();
                return;
            }

            if(FilterColumn != "FullName" && FilterColumn != "UserName")
            {
                _dtAllUsers.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterValue.Text.Trim());
            }
            else
            {
                _dtAllUsers.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtFilterValue.Text.Trim());
            }
            lblRecordsCount.Text = _dtAllUsers.Rows.Count.ToString();
        }

        private void cbIsActive_SelectedIndexChanged(object sender, EventArgs e)
        {
            string FilterColumn = "IsActive";
            string FilterValue = cbIsActive.Text;

            switch (FilterValue)
            {
                case "All":
                    break;
                case "Yes":
                    FilterValue = "1";
                    break;
                case "No":
                    FilterValue = "0";
                    break;
            }

            if(FilterValue == "All")
            {
                _dtAllUsers.DefaultView.RowFilter = "";
            }
            else
            {
                _dtAllUsers.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, FilterValue);
            }
            lblRecordsCount.Text = dgvUsers.Rows.Count.ToString();
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            frmUserInfo Form = new frmUserInfo((int)dgvUsers.CurrentRow.Cells[0].Value);
            Form.ShowDialog(); 
        }

        private void addNewUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdateUser frm = new frmAddUpdateUser();
            frm.ShowDialog();
            frmListUsers_Load(null, null);
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdateUser frm = new frmAddUpdateUser((int)dgvUsers.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            frmListUsers_Load(null, null);
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmChangePassword frm = new frmChangePassword((int)dgvUsers.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int UserID = (int)dgvUsers.CurrentRow.Cells[0].Value;
            if (clsUser.DeleteUser(UserID))
            {
                MessageBox.Show("User has been deleted successfully", "DELETED");
                frmListUsers_Load(null, null);
            }
            else
            {
                MessageBox.Show("User is not deleted ", "ERROR");
            }
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.Text == "Person ID" || cbFilterBy.Text == "User ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}