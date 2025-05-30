using BusinessLayer;
using DVLD.Driving_Licenses_Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Mange_Applications
{
    public partial class MangeLoacalDrivingLicenseApplications : Form
    {
        clsUsers clsUser = new clsUsers();

        public MangeLoacalDrivingLicenseApplications(string userUsername)
        {
            InitializeComponent();
            clsUser = clsUsers.FindUser(userUsername);
        }
        private DataTable GetDataTable()
        {
            return clsLocalDrivingLicensesApplication.GeLAllLocalDrivingLicenseApplicationsForMange();

        }
        private void LoadComboBoxData()
        {
            DataTable dt = GetDataTable();
            if (dt.Rows.Count < 1)
            {
                txbFilter.Visible = false;
                cbFilterBy.Visible = false;
                cbFliterByStatus.Visible = false;
                return;
            }else
            {
                txbFilter.Visible = true;
                cbFilterBy.Visible = true;
                cbFliterByStatus.Visible = true;
            }
            this.cbFliterByStatus.Items.AddRange(new string[] { "All", "New", "Canceld", "Completed" });
            this.cbFliterByStatus.SelectedIndex = 0;

            cbFilterBy.Items.Add("None");

            dt.Columns.Remove("Class Name");
            dt.Columns.Remove("Application Date");
            dt.Columns.Remove("Passed Tests");
            foreach (DataColumn dc in dt.Columns)
            {
                cbFilterBy.Items.Add(dc.ColumnName);
            }


            this.cbFilterBy.SelectedIndex = 0;
        }
        private void LoadDataToDataGridView()
        {
            dgvApplications.DataSource = this.GetDataTable();

            if (dgvApplications.Rows.Count > 2)
            {
                lblRecords.Text = "Records: " + dgvApplications.Rows.Count.ToString();
            }
            else
            {
                lblRecords.Text = "Record: " + dgvApplications.Rows.Count.ToString();
            }
        }
        private void MangeLoacalDrivingLicenseApplications_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        private void LoadData()
        {
            LoadComboBoxData();
            LoadDataToDataGridView();
        }
        private void AddApplication_Click(object sender, EventArgs e)
        {
            FormAddLocalLicense formAddLocalLicense = new FormAddLocalLicense(clsUser.UserName);
            formAddLocalLicense.ShowDialog();

            if (dgvApplications.Rows.Count == 0)
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
                this.cbFliterByStatus.Visible = false; this.txbFilter.Visible = false;
            }
            else if (cbFilterBy.SelectedIndex == 4)
            {
                this.cbFliterByStatus.Visible = true; this.txbFilter.Visible = false;
            }
            else
            {
                this.txbFilter.Visible = true; this.cbFliterByStatus.Visible = false;

                switch (cbFilterBy.SelectedIndex)
                {
                    case 1:
                        UpdateKeyPressHandlers(TextBoxKeyPressOnlyNumbers);
                        break;
                    case 2:
                        UpdateKeyPressHandlers(TextBoxKeyPressOnlyLettersOrNumbers);
                        break;
                    default:
                        UpdateKeyPressHandlers(TextBoxKeyPressOnlyLetters);
                        break;
                }

            }

            if (cbFilterBy.SelectedIndex == 0) cbFliterByStatus.SelectedIndex = 0;
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
        private void UpdateKeyPressHandlers(KeyPressEventHandler handler)
        {
            txbFilter.KeyPress -= TextBoxKeyPressOnlyNumbers;
            txbFilter.KeyPress -= TextBoxKeyPressOnlyLetters;
            txbFilter.KeyPress -= TextBoxKeyPressOnlyLettersOrNumbers;

            txbFilter.KeyPress += handler;
        }
        private void FilterDataGridView()
        {

            string FilterValue = txbFilter.Text;
            string ColumnName = cbFilterBy.Text;

            DataView dvUsers = GetDataTable().AsDataView();

            if ((cbFilterBy.SelectedIndex > 0 && !string.IsNullOrEmpty(FilterValue)) || cbFliterByStatus.SelectedIndex > 0)
            {
                if (cbFilterBy.SelectedIndex == 3 || cbFilterBy.SelectedIndex == 2)
                {
                    dvUsers.RowFilter = $" [{ColumnName}] LIKE '%{FilterValue}%'";
                }
                else if (cbFilterBy.SelectedIndex == 4)
                {

                    if (cbFliterByStatus.SelectedIndex == 1)
                    {
                        dvUsers.RowFilter = $"[{ColumnName}] = 'New'";
                    }
                    else if (cbFliterByStatus.SelectedIndex == 2)
                    {
                        dvUsers.RowFilter = $"[{ColumnName}] = 'Canceld'";
                    }
                    else if (cbFliterByStatus.SelectedIndex == 3)
                    {
                        dvUsers.RowFilter = $"[{ColumnName}] = 'Completed'";
                    }
                    else
                    {
                        LoadDataToDataGridView();
                    }

                }

                else if (cbFilterBy.SelectedIndex == 1)
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

                dgvApplications.DataSource = dvUsers;

                if (dgvApplications.Rows.Count > 2)
                {
                    lblRecords.Text = "Records: " + dgvApplications.Rows.Count.ToString();
                }
                else
                {
                    lblRecords.Text = "Record: " + dgvApplications.Rows.Count.ToString();
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
        private void TextBoxKeyPressOnlyLetters(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void TextBoxKeyPressOnlyLettersOrNumbers(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetterOrDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }



        ////////////////////////////////////////////////////
        ///              Context menu strep             ///
        ///////////////////////////////////////////////////

        private void canceldApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {


            if (MessageBox.Show("Are you sure you want to cancel application [" + dgvApplications.CurrentRow.Cells[0].Value + "]", "Confirm Canceld", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                if (clsApplications.UpdateApplicationStatus(clsLocalDrivingLicensesApplication.FindLocalDrvingLicenseAppByID((int)dgvApplications.CurrentRow.Cells[0].Value).BaseApplicationID, (byte)2))
                {
                    MessageBox.Show("Application canceld successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDataToDataGridView();
                }
                else
                    MessageBox.Show("This application cannot be canceld because they are associated with active records.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            LoadDataToDataGridView();


        }

        private void scheduleVisionTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            const int VisionTestID = 1;
            FormListAppointmentTest form = new FormListAppointmentTest(clsUser.UserID, (int)dgvApplications.CurrentRow.Cells[0].Value, VisionTestID);
            form.ShowDialog();
            LoadDataToDataGridView();
        }

        private void scheduleWrittenTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            const int WrittenTestID = 2;
            FormListAppointmentTest form = new FormListAppointmentTest(clsUser.UserID, (int)dgvApplications.CurrentRow.Cells[0].Value, WrittenTestID);
            form.ShowDialog();
            LoadDataToDataGridView();
        }

        private void scheduleStreetTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            const int StreetTestID = 3;
            FormListAppointmentTest form = new FormListAppointmentTest(clsUser.UserID, (int)dgvApplications.CurrentRow.Cells[0].Value, StreetTestID);
            form.ShowDialog();
            LoadDataToDataGridView();
        }

        private int _LicenseID = -1;
        private void dgvApplications_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {

            if (e.RowIndex >= 0 && e.Button == MouseButtons.Right)
            {
                dgvApplications.ClearSelection();
                dgvApplications.Rows[e.RowIndex].Selected = true;

                byte PassedTestCount = Convert.ToByte(dgvApplications.Rows[e.RowIndex].Cells["Passed Tests"].Value);
                string ApplicationStatus = dgvApplications.Rows[e.RowIndex].Cells["Application Status"].Value.ToString();

                this._LicenseID = clsLicenses.GetLicenseIDByApplicationID(clsLocalDrivingLicensesApplication.FindLocalDrvingLicenseAppByID((int)dgvApplications.CurrentRow.Cells[0].Value).BaseApplicationID);

                if (this._LicenseID != -1) 
                {
                    // after issue
                    EnabledContextMenuItem(deletToolStripMenuItem, false);
                    EnabledContextMenuItem(editApplicationToolStripMenuItem, false);
                    EnabledContextMenuItem(canceldApplicationToolStripMenuItem, false);
                    EnabledContextMenuItem(issueDrivingLicenseFirstTimeToolStripMenuItem, false);

                    EnabledContextMenuItem(showLicenseToolStripMenuItem, true);
                }
                else
                {
                    EnabledContextMenuItem(deletToolStripMenuItem, true);
                    EnabledContextMenuItem(editApplicationToolStripMenuItem, true);
                    EnabledContextMenuItem(canceldApplicationToolStripMenuItem, true);
                    EnabledContextMenuItem(issueDrivingLicenseFirstTimeToolStripMenuItem, true);

                    EnabledContextMenuItem(showLicenseToolStripMenuItem, false);
                }


                if (PassedTestCount == 3 || ApplicationStatus == "Canceled")
                {
                    EnabledContextMenuItem(sechduleTestsToolStripMenuItem, false);
                }



                else if (PassedTestCount == 0)
                {
                    EnabledContextMenuItem(sechduleTestsToolStripMenuItem, true);
                    EnabledContextMenuItem(scheduleVisionTestToolStripMenuItem, true);
                    EnabledContextMenuItem(scheduleWrittenTestToolStripMenuItem, false);
                    EnabledContextMenuItem(scheduleStreetTestToolStripMenuItem, false);
                }
                else if (PassedTestCount == 1)
                {
                    EnabledContextMenuItem(sechduleTestsToolStripMenuItem, true);
                    EnabledContextMenuItem(scheduleWrittenTestToolStripMenuItem, true);
                    EnabledContextMenuItem(scheduleVisionTestToolStripMenuItem, false);
                    EnabledContextMenuItem(scheduleStreetTestToolStripMenuItem, false);
                }
                else if (PassedTestCount == 2)
                {
                    EnabledContextMenuItem(sechduleTestsToolStripMenuItem, true);
                    EnabledContextMenuItem(scheduleStreetTestToolStripMenuItem, true);
                    EnabledContextMenuItem(scheduleVisionTestToolStripMenuItem, false);
                    EnabledContextMenuItem(scheduleWrittenTestToolStripMenuItem, false);
                }


                EnabledContextMenuItem(issueDrivingLicenseFirstTimeToolStripMenuItem, (PassedTestCount == 3 && ApplicationStatus == "New"));

            }

        }



        private void EnabledContextMenuItem(ToolStripMenuItem item, bool status)
        {
            item.Enabled = status;
        }

        private void showApplicationDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormShowApplicationDetails formShowApplicationDetails = new FormShowApplicationDetails((int)dgvApplications.CurrentRow.Cells[0].Value);
            formShowApplicationDetails.ShowDialog();
        }

        private void editApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var formEdite = new FormEditeLocalDrivingLicenseApplication((int)dgvApplications.CurrentRow.Cells[0].Value);
            formEdite.ShowDialog();
            LoadDataToDataGridView();


        }

        private void deletToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete application [" + dgvApplications.CurrentRow.Cells[0].Value + "]", "Confirm Delete", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {


                if (clsLocalDrivingLicensesApplication.DeleteLocalDrivingLicense((int)dgvApplications.CurrentRow.Cells[0].Value))
                {
                    MessageBox.Show("Application deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    if (dgvApplications.Rows.Count == 1)
                    {
                        LoadComboBoxData();
                        return;
                    }

                    LoadDataToDataGridView();
                }

                else
                    MessageBox.Show("This application has not deleted.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }


        // driver  and status (cmpleted)
        private void issueDrivingLicenseFirstTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frmIssue = new FormIssuDrivingLicenseForTheFirstTime((int)dgvApplications.CurrentRow.Cells[0].Value,clsUser.UserID);
            frmIssue.ShowDialog();
            LoadDataToDataGridView();
        }

        private void showLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormShowLicense formShowLicense = new FormShowLicense(this._LicenseID);
            formShowLicense.ShowDialog();
        }

        private void showPersonLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = clsPerson.FindPerson((string)dgvApplications.CurrentRow.Cells["National No."].Value)._PersonID;
            FormShowPersonLicensesHistory licensesHistory = new FormShowPersonLicensesHistory(PersonID);
            licensesHistory.ShowDialog();
            LoadDataToDataGridView();
        }
    }
} 
