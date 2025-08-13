using BusinessLayer;
using System;
using System.Windows.Forms;

namespace DVLD.International_Applications
{
    public partial class FormAddNewInternationlalLicense : Form
    {
        private int _LocalLicenseID = -1;
        private clsLicense _LocalLicenseInfo = null;



        public FormAddNewInternationlalLicense()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {


            if (!string.IsNullOrEmpty(txtLicenseID.Text))
            {
                 this._LocalLicenseID = Convert.ToInt32(txtLicenseID.Text);
            } else return;



            int InternationalLicenseID = int.MinValue;

            if(clsInternationalLicense.IsLocaLicenseLicenseUsedByID(this._LocalLicenseID, ref InternationalLicenseID))
            {
                MessageBox.Show($"This person already has an active international license. With ID = [{InternationalLicenseID}]", "Not allwoed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this._LocalLicenseInfo = clsLicense.FindLicense(this._LocalLicenseID);
                this.userControlDrivingLicenseInfo1.SetLicenseID(this._LocalLicenseID);
                this.userControlShowInternationalLincenseApplicationDetails1.LoadDefaultValue();
                this.linkLabelShoLicensesHistory.Enabled = true;
                this.btnIssue.Enabled = false;
                return;
            }
            
            if ((this._LocalLicenseInfo = clsLicense.FindLicense(this._LocalLicenseID)) != null)
            {
                this.userControlDrivingLicenseInfo1.SetLicenseID(this._LocalLicenseID);
                this.userControlShowInternationalLincenseApplicationDetails1.SetLocalLicenseIDAndUsername(this._LocalLicenseID, clsGlobal.CurrentUser.UserName);
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
            if(this._LocalLicenseInfo.IsActive)
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
            var formShow = new FormShowPersonLicensesHistory(this._LocalLicenseInfo.DriverInfo.PersonID);
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


        private void btnIssue_Click(object sender, EventArgs e)
        {


            if (MessageBox.Show("Are you sure you want to issue the license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {

               var clsIntLicense = new clsInternationalLicense();
                // base class
                clsIntLicense.ApplicantPersonID = this._LocalLicenseInfo.DriverInfo.PersonID;
                clsIntLicense.ApplicationStatus = clsApplication.enApplicationStatus.Completed; ;
                clsIntLicense.PaidFees = clsApplicationType.FindApplication((int)clsApplication.enApplicationType.NewInternationalLicense).ApplicationFees;
                clsIntLicense.CreatedByUserID = clsGlobal.CurrentUser.UserID;
                clsIntLicense.LicenseClassID = this._LocalLicenseInfo.LicenseClassID;

                // sub class
                clsIntLicense.DriverID = _LocalLicenseInfo.DriverID;
                clsIntLicense.ExpirationDate = this._LocalLicenseInfo.ExpirationDate;
                clsIntLicense.IssuedUsingLocalLicenseID = this._LocalLicenseID;
                clsIntLicense.IsActive = true;

                if ( clsIntLicense.Save())
                {
                    MessageBox.Show($"Internationl license has issued successfully with ID [{clsIntLicense.InternationalLicenseID}]", "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    linkLabelShowLicnesInfo.Enabled = true;
                    this.btnClose.Visible = true;
                    this.btnCanel.Visible = false;
                    this.btnIssue.Visible = false;

                    linkLabelShowLicnesInfo.Tag = clsIntLicense.InternationalLicenseID;

                }
                else
                {
                    MessageBox.Show("Faild to Issue International License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            this._LocalLicenseInfo = null;
            this.linkLabelShowLicnesInfo.Enabled = false;
            this.linkLabelShoLicensesHistory.Enabled = false;
        }
    }
}
