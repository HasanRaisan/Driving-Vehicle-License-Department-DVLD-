using BusinessLayer;
using System;
using System.Windows.Forms;

namespace DVLD.International_Applications
{
    public partial class FormAddNewInternationlalLicense : Form
    {
        private int _LicenseID = -1;


        private int _personID = -1;
        private int GetPersonIDForLicenseID()
        {
            if (_personID == -1) 
            {
                _personID = clsLicenses.GetPersonIDByLicenseID(this._LicenseID);
            }
            return _personID; 
        }



        private clsLicenses _clsLicense = null;
        private clsLicenses LicenseInfo()
        {
            if(this._clsLicense == null)
            this._clsLicense = clsLicenses.FindLicense(this._LicenseID);

            return this._clsLicense;
            
        }

        public FormAddNewInternationlalLicense()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {


            if (!string.IsNullOrEmpty(txtLicenseID.Text))
            {
                 this._LicenseID = Convert.ToInt32(txtLicenseID.Text);
            } else return;



            int InternationalLicenseID = int.MinValue;
            if(clsInternationalLicenses.IsLocaLicenseLicenseUsedByID(this._LicenseID, ref InternationalLicenseID))
            {
                MessageBox.Show($"This person already has an active international license. With ID = [{InternationalLicenseID}]", "Not allwoed", MessageBoxButtons.OK, MessageBoxIcon.Error);

                this.userControlDrivingLicenseInfo1.SetLicenseID(this._LicenseID);
                this.userControlShowInternationalLincenseApplicationDetails1.LoadDefaultValue();
                this.linkLabelShoLicensesHistory.Enabled = true;
                btnIssue.Enabled = false;
                return;
            }
            
            if (clsLicenses.IsLicenseExist(this._LicenseID))
            {
                this.userControlDrivingLicenseInfo1.SetLicenseID(this._LicenseID);
                this.userControlShowInternationalLincenseApplicationDetails1.SetLocalLicenseIDAndUsername(this._LicenseID, clsGlobal.CurrentUser.UserName);
                this.linkLabelShoLicensesHistory.Enabled = true;
       
            }
            else
            {
                this.userControlDrivingLicenseInfo1.LoadDefaultData();
                this.userControlShowInternationalLincenseApplicationDetails1.LoadDefaultValue();
                this.linkLabelShoLicensesHistory.Enabled = false;
                this.btnIssue.Enabled = false;
                return;

            }

            // check activation 
            if(LicenseInfo()._IsActive)
            {
                btnIssue.Enabled = true;
            }
            else
            {
                MessageBox.Show("The license is not active.", "Inactive License", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnIssue.Enabled = false;
                return;
            }
        }

        private void txtLicenseID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void linkLabelShoLicensesHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {   
            var formShow = new FormShowPersonLicensesHistory(GetPersonIDForLicenseID());
            formShow.ShowDialog();
        }

        private void btnCanel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormAddNewInternationlalLicense_Load(object sender, EventArgs e)
        {
            linkLabelShowLicnesInfo.Enabled = false;
            this.linkLabelShoLicensesHistory.Enabled = false;
            this.btnClose.Visible = false;
            this.btnIssue.Enabled = false;

        }

        private clsApplications SaveApplication()
        {
            var clsAppType =  clsApplicationTypes.FindApplication(6);
            var clsApplication = new clsApplications();
            clsApplication._ApplicantPersonID = GetPersonIDForLicenseID();
            clsApplication._PaidFees = clsAppType.ApplicationFees;
            clsApplication._ApplicationTypeID = clsAppType.ApplicationID;
            clsApplication._CreatedByUserID = clsGlobal.CurrentUser.UserID;
            clsApplication._LicenseClassID = LicenseInfo()._LicenseClassID;

            if (clsApplication.Save())
            {
               
              return clsApplication;
            } else return null;

        }

        private void btnIssue_Click(object sender, EventArgs e)
        {


            if (MessageBox.Show("Are you sure you want to issue the license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
               var clsApplication = SaveApplication();
               if(clsApplication == null) {   MessageBox.Show("Save Failed","Failed",MessageBoxButtons.OK,MessageBoxIcon.Error); return; }
               
               var clsIntLicense = new clsInternationalLicenses();
               
                clsIntLicense.ApplicationID = clsApplication._ApplicationID;
                clsIntLicense.DriverID = clsDrivers.FindDriverByPersonID(clsApplication._ApplicantPersonID)._DriverID;
                clsIntLicense.ExpirationDate =LicenseInfo()._ExpirationDate;
                clsIntLicense.IssuedUsingLocalLicenseID = this._LicenseID;
                clsIntLicense.IsActive = true;
                clsIntLicense.CreatedByUserID = clsGlobal.CurrentUser.UserID;

                if ( clsIntLicense.Save())
               {
                    MessageBox.Show($"Internationl license has issued successfully with ID [{clsIntLicense.InternationalLicenseID}]", "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    linkLabelShowLicnesInfo.Enabled = true;
                    this.btnClose.Visible = true;
                    this.btnCanel.Visible = false;
                    this.btnIssue.Visible = false;

                    linkLabelShowLicnesInfo.Tag = clsIntLicense.InternationalLicenseID;

                    clsApplications.UpdateApplicationStatus(clsApplication._ApplicationID, 3);
                    // application status turn to complete
               }
               else
               {
                    // delete the application 
                    clsApplication.DeleteApplication();
               }
            }              
        }

        private void linkLabelShowLicnesInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {        
            FormShowInternatonalLicenseInfo formShow = new FormShowInternatonalLicenseInfo((int)linkLabelShowLicnesInfo.Tag);
            formShow.ShowDialog();
        }

        private void txtLicenseID_TextChanged(object sender, EventArgs e)
        {
            this._clsLicense = null;
            this._personID = -1;
            this.linkLabelShowLicnesInfo.Enabled = false;
            this.linkLabelShoLicensesHistory.Enabled = false;
        }
    }
}
