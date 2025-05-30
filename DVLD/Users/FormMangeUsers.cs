using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using BusinessLayer;

namespace DVLD
{
    public partial class FormMangeUsers : Form
    {
        public FormMangeUsers()
        {
            InitializeComponent();
        }

        private DataTable GetDataTable()
        {
            return clsUsers.GetUsers();
        }

        private void LoadDataToDataGridView()
        {
            dgvUsers.DataSource = GetDataTable();

            if (dgvUsers.Rows.Count > 2)
            {
                lblRecords.Text = "Records: " + dgvUsers.Rows.Count.ToString();
            }
            else
            {
                lblRecords.Text = "Record: " + dgvUsers.Rows.Count.ToString();
            }
        }
        private void LoadDataToComboBox()
        {



            // Activation combo box
            this.cbFliterByActivation.Items.AddRange(new string[] { "All", "Active", "Not Active" });
            this.cbFliterByActivation.SelectedIndex = 0;



            // main combo box
            cbFilterBy.Items.Add("Noun");

            DataTable dt = GetDataTable();

            foreach (DataColumn dc in dt.Columns)
            {
                cbFilterBy.Items.Add(dc.ColumnName);
            }
            this.cbFilterBy.SelectedIndex = 0;

        }
        private void FormMangeUsers_Load(object sender, EventArgs e)
        {
            LoadDataToComboBox();
            LoadDataToDataGridView();
            this.txbFilter.Visible = false;
            this.cbFliterByActivation.Visible = false;



        }
        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilterBy.SelectedIndex == 0)
            {
                this.cbFliterByActivation.Visible = false; this.txbFilter.Visible = false;
            }
            else if (cbFilterBy.SelectedIndex == 4)
            {
                this.cbFliterByActivation.Visible = true; this.txbFilter.Visible = false;
            }
            else
            {
                this.txbFilter.Visible = true; this.cbFliterByActivation.Visible = false;

                if (cbFilterBy.SelectedIndex == 1 || cbFilterBy.SelectedIndex == 2)
                {

                    //MessageBox.Show("Numbers only");
                    txbFilter.KeyPress -= TextBoxKeyPressOnlyLettersOrNumbers;
                    txbFilter.KeyPress += TextBoxKeyPressOnlyNumbers;
                }
                else
                {
                  //  MessageBox.Show("Letters and numbers");
                    txbFilter.KeyPress -= TextBoxKeyPressOnlyNumbers;
                    txbFilter.KeyPress += TextBoxKeyPressOnlyLettersOrNumbers;
                }


            }

            if (cbFilterBy.SelectedIndex == 0) cbFliterByActivation.SelectedIndex = 0;

            FilterDataGridView();


        }
        private void TextBoxKeyPressOnlyNumbers(object sender, KeyPressEventArgs e)
        {
            if(!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void TextBoxKeyPressOnlyLettersOrNumbers(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetterOrDigit(e.KeyChar))
            {
                e.Handled= true;
            }
        }
        private void txbFilter_TextChanged(object sender, EventArgs e)
        {
            FilterDataGridView();
        }
        private void cbFliterByActivation_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterDataGridView();



        }
        private void FilterDataGridView()
        {
            string FilterValue = txbFilter.Text;
            string ColumnName = cbFilterBy.Text;

            DataView dvUsers = GetDataTable().AsDataView();


            // 1- user id  2- person id  3-username  4- is active
            if ((cbFilterBy.SelectedIndex > 0 && !string.IsNullOrEmpty(FilterValue)) || cbFliterByActivation.SelectedIndex > 0)
            {
                if (cbFilterBy.SelectedIndex == 3)
                {
                    dvUsers.RowFilter = $"[{ColumnName}] LIKE '%{FilterValue}%'";
                }
                else if (cbFilterBy.SelectedIndex == 4)
                {

                    if (cbFliterByActivation.SelectedIndex == 1)
                    {
                        dvUsers.RowFilter = "[Is Active] = 'Yes'";
                    }
                    else if (cbFliterByActivation.SelectedIndex == 2)
                    {
                        dvUsers.RowFilter = "[Is Active] = 'No'";
                    }
                    else
                    {
                        dvUsers = GetDataTable().AsDataView();
                    }
                }

                else if (cbFilterBy.SelectedIndex == 2 || cbFilterBy.SelectedIndex == 1)
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

                dgvUsers.DataSource = dvUsers;

                if (dgvUsers.Rows.Count > 2)
                {
                    lblRecords.Text = "Records: " + dgvUsers.Rows.Count.ToString();
                }
                else
                {
                    lblRecords.Text = "Record: " + dgvUsers.Rows.Count.ToString();
                }
            }
            else
            {
                cbFliterByActivation.SelectedIndex = 0;
                LoadDataToDataGridView();
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void AddPerson_Click(object sender, EventArgs e)
        {
            FormAddEditUser formAddUser = new FormAddEditUser(-1);
            formAddUser.ShowDialog();
            LoadDataToDataGridView();
        }       
        private void showUserDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormShowUserDetails userDetails = new FormShowUserDetails((int)dgvUsers.CurrentRow.Cells[0].Value);
            userDetails.ShowDialog();
            LoadDataToDataGridView();
        }
        private void addNewUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAddEditUser formAddUser = new FormAddEditUser(-1);
            formAddUser.ShowDialog();
            LoadDataToDataGridView();
        }
        private void deleteUserToolStripMenuItem_Click(object sender, EventArgs e)
        {


            if (MessageBox.Show("Are you sure you want to delete user [" + dgvUsers.CurrentRow.Cells[0].Value + "]", "Confirm Delete", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                if (clsUsers.DeleteUser((int)dgvUsers.CurrentRow.Cells[0].Value))
                {
                    MessageBox.Show("User deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDataToDataGridView();
                }

                else
                    MessageBox.Show("This user has not deleted.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
        private void editUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAddEditUser formEditUser = new FormAddEditUser((int)dgvUsers.CurrentRow.Cells[0].Value);
            formEditUser.ShowDialog();
            LoadDataToDataGridView();
        }
        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormChangePassword formChangePassword = new FormChangePassword((int)dgvUsers.CurrentRow.Cells[0].Value);
            formChangePassword.ShowDialog();
            LoadDataToDataGridView();
        }
    }
}
