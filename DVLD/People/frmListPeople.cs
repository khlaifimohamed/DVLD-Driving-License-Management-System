using DVLD_BusinessLayer;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace DVLD.People
{
    public partial class frmListPeople : Form
    {

        private static DataTable _dtAllPeople = clsPerson.GetAllPeople();

        private DataTable _dtPeople = _dtAllPeople.DefaultView.ToTable(false,"PersonID","NationalNo",
            "FirstName","SecondName","ThirdName","LastName","GenderCaption","DateOfBirth","CountryName","Phone","Email");

        private void _RefreshPeopleList()
        {
            _dtAllPeople = clsPerson.GetAllPeople();
            _dtPeople = _dtAllPeople.DefaultView.ToTable(false, "PersonID", "NationalNo",
            "FirstName", "SecondName", "ThirdName", "LastName", "GenderCaption", "DateOfBirth", "CountryName", "Phone", "Email");
            dgvPeople.DataSource = _dtPeople;
            lblRecordsCount.Text = dgvPeople.Rows.Count.ToString();

        }
        public frmListPeople()
        {
            InitializeComponent();
            ApplyStyling();
        }

        private void ApplyStyling()
        {
            contextMenuStrip1.Renderer = new DarkContextMenuRenderer();
            // Style the ComboBox dropdown to match dark theme
            cbFilterBy.DrawMode = DrawMode.OwnerDrawFixed;
            cbFilterBy.DrawItem += ComboBox1_DrawItem;

            // Style DataGridView alternating rows
            dgvPeople.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(55, 55, 60);
            dgvPeople.AlternatingRowsDefaultCellStyle.ForeColor = Color.FromArgb(200, 200, 205);
            dgvPeople.AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(52, 152, 219);
            dgvPeople.AlternatingRowsDefaultCellStyle.SelectionForeColor = Color.White;

            // Set row height
            dgvPeople.RowTemplate.Height = 32;

            // Style the header
            dgvPeople.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgvPeople.ColumnHeadersDefaultCellStyle.Padding = new Padding(8, 0, 0, 0);
            dgvPeople.ColumnHeadersHeight = 40;

            // Add CellMouseEnter/Leave events for hover effect
            dgvPeople.CellMouseEnter += DataGridView1_CellMouseEnter;
            dgvPeople.CellMouseLeave += DataGridView1_CellMouseLeave;

            // Style buttons with rounded corners
            ApplyButtonHoverEffects();
        }

        private void ApplyButtonHoverEffects()
        {
            // Add button
            btnAddPerson.MouseEnter += (s, e) => {
                btnAddPerson.BackColor = Color.FromArgb(39, 174, 96);
                btnAddPerson.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            };
            btnAddPerson.MouseLeave += (s, e) => {
                btnAddPerson.BackColor = Color.FromArgb(46, 204, 113);
                btnAddPerson.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            };

            // Close button
            button2.MouseEnter += (s, e) => {
                button2.BackColor = Color.FromArgb(231, 76, 60);
                button2.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            };
            button2.MouseLeave += (s, e) => {
                button2.BackColor = Color.FromArgb(85, 85, 90);
                button2.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            };
        }

        private void ComboBox1_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;

            // Draw background
            e.DrawBackground();

            bool isSelected = (e.State & DrawItemState.Selected) == DrawItemState.Selected;

            // Set colors based on selection
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

                // Draw text with padding
                Rectangle textRect = new Rectangle(
                    e.Bounds.X + 5,
                    e.Bounds.Y + 2,
                    e.Bounds.Width - 10,
                    e.Bounds.Height - 4);

                e.Graphics.DrawString(
                    cbFilterBy.Items[e.Index].ToString(),
                    new Font("Segoe UI", 9.5F),
                    textBrush,
                    textRect);
            }
        }

        private void DataGridView1_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                dgvPeople.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(65, 65, 70);
            }
        }

        private void DataGridView1_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                // Reset to alternating row colors
                Color rowColor = e.RowIndex % 2 == 0
                    ? Color.FromArgb(50, 50, 54)
                    : Color.FromArgb(55, 55, 60);

                dgvPeople.Rows[e.RowIndex].DefaultCellStyle.BackColor = rowColor;
            }
        }

        private void btnAddPerson_Click(object sender, EventArgs e)
        {
            frmAddUpdatePerson form = new frmAddUpdatePerson();
            form.ShowDialog();
            _RefreshPeopleList();
        }

        private void frmListPeople_Load(object sender, EventArgs e)
        {
            dgvPeople.DataSource = _dtPeople;
            cbFilterBy.SelectedIndex = 0;
            lblRecordsCount.Text = dgvPeople.Rows.Count.ToString();
            txtFilterValue.Visible = false;

            if (dgvPeople.Rows.Count > 0)
            {
                dgvPeople.Columns[0].HeaderText = "Person ID";
                dgvPeople.Columns[0].Width = 110;

                dgvPeople.Columns[1].HeaderText = "National No";
                dgvPeople.Columns[1].Width = 110;

                dgvPeople.Columns[2].HeaderText = "First Name";
                dgvPeople.Columns[2].Width = 110;

                dgvPeople.Columns[3].HeaderText = "Second Name";
                dgvPeople.Columns[3].Width = 130;

                dgvPeople.Columns[4].HeaderText = "Third Name";
                dgvPeople.Columns[4].Width = 110;

                dgvPeople.Columns[5].HeaderText = "Last Name";
                dgvPeople.Columns[5].Width = 110;

                dgvPeople.Columns[6].HeaderText = "Gender";
                dgvPeople.Columns[6].Width = 110;

                dgvPeople.Columns[7].HeaderText = "Date Of Birth";
                dgvPeople.Columns[7].Width = 130;

                dgvPeople.Columns[8].HeaderText = "Nationality";
                dgvPeople.Columns[8].Width = 110;

                dgvPeople.Columns[9].HeaderText = "Phone";
                dgvPeople.Columns[9].Width = 110;

                dgvPeople.Columns[10].HeaderText = "Email";
                dgvPeople.Columns[10].Width = 160;
            }

             
    }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";

            switch (cbFilterBy.Text)
            {
                case "Person ID":
                    FilterColumn = "PersonID";
                    break;
                case "National No":
                    FilterColumn = "NationalNo";
                    break;
                case "First Name":
                    FilterColumn = "FirstName";
                    break;
                case "Second Name":
                    FilterColumn = "SecondName";
                    break;
                case "Third Name":
                    FilterColumn = "ThirdName";
                    break;
                case "Last Name":
                    FilterColumn = "LastName";
                    break;
                case "Nationality":
                    FilterColumn = "CountryName";
                    break;
                case "Gender":
                    FilterColumn = "Gender";
                    break;
                case "Phone":
                    FilterColumn = "Phone";
                    break;
                case "Email":
                    FilterColumn = "Email";
                    break;
                default:
                    FilterColumn = "None";
                    break;
            }

            if(txtFilterValue.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtPeople.DefaultView.RowFilter = "";
                lblRecordsCount.Text = dgvPeople.Rows.Count.ToString();
                return;
            }


            if(FilterColumn == "PersonID")
            {
                _dtPeople.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterValue.Text.Trim());
            }
            else
            {
                _dtPeople.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtFilterValue.Text.Trim());
            }
            lblRecordsCount.Text = dgvPeople.Rows.Count.ToString();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Visible = (cbFilterBy.Text != "NONE");

            if (txtFilterValue.Visible)
            {
                txtFilterValue.Text = "";
                txtFilterValue.Focus();
            }
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = (int)dgvPeople.CurrentRow.Cells[0].Value;
            Form frm = new frmShowPersonInfo(PersonID);
            frm.ShowDialog();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonId = (int)dgvPeople.CurrentRow.Cells[0].Value;
            Form frm = new frmAddUpdatePerson(PersonId);
            frm.ShowDialog();
            _RefreshPeopleList();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonId = (int)dgvPeople.CurrentRow.Cells[0].Value;
            if(MessageBox.Show("Are You sure you want to delete person ["+ PersonId + "]","Confirm Delete",MessageBoxButtons.OKCancel,MessageBoxIcon.Question) == DialogResult.OK)
            {
                if (clsPerson.DeletePerson(PersonId))
                {
                    MessageBox.Show("Person Deleted Successfully.", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _RefreshPeopleList();
                }
                else
                {
                    MessageBox.Show("Person was not deleted because it has data linked to it.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            
        }

        private void addNewPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddUpdatePerson();
            frm.ShowDialog();
            _RefreshPeopleList();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvPeople_DoubleClick(object sender, EventArgs e)
        {
            int PersonID = (int)dgvPeople.CurrentRow.Cells[0].Value;
            Form frm = new frmShowPersonInfo(PersonID);
            frm.ShowDialog();
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(cbFilterBy.Text == "Person ID")
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }
        }
        /// <summary>
        /// Custom dark renderer for context menu hover highlighting
        /// </summary>
        public class DarkContextMenuRenderer : ToolStripProfessionalRenderer
        {
            public DarkContextMenuRenderer() : base(new DarkContextMenuColorTable()) { }

            protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
            {
                if (e.Item.Selected)
                {
                    Rectangle rect = new Rectangle(Point.Empty, e.Item.Size);
                    using (SolidBrush brush = new SolidBrush(Color.FromArgb(60, 60, 65)))
                    {
                        e.Graphics.FillRectangle(brush, rect);
                    }

                    // Blue accent bar on the left edge when hovering
                    using (SolidBrush accentBrush = new SolidBrush(Color.FromArgb(52, 152, 219)))
                    {
                        e.Graphics.FillRectangle(accentBrush, 0, 0, 4, rect.Height);
                    }
                }
                else
                {
                    base.OnRenderMenuItemBackground(e);
                }
            }

            private class DarkContextMenuColorTable : ProfessionalColorTable
            {
                public override Color ToolStripDropDownBackground => Color.FromArgb(45, 45, 48);
                public override Color MenuBorder => Color.FromArgb(68, 68, 72);
                public override Color ImageMarginGradientBegin => Color.FromArgb(40, 40, 44);
                public override Color ImageMarginGradientMiddle => Color.FromArgb(40, 40, 44);
                public override Color ImageMarginGradientEnd => Color.FromArgb(40, 40, 44);
            }
        }
    }
}