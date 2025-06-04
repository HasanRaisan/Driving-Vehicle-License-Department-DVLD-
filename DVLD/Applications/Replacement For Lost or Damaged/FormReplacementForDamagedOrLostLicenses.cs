using BusinessLayer;
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
    public partial class FormReplacementForDamagedOrLostLicenses : Form
    {
        //string _username = string.Empty;
        byte _applicationTypeID = 0;
        int _oldLicenseID = -1;

        const byte LostAppID = 3;
        const byte DamagedAppID = 4;



        private int _personID = -1;
        private int GetPersonIDForLicenseID()
        {
            if (_personID == -1)
            {
                _personID = clsLicenses.GetPersonIDByLicenseID(this._oldLicenseID);
            }
            return _personID;
        }

        //private int _userID = -1;
        //private int GetUserID()
        //{
        //    if (_userID == -1)
        //    {
        //        _userID = clsUsers.GetUserIDByUserName(this._username);
        //    }
        //    return _userID;
        //}


        private clsLicenses _clsLicense = null;
        private clsLicenses LicenseInfo()
        {
            if (this._clsLicense == null)
                this._clsLicense = clsLicenses.FindLicense(this._oldLicenseID);

            return this._clsLicense;

        }


        public FormReplacementForDamagedOrLostLicenses()
        {
            InitializeComponent();
        }

        clsLicenses OldLicenseInfo = null;


        private void RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is RadioButton radioButton && radioButton.Checked)
            {
                if (radioButton == rbLostLicense)
                {
                    this. _applicationTypeID = LostAppID;
                    this.lblHeadLine.Text = "Replacement For Lost License";
                    this.userControlShowReplacementApplicationDetails1.SetApplicationTypeID(this._applicationTypeID);
                }
                else if (radioButton == rbDamagedLicense)
                {
                    this._applicationTypeID = DamagedAppID;
                    this.lblHeadLine.Text = "Replacement For Damaged License";
                    this.userControlShowReplacementApplicationDetails1.SetApplicationTypeID(this._applicationTypeID);
                }
            }
        }
        void LoadDefaultValue()
        {
            this._applicationTypeID = DamagedAppID;
            this.lblHeadLine.Text = "Replacement For Damaged License";
            this.userControlShowReplacementApplicationDetails1.SetApplicationTypeID(this._applicationTypeID);

        }
        private void FormReplacementForDamagedOrLostLicenses_Load(object sender, EventArgs e)
        {
            LoadDefaultValue();


            this.btnClose.Visible = false;
            this.btnReplae.Enabled = false;
            this.linkLabelShoLicensesHistory.Enabled = false;
            this.linkLabelShowLicnesInfo.Enabled = false;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtLicenseID.Text))
            {
                this._oldLicenseID = Convert.ToInt32(txtLicenseID.Text);
            }
            else return;



            // check if exist and get license info
            if ((OldLicenseInfo = clsLicenses.FindLicense(_oldLicenseID)) != null)
            {
                this.userControlDrivingLicenseInfo1.SetLicenseID(this._oldLicenseID);
                this.linkLabelShoLicensesHistory.Enabled = true;



                    // check activation 
                    if (OldLicenseInfo._IsActive)
                    {
                      this.userControlShowReplacementApplicationDetails1.SetOldLicenseIDAndUsername(this._oldLicenseID, clsGlobal.CurrentUser.UserName);

                        btnReplae.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show("The license is not active.", "Inactive License", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        btnReplae.Enabled = false;
                        this.userControlShowReplacementApplicationDetails1.LoadDefaultValue();

                        return;
                    }

            }
            else
            {
                this.userControlDrivingLicenseInfo1.LoadDefaultData();
                this.userControlShowReplacementApplicationDetails1.LoadDefaultValue();
                this.linkLabelShoLicensesHistory.Enabled = false;
                btnReplae.Enabled = false;
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


        private clsApplications SaveApplication()
        {

            var clsAppType = clsApplicationTypes.FindApplication(this._applicationTypeID);
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


        private void btnReplace_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to replace the license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                var clsApplication = SaveApplication();
                if (clsApplication == null) { MessageBox.Show("Save Failed", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

                var NewLicense = new clsLicenses();

                NewLicense._LicenseClassID = LicenseInfo()._LicenseClassID;
                NewLicense._CreatedByUserID = clsGlobal.CurrentUser.UserID;
                // no fees for replacement
                NewLicense._PaidFees = 0;
                NewLicense._DriverID = LicenseInfo()._DriverID;
                NewLicense._ExpirationDate = LicenseInfo()._ExpirationDate;
                NewLicense._ApplicationID = clsApplication._ApplicationID;


                NewLicense.IssueReason = rbDamagedLicense.Checked? clsLicenses .enIssueReason.DamagedReplacement:clsLicenses.enIssueReason.LostReplacement;


                NewLicense._Notes = LicenseInfo()._Notes;
                NewLicense._IsActive = true;


                if (NewLicense.Save())
                {
                   SetOldLicenseInactive();

                    MessageBox.Show($"License has renewed successfully with ID [{NewLicense._LicenseID}]", "License Renewed", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    linkLabelShowLicnesInfo.Enabled = true;
                    this.btnClose.Visible = true;
                    this.btnCanel.Visible = false;
                    this.btnReplae.Visible = false;
                    GroupBoxFilter.Enabled = false;

                    linkLabelShowLicnesInfo.Tag = NewLicense._LicenseID;

                    // application status turn to complete
                    clsApplications.UpdateApplicationStatus(clsApplication._ApplicationID, 3);
                    this.userControlShowReplacementApplicationDetails1.SetReplacementdAppAndLicenseID(clsApplication._ApplicationID, NewLicnseID: NewLicense._LicenseID);
                    this.userControlDrivingLicenseInfo1.SetLicenseID(NewLicense._LicenseID);
                }
                else
                {
                    // delete the application 
                    clsApplication.DeleteApplication();
                }
            }

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



        private void btnCanel_Click(object sender, EventArgs e)
        {
            this.Close();
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
