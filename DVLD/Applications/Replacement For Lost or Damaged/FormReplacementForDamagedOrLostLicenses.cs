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
        clsApplication.enApplicationType _ReplaceReason;
        int _LicenseID = -1;
        clsLicense _LicenseInfo = null;





        public FormReplacementForDamagedOrLostLicenses()
        {
            InitializeComponent();
        }



        private void RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is RadioButton radioButton && radioButton.Checked)
            {
                if (radioButton == rbLostLicense)
                {
                    this._ReplaceReason = clsApplication.enApplicationType.ReplaceLostDrivingLicense;
                    this.lblHeadLine.Text = "Replacement For Lost License";
                    this.userControlShowReplacementApplicationDetails1.SetApplicationTypeID((int)this._ReplaceReason);
                }
                else if (radioButton == rbDamagedLicense)
                {
                    this._ReplaceReason = clsApplication.enApplicationType.ReplaceLostDrivingLicense;
                    this.lblHeadLine.Text = "Replacement For Damaged License";
                    this.userControlShowReplacementApplicationDetails1.SetApplicationTypeID((int)this._ReplaceReason);
                }
            }
        }
        void LoadDefaultValue()
        {
            this._ReplaceReason = clsApplication.enApplicationType.ReplaceDamagedDrivingLicense;
            this.lblHeadLine.Text = "Replacement For Damaged License";
            this.userControlShowReplacementApplicationDetails1.SetApplicationTypeID((int)this._ReplaceReason);

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
                this._LicenseID = Convert.ToInt32(txtLicenseID.Text);
            }
            else return;



            // check if exist and get license info
            if ((_LicenseInfo = clsLicense.FindLicense(_LicenseID)) != null)
            {
                this.userControlDrivingLicenseInfo1.SetLicenseID(this._LicenseID);
                this.linkLabelShoLicensesHistory.Enabled = true;



                    // check activation 
                    if (_LicenseInfo.IsActive)
                    {
                      this.userControlShowReplacementApplicationDetails1.SetOldLicenseIDAndUsername(this._LicenseID, clsGlobal.CurrentUser.UserName);

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





        private void btnReplace_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to replace the license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                clsLicense NewLicense = this._LicenseInfo.Replace((clsLicense.enIssueReason)this._ReplaceReason, clsGlobal.CurrentUser.UserID);
                if (NewLicense != null)
                {

                    MessageBox.Show($"License has renewed successfully with ID [{NewLicense.LicenseID}]", "License Renewed", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    linkLabelShowLicnesInfo.Enabled = true;
                    this.btnClose.Visible = true;
                    this.btnCanel.Visible = false;
                    this.btnReplae.Visible = false;
                    GroupBoxFilter.Enabled = false;

                    linkLabelShowLicnesInfo.Tag = NewLicense.LicenseID;


                    this.userControlShowReplacementApplicationDetails1.SetReplacementdAppAndLicenseID(NewLicense.ApplicationID, NewLicnseID: NewLicense.LicenseID);
                    this.userControlDrivingLicenseInfo1.SetLicenseID(NewLicense.LicenseID);
                }

            }

        }



        private void txtLicenseID_TextChanged(object sender, EventArgs e)
        {
            this._LicenseInfo = null;
            this.linkLabelShoLicensesHistory.Enabled = false;

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
            FormShowLicense formShow = new FormShowLicense((int)this.linkLabelShowLicnesInfo.Tag);
            formShow.ShowDialog();
        }

    }
}
