using DVLD_BusinessLayer;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace DVLD.Applications.Application_Types
{
    public partial class frmListApplicationTypes : Form
    {
        private DataTable _dtAllApplicationTypes;

        public frmListApplicationTypes()
        {
            InitializeComponent();
            ApplyStyling();
            ApplyContextMenuStyle();
        }

        private void frmListApplicationTypes_Load(object sender, EventArgs e)
        {
            _dtAllApplicationTypes = clsApplicationType.GetAllApplicationTypes();

            dgvApplicationTypes.DataSource = _dtAllApplicationTypes;

            lblRecordsCount.Text = dgvApplicationTypes.Rows.Count.ToString();

            if (dgvApplicationTypes.Rows.Count > 0)
            {
                dgvApplicationTypes.Columns[0].HeaderText = "ID";
                dgvApplicationTypes.Columns[0].Width = 120;

                dgvApplicationTypes.Columns[1].HeaderText = "Title";
                dgvApplicationTypes.Columns[1].Width = 420;

                dgvApplicationTypes.Columns[2].HeaderText = "Fees";
                dgvApplicationTypes.Columns[2].Width = 150;
            }
        }

        private void ApplyStyling()
        {
            // DataGridView styling
            dgvApplicationTypes.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(55, 55, 60);
            dgvApplicationTypes.AlternatingRowsDefaultCellStyle.ForeColor = Color.FromArgb(200, 200, 205);
            dgvApplicationTypes.AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(52, 152, 219);
            dgvApplicationTypes.AlternatingRowsDefaultCellStyle.SelectionForeColor = Color.White;
            dgvApplicationTypes.RowTemplate.Height = 32;
            dgvApplicationTypes.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgvApplicationTypes.ColumnHeadersDefaultCellStyle.Padding = new Padding(8, 0, 0, 0);
            dgvApplicationTypes.ColumnHeadersHeight = 40;

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

            editApplicationTypeToolStripMenuItem.Text = "✏️  Edit Application Type";
            editApplicationTypeToolStripMenuItem.ForeColor = Color.FromArgb(52, 152, 219);
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

        private void editApplicationTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmEditApplicationTypes frm = new frmEditApplicationTypes((int)dgvApplicationTypes.CurrentRow.Cells[0].Value); 
            frm.ShowDialog();
            frmListApplicationTypes_Load(null, null);
        }
    }
}