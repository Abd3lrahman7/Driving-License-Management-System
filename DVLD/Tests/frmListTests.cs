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

namespace DVLD.Tests
{
    public partial class frmListTests : Form
    {
        private static DataTable dt = clsTest.GetAllTests_View();
        private DataTable _dtAllTests = dt.DefaultView.ToTable(false, "TestID", "TestTypeTitle", "LDLAppID", "FullName",
                                               "TestResult", "AppointmentDate", "PaidFees", "CreatedBy");
        public frmListTests()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmListTests_Load(object sender, EventArgs e)
        {
            this.Text = "List Tests";

            dgvTests.DataSource = _dtAllTests;
            cbFilterBy.SelectedIndex = 0;
            lblRecordsCount.Text = dgvTests.Rows.Count.ToString();

            if(dgvTests.Rows.Count > 0 )
            {
                dgvTests.Columns[0].HeaderText = "Test ID";
                dgvTests.Columns[0].Width = 90;

                dgvTests.Columns[1].HeaderText = "Test Title";
                dgvTests.Columns[1].Width = 170;

                dgvTests.Columns[2].HeaderText = "LDLAppID";
                dgvTests.Columns[2].Width = 110;

                dgvTests.Columns[3].HeaderText = "Full Name";
                dgvTests.Columns[3].Width = 320;

                dgvTests.Columns[4].HeaderText = "Test Result";
                dgvTests.Columns[4].Width = 110;

                dgvTests.Columns[5].HeaderText = "Appointment Date";
                dgvTests.Columns[5].Width = 170;

                dgvTests.Columns[6].HeaderText = "Paid Fees";
                dgvTests.Columns[6].Width = 90;

                dgvTests.Columns[7].HeaderText = "Created By";
                dgvTests.Columns[7].Width = 120;
            }
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Visible = (cbFilterBy.Text != "None");
            txtFilterValue.Text = "";
            txtFilterValue.Focus();

            if(cbFilterBy.Text == "None" || txtFilterValue.Text.Trim() == "")
            {
                _dtAllTests.DefaultView.RowFilter = "";
                lblRecordsCount.Text = dgvTests.Rows.Count.ToString();
                return;
            }
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.Text == "Test ID" || cbFilterBy.Text == "LDLAppID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);

        }

        private void dgvTests_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (e.RowIndex >= 0)
                {
                    dgvTests.ClearSelection();
                    dgvTests.CurrentCell = dgvTests.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    dgvTests.Rows[e.RowIndex].Selected = true;
                }
            }

        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";

            switch(cbFilterBy.Text)
            {
                case "Test ID":
                    FilterColumn = "TestID";
                    break;
                case "LDLApp ID":
                    FilterColumn = "LDLAppID";
                    break;
                case "Full Name":
                    FilterColumn = "FullName";
                    break;
                default:
                    FilterColumn = "None";
                    break;
            }

            if (FilterColumn == "None" || txtFilterValue.Text.Trim() == "")
            {
                _dtAllTests.DefaultView.RowFilter = "";
                lblRecordsCount.Text = dgvTests.Rows.Count.ToString();
                return;
            }

            if(FilterColumn == "TestID" || FilterColumn == "LDLAppID")
            {
                _dtAllTests.DefaultView.RowFilter = string.Format("{0} = {1}", FilterColumn, txtFilterValue.Text.Trim());
            }
            else
            {
                _dtAllTests.DefaultView.RowFilter = $"{FilterColumn} LIKE '{txtFilterValue.Text.Trim()}%'";
            }
            lblRecordsCount.Text = dgvTests.Rows.Count.ToString() ;
        }
    }
}
