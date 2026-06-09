using DVLD.DriverLicense;
using DVLD.People;
using DVLD_Buisness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Licenses
{
    public partial class frmListLicenses : Form
    {
        private static DataTable dataTable = clsLicense.GetAllLicensesView();

        private static DataTable _dtAllLicenses = dataTable.DefaultView.ToTable(false, "LicenseID", "NationalNo", "FullName", "ClassName",
                                       "IssueDate", "ExpirationDate", "IsActive");

        private void _RefreshLicensesList()
        {
            dataTable = clsLicense.GetAllLicensesView();
            _dtAllLicenses = dataTable.DefaultView.ToTable(false, "LicenseID", "NationalNo", "FullName", "ClassName",
                                       "IssueDate", "ExpirationDate", "IsActive");

            dgvLicenses.DataSource = _dtAllLicenses;
            lblRecordsCount.Text = dgvLicenses.Rows.Count.ToString();
        }

        public frmListLicenses()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmListLicenses_Load(object sender, EventArgs e)
        {
            this.Text = "List Licenses";

            dgvLicenses.DataSource = _dtAllLicenses;
            cbFilterBy.SelectedIndex = 0;
            lblRecordsCount.Text = dgvLicenses.Rows.Count.ToString();

            if (dgvLicenses.Rows.Count > 0)
            {
                dgvLicenses.Columns[0].HeaderText = "License ID";
                dgvLicenses.Columns[0].Width = 100;

                dgvLicenses.Columns[1].HeaderText = "National No.";
                dgvLicenses.Columns[1].Width = 130;

                dgvLicenses.Columns[2].HeaderText = "Full Name";
                dgvLicenses.Columns[2].Width = 320;

                dgvLicenses.Columns[3].HeaderText = "Class Name";
                dgvLicenses.Columns[3].Width = 230;

                dgvLicenses.Columns[4].HeaderText = "Issue Date";
                dgvLicenses.Columns[4].Width = 190;

                dgvLicenses.Columns[5].HeaderText = "Expiration Date";
                dgvLicenses.Columns[5].Width = 190;

                dgvLicenses.Columns[6].HeaderText = "IsActive";
                dgvLicenses.Columns[6].Width = 90;
            }

        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmShowLicenseInfo frm = new frmShowLicenseInfo((int)dgvLicenses.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }

        private void showPersonInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmShowPersonInfo frm = new frmShowPersonInfo((string)dgvLicenses.CurrentRow.Cells[1].Value);
            frm.ShowDialog();
            _RefreshLicensesList();
        }

        private void dgvLicenses_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if(e.RowIndex >=0)
                {
                    dgvLicenses.ClearSelection();
                    dgvLicenses.Rows[e.RowIndex].Selected = true;
                    dgvLicenses.CurrentCell = dgvLicenses.Rows[e.RowIndex].Cells[e.ColumnIndex];
                }
            }
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            //we allow number incase person id is selected.
            if (cbFilterBy.Text == "License ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = ""; 

            switch (cbFilterBy.Text)
            {
                case "License ID":
                    FilterColumn = "LicenseID";
                    break;
                case "National No.":
                    FilterColumn = "NationalNo";
                    break;
                case "Full Name":
                    FilterColumn = "FullName";
                    break;
                case "Class Name":
                    FilterColumn = "ClassName";
                    break;
                default:
                    FilterColumn = "None";
                    break;
            }

            if (FilterColumn == "None" || txtFilterValue.Text.Trim() == "")
            {
                _dtAllLicenses.DefaultView.RowFilter = "";
                lblRecordsCount.Text = dgvLicenses.Rows.Count.ToString();
                return;
            }

            if (FilterColumn == "LicenseID")
            {
                _dtAllLicenses.DefaultView.RowFilter = string.Format("{0} = {1}", FilterColumn, txtFilterValue.Text.Trim());
            }
            else
            {
                _dtAllLicenses.DefaultView.RowFilter = string.Format("{0} LIKE '{1}%'", FilterColumn, txtFilterValue.Text.Trim());
            }
            lblRecordsCount.Text = dgvLicenses.Rows.Count.ToString();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbFilterBy.Text == "Is Active")
            {
                txtFilterValue.Visible = false;
                cbIsActive.Visible = true;
                cbIsActive.SelectedIndex = 0;
                cbIsActive.Focus();
            }
            else
            {
                txtFilterValue.Visible = (cbFilterBy.Text != "None");
                cbIsActive.Visible = false;
                txtFilterValue.Text = "";
                txtFilterValue.Focus();
            }
            if (cbFilterBy.Text == "None" || txtFilterValue.Text.Trim() == "")
            {
                _dtAllLicenses.DefaultView.RowFilter = "";
                lblRecordsCount.Text = dgvLicenses.Rows.Count.ToString();
                return;
            }
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


            if (FilterValue == "All")
                _dtAllLicenses.DefaultView.RowFilter = "";
            else
                //in this case we deal with numbers not string.
                _dtAllLicenses.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, FilterValue);

            lblRecordsCount.Text = _dtAllLicenses.Rows.Count.ToString();

        }
    }
}
