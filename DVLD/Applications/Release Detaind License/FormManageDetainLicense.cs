using BusinessLayer;
using DVLD.Mange_Applications;
using System;
using System.Data;
using System.Deployment.Application;
using System.Net.Http;
using System.Windows.Forms;

namespace DVLD.Serveces
{
    public partial class FormManageDetainLicense : Form
    {
        public FormManageDetainLicense()
        {
            InitializeComponent();
        }

        DataTable GetDataTabel()
        {
            return clsDetainLicense.GetAllDetainedLicense();
        }

        void LoadDataToDataGridView()
        {
            dgvDetains.DataSource = GetDataTabel();

            int DriversCount = (int)dgvDetains.Rows.Count;
            lblRecords.Text = DriversCount > 2 ? $"# Rocords: {DriversCount}" : $"# Rocord: {DriversCount}";
        }

        void LoadComboBoxItems()
        {
            DataTable dtDetains = GetDataTabel();

            dtDetains.Columns.Remove("D.Date");
            dtDetains.Columns.Remove("Fine Fees");
            dtDetains.Columns.Remove("Release Date");

            cbFilterBy.Items.Add("None");
            foreach(DataColumn dc in dtDetains.Columns)
                cbFilterBy.Items.Add(dc.ColumnName);

            this.cbFilterBy.SelectedIndex = 0;
        }

        private void FormManageDetainLicense_Load(object sender, System.EventArgs e)
        {
            txbFilter.Visible = false;
            LoadComboBoxItems();
            LoadDataToDataGridView();
        }

        private void FilterDataGridView()
        {
            DataView dataview = GetDataTabel().AsDataView();

            if (cbFilterBy.SelectedIndex > 0 && !string.IsNullOrEmpty(txbFilter.Text))
            {
                string ColumnName = cbFilterBy.SelectedItem.ToString();
                string FilterValue = txbFilter.Text.ToString();

                if (dataview.Table.Columns[ColumnName].DataType == typeof(string))
                {
                    dataview.RowFilter = $"[{ColumnName}]  LIKE '%{FilterValue}%'";
                }
                else if (dataview.Table.Columns[ColumnName].DataType == typeof(int))
                {
                    if (int.TryParse(FilterValue, out int result))
                    {
                        dataview.RowFilter = $"[{ColumnName}] = {result}";
                    }
                    else
                    {
                        //MessageBox.Show("The value must be numries");
                        txbFilter.Text = string.Empty;
                    }
                }

                if (dataview.Count > 1)
                {
                    lblRecords.Text = "Records: " + dataview.Count.ToString();
                }
                else
                {
                    lblRecords.Text = "Record: " + dataview.Count.ToString();
                }

                dgvDetains.DataSource = dataview;
            }
            else
            {
                LoadDataToDataGridView();
            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txbFilter.Visible = cbFilterBy.SelectedIndex == 0 ? false : true;
            FilterDataGridView();
        }

        private void txbFilter_TextChanged(object sender, EventArgs e)
        {
            FilterDataGridView();
        }

        private void btnDetainLicense_Click(object sender, EventArgs e)
        {
            var ShowDetainLicense = new FormDetainLicense();
            ShowDetainLicense.ShowDialog();
            LoadDataToDataGridView();

        }

        private void btnRleaseLicense_Click(object sender, EventArgs e)
        {
            var ShowReleaseLicense = new FormReleaseLicense();
            ShowReleaseLicense.ShowDialog();
            LoadDataToDataGridView();
        }


        //////////////////////////////////////
        //////// CONTEXT MENU ITEMS //////////
        //////////////////////////////////////
        
        private void showPersonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = clsLicense.GetPersonIDByLicenseID((int)dgvDetains.CurrentRow.Cells["L.ID"].Value);
            var ShowPersonDetails = new FormShowPersonDetails(PersonID);
            ShowPersonDetails.ShowDialog();
            LoadDataToDataGridView();
        }

        private void showLicenseDetaiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var ShowLicenseDetails = new FormShowLicense((int)dgvDetains.CurrentRow.Cells["L.ID"].Value);
            ShowLicenseDetails.ShowDialog();
        }

        private void lToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = clsLicense.GetPersonIDByLicenseID((int)dgvDetains.CurrentRow.Cells["L.ID"].Value);
            var ShowLlicenseHistory = new FormShowPersonLicensesHistory(PersonID);
            ShowLlicenseHistory.ShowDialog();
        }

        private void dgvDetains_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (!(e.RowIndex >= 0 && e.Button == MouseButtons.Right))
                return;

            if(clsDetainLicense.IsDetainReleaseByDetainID((int)dgvDetains.CurrentRow.Cells["D.ID"].Value))
                this.releasDetainedLicenseToolStripMenuItem.Enabled = true;
            else
                this.releasDetainedLicenseToolStripMenuItem.Enabled = false;
        }

        private void releasDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var releaseLicense = new FormReleaseLicense();

            releaseLicense.SetLicensIDForDirectRelease((int)dgvDetains.CurrentRow.Cells["L.ID"].Value);

            releaseLicense.ShowDialog();
            LoadDataToDataGridView();
        }
    }


}
