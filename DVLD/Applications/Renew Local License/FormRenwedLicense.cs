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


        private int _personID = -1;
        private int GetPersonIDForLicenseID()
        {
            if (_personID == -1)
            {
                _personID = clsLicenses.GetPersonIDByLicenseID(this._OldLicenseID);
            }
            return _personID;
        }



        private clsLicenses _clsLicense = null;
        private clsLicenses LicenseInfo()
        {
            if (this._clsLicense == null)
                this._clsLicense = clsLicenses.FindLicense(this._OldLicenseID);

            return this._clsLicense;

        }




        public FormRenwedLicense()
        {
            InitializeComponent();
        }


        clsLicenses OldLicenseInfo = null;

        private void btnSearch_Click(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(txtLicenseID.Text))
            {
                this._OldLicenseID = Convert.ToInt32(txtLicenseID.Text);
            }
            else return;


            // check if exist and get license info
            if (( OldLicenseInfo = clsLicenses.FindLicense(_OldLicenseID)) != null)
            {
                this.userControlDrivingLicenseInfo1.SetLicenseID(this._OldLicenseID);
                this.linkLabelShoLicensesHistory.Enabled = true;


                if (OldLicenseInfo._ExpirationDate < DateTime.Today)
                {
                    this.userControlShowRenwedApplicationsInfo1.SetOldLicenseIDAndUsername(this._OldLicenseID, clsGlobal.CurrentUser.UserName);

                    // check activation 
                    if (OldLicenseInfo._IsActive)
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

                    MessageBox.Show($"Selected License is not yet expiared, it will be ecpire on: {OldLicenseInfo._ExpirationDate.ToString("dd/MMM/yyyy")}", "Not allowed", MessageBoxButtons.OK,MessageBoxIcon.Error);
                    this.userControlShowRenwedApplicationsInfo1.LoadDefaultValue();

                    return;
                }

            }
            else
            {
                this.userControlDrivingLicenseInfo1.LoadDefaultData();
                this.userControlShowRenwedApplicationsInfo1.LoadDefaultValue();
                this.linkLabelShoLicensesHistory.Enabled = false;
                btnRenew.Enabled = false;
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


        private clsApplications SaveApplication()
        {
            const int ApplicationsTypeID = 2;

            var clsAppType = clsApplicationTypes.FindApplication(ApplicationsTypeID);
            var clsApplication = new clsApplications();
            clsApplication._ApplicantPersonID = GetPersonIDForLicenseID();
            clsApplication._PaidFees = clsAppType.ApplicationFees;
            clsApplication._ApplicationTypeID = clsAppType.ApplicationID;
            clsApplication._CreatedByUserID = clsGlobal.CurrentUser.UserID;
            clsApplication._LicenseClassID = LicenseInfo()._LicenseClassID;

            if (clsApplication.Save())
            {
                return clsApplication;
            }
            else return null;

        }

        private void txtLicenseID_TextChanged(object sender, EventArgs e)
        {
            this._clsLicense = null;
            this._personID = -1;

        }

        void SetOldLicenseInactive()
        {
           
            _clsLicense._IsActive = false;
            _clsLicense.UpdateLicenseActivationStatus();
        }

        private void btnRenew_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to renew the license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                var clsApplication = SaveApplication();
                if (clsApplication == null) { MessageBox.Show("Save Failed", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

                var NewLicense = new clsLicenses();

                NewLicense._LicenseClassID = LicenseInfo()._LicenseClassID;
                NewLicense._CreatedByUserID = clsGlobal.CurrentUser.UserID;
                NewLicense._PaidFees = LicenseInfo()._PaidFees;
                NewLicense._DriverID = LicenseInfo()._DriverID;
                NewLicense._ExpirationDate = DateTime.Today.AddYears(clsLicenseClasses.FindLicenseClass(NewLicense._LicenseClassID).DefaultValidityLength);
                NewLicense._ApplicationID = clsApplication._ApplicationID;
                NewLicense.IssueReason = clsLicenses.enIssueReason.Renew;
                NewLicense._Notes = this.userControlShowRenwedApplicationsInfo1.GetNotes();
                NewLicense._IsActive = true;

                SetOldLicenseInactive();

                if (NewLicense.Save())
                {

                    MessageBox.Show($"License has renewed successfully with ID [{NewLicense._LicenseID}]", "License Renewed", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    linkLabelShowLicnesInfo.Enabled = true;
                    this.btnClose.Visible = true;
                    this.btnCanel.Visible = false;
                    this.btnRenew.Visible = false;

                    linkLabelShowLicnesInfo.Tag = NewLicense._LicenseID;

                    // application status turn to complete
                    clsApplications.UpdateApplicationStatus(clsApplication._ApplicationID, 3);
                    this.userControlShowRenwedApplicationsInfo1.SetTxetBoxNotesNotEnabled();
                    this.userControlShowRenwedApplicationsInfo1.SetRenwedAppAndLicenseID(clsApplication._ApplicationID, NewLicnseID:NewLicense._LicenseID);
                    this.userControlDrivingLicenseInfo1.SetLicenseID(NewLicense._LicenseID);
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
            var formShow = new FormShowPersonLicensesHistory(GetPersonIDForLicenseID());
            formShow.ShowDialog();
        }

        private void linkLabelShowLicnesInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FormShowLicense formShow = new FormShowLicense((int)this.linkLabelShowLicnesInfo.Tag);
            formShow.ShowDialog();
        }
    }
}
