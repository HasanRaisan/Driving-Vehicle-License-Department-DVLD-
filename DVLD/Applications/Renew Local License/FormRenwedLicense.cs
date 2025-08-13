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

namespace DVLD.Serveces
{
    public partial class FormRenwedLicense : Form
    {

        private int _OldLicenseID = -1;
        clsLicense OldLicenseInfo = null;



        private clsLicense _clsLicense = null;
        private clsLicense LicenseInfo()
        {
            if (this._clsLicense == null)
                this._clsLicense = clsLicense.FindLicense(this._OldLicenseID);

            return this._clsLicense;

        }




        public FormRenwedLicense()
        {
            InitializeComponent();
        }



        private void btnSearch_Click(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(txtLicenseID.Text))
            {
                this._OldLicenseID = Convert.ToInt32(txtLicenseID.Text);
            }
            else return;


            // check if exist and get license info
            if (( OldLicenseInfo = clsLicense.FindLicense(_OldLicenseID)) != null)
            {
                this.userControlDrivingLicenseInfo1.SetLicenseID(this._OldLicenseID);
                this.linkLabelShoLicensesHistory.Enabled = true;


                if (OldLicenseInfo.ExpirationDate < DateTime.Today)
                {
                    this.userControlShowRenwedApplicationsInfo1.SetOldLicenseIDAndUsername(this._OldLicenseID, clsGlobal.CurrentUser.UserName);

                    // check activation 
                    if (OldLicenseInfo.IsActive)
                    {
                        btnRenew.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show("The license is not active.", "Inactive License", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        btnRenew.Enabled = false;
                        this.userControlShowRenwedApplicationsInfo1.LoadDefaultValue();

                        return;
                    }
                }
                else
                {

                    MessageBox.Show($"Selected License is not yet expiared, it will be ecpire on: {OldLicenseInfo.ExpirationDate.ToString("dd/MMM/yyyy")}", "Not allowed", MessageBoxButtons.OK,MessageBoxIcon.Error);
                    this.userControlShowRenwedApplicationsInfo1.LoadDefaultValue();

                    return;
                }

            }
            else
            {
                this.userControlDrivingLicenseInfo1.LoadDefaultData();
                this.userControlShowRenwedApplicationsInfo1.LoadDefaultValue();
                this.linkLabelShoLicensesHistory.Enabled = false;
                this.btnRenew.Enabled = false;
                return ;
            }

        }


        private void txtLicenseID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }



        private void txtLicenseID_TextChanged(object sender, EventArgs e)
        {
            this._clsLicense = null;
        }


        private void btnRenew_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to renew the license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {

                clsLicense NewLicense = this.OldLicenseInfo.RenewLicense(this.userControlShowRenwedApplicationsInfo1.GetNotes(), clsGlobal.CurrentUser.UserID);
                if (NewLicense != null)
                {

                    MessageBox.Show($"License has renewed successfully with ID [{NewLicense.LicenseID}]", "License Renewed", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    linkLabelShowLicnesInfo.Enabled = true;
                    this.btnClose.Visible = true;
                    this.btnCanel.Visible = false;
                    this.btnRenew.Visible = false;

                    linkLabelShowLicnesInfo.Tag = NewLicense.LicenseID;

                    this.userControlShowRenwedApplicationsInfo1.SetTxetBoxNotesNotEnabled();
                    this.userControlShowRenwedApplicationsInfo1.SetRenwedAppAndLicenseID(NewLicense.ApplicationID, NewLicnseID:NewLicense.LicenseID);
                    this.userControlDrivingLicenseInfo1.SetLicenseID(NewLicense.LicenseID);
                }
                else
                {
                    // delete the application 
                }
            }

        }



        private void btnCanel_Click(object sender, EventArgs e)
        {
            this.Close();
        }




        private void FormRenwedLicense_Load(object sender, EventArgs e)
        {
            this.btnClose.Visible = false;  
            this.btnRenew.Enabled = false;
            this.linkLabelShoLicensesHistory.Enabled = false;
            this.linkLabelShowLicnesInfo.Enabled = false;
        }

        private void linkLabelShoLicensesHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var formShow = new FormShowPersonLicensesHistory(this.OldLicenseInfo.DriverInfo.PersonID);
            formShow.ShowDialog();
        }

        private void linkLabelShowLicnesInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FormShowLicense formShow = new FormShowLicense((int)this.linkLabelShowLicnesInfo.Tag);
            formShow.ShowDialog();
        }
    }
}
