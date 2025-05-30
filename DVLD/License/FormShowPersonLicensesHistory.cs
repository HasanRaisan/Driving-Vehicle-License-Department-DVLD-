using BusinessLayer;
using DVLD.International_Applications;
using DVLD.Mange_Applications;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD
{
    public partial class FormShowPersonLicensesHistory : Form
    {
        int PersonID = -1;
        public FormShowPersonLicensesHistory(int PersonID)
        {
            this.PersonID = PersonID;
            InitializeComponent();
            this.userControlShowPersonDetails1.SetPersonID(PersonID);
        }

        void LoadLocalLicenses()
        {
            dgvLocalLicenses.DataSource = clsLicenses.GetAllLicenseForPersonByPersonID(this.PersonID);

            byte LicensesCount = (byte) dgvLocalLicenses.Rows.Count;
            lblLocalRecords.Text = LicensesCount > 2 ? $"# Rocords: {LicensesCount}" : $"# Rocord: {LicensesCount}";


        }

        void LoadInternationalLicenses()
        {
            dgvInternationalLicense.DataSource = clsInternationalLicenses.GetAllLicenseInternationalForPersonByPersonID(this.PersonID);

            byte LicensesCount = (byte)dgvInternationalLicense.Rows.Count;
            lblInternationalRecords.Text = LicensesCount > 2 ? $"# Rocords: {LicensesCount}" : $"# Rocord: {LicensesCount}";

        }

        private void FormShowPersonLicensesHistory_Load(object sender, EventArgs e)
        {
            LoadLocalLicenses();
            LoadInternationalLicenses();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void showLicenseDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var ShowLicenseDetails = new FormShowLicense((int)dgvLocalLicenses.CurrentRow.Cells[0].Value);
            ShowLicenseDetails.ShowDialog();
        }

        private void showLicenseDetailsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var ShowLicenseDetails = new FormShowInternatonalLicenseInfo((int)dgvInternationalLicense.CurrentRow.Cells[0].Value);
            ShowLicenseDetails.ShowDialog();
        }
    }
}
