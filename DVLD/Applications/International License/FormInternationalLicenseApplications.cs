using BusinessLayer;
using DVLD.Mange_Applications;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.International_Applications
{
    public partial class FormInternationalLicenseApplications : Form
    {
        string Username = string.Empty;
        public FormInternationalLicenseApplications(string Username)
        {
            this.Username = Username;
            InitializeComponent();
        }


        private DataTable GetDataTable()
        {
          return  clsInternationalLicenses.GetInternationalApplications();
        }

        private void LoadComboBoxData()
        {
            this.cbFliterByActvation.Items.AddRange(new string[] { "All", "Yes", "No"});
            this.cbFliterByActvation.SelectedIndex = 0;


            cbFilterBy.Items.Add("None");

            DataTable dt = GetDataTable();
            dt.Columns.Remove("Issue Date");
            dt.Columns.Remove("Expiration Date");

            foreach (DataColumn dc in dt.Columns)
            {
                cbFilterBy.Items.Add(dc.ColumnName);
            }
            this.cbFilterBy.SelectedIndex = 0;


        }

        private void LoadDataToDataGridView()
        {
            dgvInternationalApplications.DataSource = this.GetDataTable();

            if (dgvInternationalApplications.Rows.Count > 2)
            {
                lblRecords.Text = "Records: " + dgvInternationalApplications.Rows.Count.ToString();
            }
            else
            {
                lblRecords.Text = "Record: " + dgvInternationalApplications.Rows.Count.ToString();
            }
        }

        private void LoadData()
        {
            LoadComboBoxData();
            LoadDataToDataGridView();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilterBy.SelectedIndex == 0)
            {
                this.cbFliterByActvation.Visible = false; this.txbFilter.Visible = false;
            }
            else if (cbFilterBy.SelectedIndex == 5)
            {
                this.cbFliterByActvation.Visible = true; this.txbFilter.Visible = false;
            }
            else
            {
                this.txbFilter.Visible = true; this.cbFliterByActvation.Visible = false;


            }

            if (cbFilterBy.SelectedIndex == 0) cbFliterByActvation.SelectedIndex = 0;
            FilterDataGridView();


        }
        private void cbFliterByStatus_SelectedValueChanged(object sender, EventArgs e)
        {
            FilterDataGridView();

        }
        private void txbFilter_TextChanged(object sender, EventArgs e)
        {
            FilterDataGridView();
        }

        private void FilterDataGridView()
        {

            string FilterValue = txbFilter.Text;
            string ColumnName = cbFilterBy.Text;

            DataView dvUsers = GetDataTable().AsDataView();

            if ((cbFilterBy.SelectedIndex > 0 && !string.IsNullOrEmpty(FilterValue)) || cbFliterByActvation.SelectedIndex > 0)
            {

                if (cbFilterBy.SelectedIndex == 5)
                {

                    if (cbFliterByActvation.SelectedIndex == 1)
                    {
                        dvUsers.RowFilter = $"[{ColumnName}] = 'Yes'";
                    }
                    else if (cbFliterByActvation.SelectedIndex == 2)
                    {
                        dvUsers.RowFilter = $"[{ColumnName}] = 'No'";
                    }                  
                    else
                    {
                        LoadDataToDataGridView();
                    }

                }
                else
                {
                    if (int.TryParse(FilterValue, out int value))
                    {
                        dvUsers.RowFilter = $"[{ColumnName}] = {value}";
                    }
                    else
                    {
                        txbFilter.Text = string.Empty;
                    }
                }

                dgvInternationalApplications.DataSource = dvUsers;

                if (dgvInternationalApplications.Rows.Count > 2)
                {
                    lblRecords.Text = "Records: " + dgvInternationalApplications.Rows.Count.ToString();
                }
                else
                {
                    lblRecords.Text = "Record: " + dgvInternationalApplications.Rows.Count.ToString();
                }
            }
            else
            {
                LoadDataToDataGridView();
            }
        }
        private void TextBoxKeyPressOnlyNumbers(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void FormInternationalLicenseApplications_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void AddPerson_Click(object sender, EventArgs e)
        {
            var formAdd = new FormAddNewInternationlalLicense(this.Username);
            formAdd.ShowDialog();
        }



        /// CONTEXT MENU STRIP ///

        private void showLicenseDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormShowLicense showLicense = new FormShowLicense((int)dgvInternationalApplications.CurrentRow.Cells["L.License ID"].Value);
            showLicense.ShowDialog();
        }

        private void showPersonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormShowPersonDetails formPerson = new FormShowPersonDetails(clsLicenses.GetPersonIDByLicenseID((int)dgvInternationalApplications.CurrentRow.Cells["L.License ID"].Value));
            formPerson.ShowDialog();

        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormShowPersonLicensesHistory formLicenses = new FormShowPersonLicensesHistory(clsLicenses.GetPersonIDByLicenseID((int)dgvInternationalApplications.CurrentRow.Cells["L.License ID"].Value));
            formLicenses.ShowDialog();
        }
    }

}