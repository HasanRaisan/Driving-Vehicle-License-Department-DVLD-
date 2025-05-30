using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessLayer;

namespace DVLD
{
    public partial class MangePeople : Form
    {

        public MangePeople()
        {
            InitializeComponent();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;

        }

        private void MangePeople_Load(object sender, EventArgs e)
        {
            LoadFormData();
        }

        private void LoadFormData()
        {
            LoadDataToDataGridView();
            LoadComboBox();
            txbFilter.Visible = false;

        }

        private DataTable LoadDataTable()
        {
          return clsPerson.GetPeople();
        }

        private void LoadDataToDataGridView()
        {
            dgvPeople.DataSource = LoadDataTable();
     


            if (dgvPeople.Rows.Count > 2)
            {
                lblRecords.Text = "Records: " + dgvPeople.Rows.Count.ToString();
            }
            else
            {
                lblRecords.Text = "Record: " + dgvPeople.Rows.Count.ToString();
            }
        }

        private void LoadComboBox()
        {

            cbFilterBy.Items.Add("Noun");


            DataTable dt = LoadDataTable();

            foreach (DataColumn dc in dt.Columns)
            {
                cbFilterBy.Items.Add(dc.ColumnName);     
            }

            cbFilterBy.SelectedIndex = 0;

        }

        private void FilterDataGridView()
        { 
            DataView dataview = LoadDataTable().AsDataView();

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

                dgvPeople.DataSource = dataview;
            }
            else
            {
                LoadDataToDataGridView();
            }

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

        private void btnClose_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AddPerson_Click(object sender, EventArgs e)
        {
            AddEditPerson addPerson = new AddEditPerson(-1);
            addPerson.ShowDialog();
            LoadDataToDataGridView();
        }

        // context menue strip

        private void addNewPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            AddEditPerson addPerson = new AddEditPerson(-1);
            addPerson.ShowDialog();
            LoadDataToDataGridView();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvPeople.Rows.Count > 0)
            {
                AddEditPerson EditPerson = new AddEditPerson((int)dgvPeople.CurrentRow.Cells[0].Value);
                EditPerson.ShowDialog();
                LoadDataToDataGridView();
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {


            if (MessageBox.Show("Are you sure you want to delete person [" + dgvPeople.CurrentRow.Cells[0].Value + "]", "Confirm Delete", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                if (clsPerson.DeletePerson((int)dgvPeople.CurrentRow.Cells[0].Value))
                {
                    MessageBox.Show("Person deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDataToDataGridView();
                }

                else
                    MessageBox.Show("This person cannot be deleted because they are associated with active records.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvPeople.Rows.Count > 0)
            {
                FormShowPersonDetails ShowDetails = new FormShowPersonDetails((int)dgvPeople.CurrentRow.Cells[0].Value);
                ShowDetails.ShowDialog();
                LoadDataToDataGridView();

            }

        }

        private void showPersonLicensesHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // enbaled and dis for Show Person Licenses...
            var ShowPersonLicenses = new FormShowPersonLicensesHistory((int)dgvPeople.CurrentRow.Cells[0].Value);
            ShowPersonLicenses.ShowDialog();
            LoadDataToDataGridView();

        }

        private void dgvPeople_MouseDown(object sender, MouseEventArgs e)
        {

        }
    }
}
