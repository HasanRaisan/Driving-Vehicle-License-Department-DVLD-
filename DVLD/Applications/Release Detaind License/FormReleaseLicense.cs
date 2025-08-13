using BusinessLayer;
using DVLD.Mange_Applications;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace DVLD.Serveces
{
    public partial class FormReleaseLicense : Form
    {
        private int _licenseID = -1;
        private bool _isDirect = false;
        clsLicense _LicenseInfo = null;
        clsDetainLicense _DetainInfo = null;






        public FormReleaseLicense()
        {
            InitializeComponent();
        }




        private void btnCanel_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        private void linkLabelShoLicensesHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var formShow = new FormShowPersonLicensesHistory(this._LicenseInfo.DriverInfo.PersonID);
            formShow.ShowDialog();
        }

        private void linkLabelShowLicnesInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FormShowLicense formShow = new FormShowLicense(this._licenseID);
            formShow.ShowDialog();
        }



        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtLicenseID.Text))
            {
                this._licenseID = Convert.ToInt32(txtLicenseID.Text);
            }
            else return;

            if ((this._LicenseInfo = clsLicense.FindLicense(this._licenseID)) == null)
            {
                this.userControlDrivingLicenseInfo1.LoadDefaultData();
                this.userControlReleaseLicense1.SetDefualtValue();
                this.linkLabelShoLicensesHistory.Enabled = false;
                this.btnRelease.Enabled = false;
                MessageBox.Show("Could not find the license. Please check the information and try again.", "License Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                this.userControlDrivingLicenseInfo1.SetLicenseID(this._licenseID);
                this.linkLabelShoLicensesHistory.Enabled = true;
            }

            if ((this._DetainInfo = clsDetainLicense.FindByLicenseID(this._licenseID)) == null)
            {
                this.userControlDrivingLicenseInfo1.LoadDefaultData();
                this.userControlReleaseLicense1.SetDefualtValue();
                this.btnRelease.Enabled = false;
                MessageBox.Show("The license is not detained.", "License Not Detained", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                this.userControlReleaseLicense1.SetDetainInfoAndUsername(this._DetainInfo, clsGlobal.CurrentUser.UserName);
                this.btnRelease.Enabled = true;
            }
        }



        private void DefualtValue()
        {
            this._LicenseInfo = null;

            this.btnRelease.Enabled = false;
            this.linkLabelShoLicensesHistory.Enabled = false;
            this.linkLabelShowLicnesInfo.Enabled = false;

            this.userControlReleaseLicense1.SetDefualtValue();
            this.userControlDrivingLicenseInfo1.LoadDefaultData();
        }

        private void txtLicenseID_TextChanged(object sender, EventArgs e)
        {
            if (this._isDirect) return;
            DefualtValue();
        }

        private void FormReleaseLicense_Load(object sender, EventArgs e)
        {
            this.btnClose.Visible = false;
            this.linkLabelShowLicnesInfo.Enabled = false;

            if (this._isDirect) return;

            this.btnRelease.Enabled = false;
            this.linkLabelShoLicensesHistory.Enabled = false;
            this.userControlReleaseLicense1.SetDefualtValue();
        }

        private void btnRelease_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to release this license?", "Relese License", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int ApplicationID = 0;
               if (this._LicenseInfo.ReleaseDetainedLicense(clsGlobal.CurrentUser.UserID, ref ApplicationID))
               {
                    this.userControlReleaseLicense1.SetApplicationID(ApplicationID);
                    this.userControlDrivingLicenseInfo1.SetLicenseID(this._licenseID);
                    this.GroupBoxFilter.Enabled = false;
                    this.linkLabelShowLicnesInfo.Enabled = true;
                    this.btnRelease.Visible = false;
                    this.btnCanel.Visible = false;
                    this.btnClose.Visible = true;

                    MessageBox.Show("The license has been successfully released.", "Release Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
               }
               else
               {
                   MessageBox.Show("Failed to release the license. Please try again.", "Release Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            
               }
            }
        }


        public void SetLicensIDForDirectRelease(int licenseID)
        {
            this._isDirect = true;
            this._licenseID = licenseID;
            this.txtLicenseID.Text = this._licenseID.ToString();
            GroupBoxFilter.Enabled = false;
            this.userControlReleaseLicense1.SetDefualtValue();



            if ((this._LicenseInfo = clsLicense.FindLicense(this._licenseID)) == null)
            {
                this.userControlDrivingLicenseInfo1.LoadDefaultData();
                this.userControlReleaseLicense1.SetDefualtValue();
                this.linkLabelShoLicensesHistory.Enabled = false;
                this.btnRelease.Enabled = false;
                MessageBox.Show("Could not find the license. Please check the information and try again.", "License Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                this.userControlDrivingLicenseInfo1.SetLicenseID(this._licenseID);
                this.linkLabelShoLicensesHistory.Enabled = true;
            }

            if ((this._DetainInfo = clsDetainLicense.FindByLicenseID(this._licenseID)) == null)
            {
                this.userControlReleaseLicense1.SetDefualtValue();
                this.btnRelease.Enabled = false;
                MessageBox.Show("The license is not detained.", "License Not Detained", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                this.userControlReleaseLicense1.SetDetainInfoAndUsername(this._DetainInfo, clsGlobal.CurrentUser.UserName);
                this.btnRelease.Enabled = true;
            }
        }

        private void txtLicenseID_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

        }
    }
}
