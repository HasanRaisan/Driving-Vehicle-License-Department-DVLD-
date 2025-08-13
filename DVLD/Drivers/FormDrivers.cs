using BusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD
{
    public partial class FormDrivers : Form
    {
        public FormDrivers()
        {
            InitializeComponent();
        }

        void LoadDataToDataGridView()
        {
            dgvDrivers.DataSource = clsDriver.GetAllDrivers();

            int DriversCount = (int)dgvDrivers.Rows.Count;
            lblRecords.Text = DriversCount > 2 ? $"# Rocords: {DriversCount}" : $"# Rocord: {DriversCount}";

        }

        void LoadComboBoxItems()
        {
            this.cbFilterBy.Items.AddRange(new string[] { "None", "Driver ID", "Person ID", "National NO.", "Full Name" });
            this.cbFilterBy.SelectedIndex = 0;
        }

        private void FilterDataGridView()
        {
            DataView dataview = clsDriver.GetAllDrivers().AsDataView();



            if (cbFilterBy.SelectedIndex <= 0 || string.IsNullOrEmpty(txbFilter.Text))
            {
                txbFilter.Text = "";
                dgvDrivers.DataSource = clsDriver.GetAllDrivers();
                return;
            }
      
            string ColumnName = cbFilterBy.SelectedItem.ToString();
            string FilterValue = txbFilter.Text.ToString();
      
            if (dataview.Table.Columns[ColumnName].DataType == typeof(string))
            {
                dataview.RowFilter = $"[{ColumnName}]  LIKE '%{FilterValue}%'";
            }
            else if (dataview.Table.Columns[ColumnName].DataType == typeof(int))
            {
                if (int.TryParse(FilterValue, out int FilterResult))
                    dataview.RowFilter = $"[{ColumnName}] = {FilterResult}";
                else
                {
                    txbFilter.Text = string.Empty;
                }
            }
      
            if (dataview.Count > 1)
                lblRecords.Text = "Records: " + dataview.Count.ToString();
            else
                lblRecords.Text = "Record: " + dataview.Count.ToString();
      
            dgvDrivers.DataSource = dataview;
        }


        private void FormDrivers_Load(object sender, EventArgs e)
        {
            txbFilter.Visible = false;
            LoadComboBoxItems();
            LoadDataToDataGridView();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txbFilter.Visible = cbFilterBy.SelectedIndex == 0 ? false : true;
            txbFilter.Focus();
            FilterDataGridView();
        }

        private void txbFilter_TextChanged(object sender, EventArgs e)
        {
            FilterDataGridView();
        }


        // handle context menu strip 

        private void showPersonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var ShowPersonDetaisl = new FormShowPersonDetails((int)dgvDrivers.CurrentRow.Cells[1].Value);
            ShowPersonDetaisl.ShowDialog();
        }

        private void showLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var ShowLicenseDetails = new FormShowPersonLicensesHistory((int)dgvDrivers.CurrentRow.Cells[1].Value);
            ShowLicenseDetails.ShowDialog();
        }

        private void txbFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(cbFilterBy.Text == "Full Name" && !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }

}
