using DVLD_BusinessLayer;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
namespace DVLD.People.Controls
{
    public partial class ctrlPersonCardWithFilter : UserControl
    {

        public event Action<int> OnPersonSelected;

        protected virtual void PersonSelected(int PersonID)
        {
            Action<int> handler = OnPersonSelected;
            if (handler != null)
            {
                handler(PersonID);
                
            }
        }

        private bool _ShowAddPerson = true;

        public bool ShowAddPerson
        {
            get
            {
                return _ShowAddPerson;
            }
            set
            {
                _ShowAddPerson = value;
                btnAddNewPerson.Visible = _ShowAddPerson;
            }
        }

        private bool _FilterEnable = true;

        public bool FilterEnable
        {
            get
            {
                return _FilterEnable;
            }
            set
            {
                _FilterEnable = value;
                gbFilters.Enabled = _FilterEnable;
            }
        }

        private int _PersonID = -1;

        public int PersonID
        {
            get { return ctrlPersonCard1.PersonID; }
        }

        public clsPerson SelectedPersonInfo
        {
            get { return ctrlPersonCard1.SelectedPersonInfo; }
        }

        public void LoadPersonInfo(int PersonID)
        {
            cbFindBy.SelectedIndex = 0;
            txtFilterValue.Text = PersonID.ToString();
            FindNow();
        }

        private void FindNow()
        {
            switch (cbFindBy.Text)
            {
                case "PersonID":
                    ctrlPersonCard1.LoadPersonInfo(int.Parse(txtFilterValue.Text));
                    break;
                case "NationalNo":
                    ctrlPersonCard1.LoadPersonInfo(txtFilterValue.Text);
                    break;
                default:
                    break;
            }
            if (OnPersonSelected != null && FilterEnable)
            {
                OnPersonSelected(ctrlPersonCard1.PersonID);
            }
        }

        public ctrlPersonCardWithFilter()
        {
            InitializeComponent();
            ApplyStyling();
        }

        private void ctrlPersonCardWithFilter_Load(object sender, EventArgs e)
        {
            cbFindBy.SelectedIndex = 0;
            txtFilterValue.Focus();
        }

        public void FilterFocus()
        {
            txtFilterValue.Focus();
        }
        private void ApplyStyling()
        {
            // Owner draw for ComboBox dropdown items
            cbFindBy.DrawMode = DrawMode.OwnerDrawFixed;
            cbFindBy.DrawItem += ComboBox1_DrawItem;

            // Hover effects for Search button (button1)
            btnFind.MouseEnter += (s, e) => {
                btnFind.BackColor = Color.FromArgb(41, 128, 185);
                btnFind.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            };
            btnFind.MouseLeave += (s, e) => {
                btnFind.BackColor = Color.FromArgb(52, 152, 219);
                btnFind.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            };

            // Hover effects for Add button (button2)
            btnAddNewPerson.MouseEnter += (s, e) => {
                btnAddNewPerson.BackColor = Color.FromArgb(39, 174, 96);
                btnAddNewPerson.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            };
            btnAddNewPerson.MouseLeave += (s, e) => {
                btnAddNewPerson.BackColor = Color.FromArgb(46, 204, 113);
                btnAddNewPerson.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            };

            // Focus & hover effects for Filter TextBox
            txtFilterValue.Enter += (s, e) => {
                txtFilterValue.BackColor = Color.FromArgb(70, 70, 75);
            };
            txtFilterValue.Leave += (s, e) => {
                txtFilterValue.BackColor = Color.FromArgb(60, 60, 65);
            };
        }

        private void ComboBox1_DrawItem(object sender, DrawItemEventArgs e)
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
                    e.Bounds.Y + 3,
                    e.Bounds.Width - 16,
                    e.Bounds.Height - 6);

                e.Graphics.DrawString(
                    cbFindBy.Items[e.Index].ToString(),
                    new Font("Segoe UI", 10F),
                    textBrush,
                    textRect);
            }
        }

        private void cbFindBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Text = "";
            txtFilterValue.Focus();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fields are not validate , put mouse pver the red icon", "ERROR");
                return;
            }
            FindNow();
        }

        private void txtFilteValue_Validating(object sender, CancelEventArgs e) {
            if (string.IsNullOrEmpty(txtFilterValue.Text.Trim()))
            {
                e.Cancel = true ;
                errorProvider1.SetError(txtFilterValue, "This field is required");
            }
            else
            {
                errorProvider1.SetError(txtFilterValue, null);
            }
            
        }

        private void btnAddNewPerson_Click(object sender, EventArgs e)
        {
            frmAddUpdatePerson frm1 = new frmAddUpdatePerson();
            frm1.DataBack += DataBackEvent;
            frm1.ShowDialog();
        }

        private void DataBackEvent(object sender ,int personID)
        {
            cbFindBy.SelectedIndex = 1;
            txtFilterValue.Text = PersonID.ToString();
            ctrlPersonCard1.LoadPersonInfo(personID);
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)13)
            {
                btnFind.PerformClick();
            }
            if(cbFindBy.Text == "PersonID")
            {
                e.Handled = !char.IsDigit(e.KeyChar)&&!char.IsControl(e.KeyChar);
            }
        }
    }
}