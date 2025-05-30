using BusinessLayer;
using DVLD.Mange_Applications;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Serveces
{
    public partial class FormDetainLicense : Form
    {

        string _username = string.Empty;

        private int _licenseID = -1;


        private int _personID = -1;
        private int GetPersonIDForLicenseID()
        {
            if (_personID == -1)
            {
                _personID = clsLicenses.GetPersonIDByLicenseID(this._licenseID);
            }
            return _personID;
        }

        private int _userID = -1;
        private int GetUserID()
        {
            if (_userID == -1)
            {
                _userID = clsUsers.GetUserIDByUserName(this._username);
            }
            return _userID;
        }

        clsLicenses _clsLicenseInfo = null;
        private clsLicenses LicenseInfo()
        {
            if (this._clsLicenseInfo == null)
                this._clsLicenseInfo = clsLicenses.FindLicense(this._licenseID);

            return this._clsLicenseInfo;
        }



        public FormDetainLicense(string username)
        {
            this._username = username;
            InitializeComponent();
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
            FormShowLicense formShow = new FormShowLicense(this._licenseID);
            formShow.ShowDialog();
        }



        private void txtLicenseID_KeyPress(object sender, KeyPressEventArgs e)
        {

            //e.Handled = !char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar);

            // when you press ENTER it works but the id disapper becouse it create a new line like when we press enter 

            if (e.KeyChar == (char)13)
            {
                btnSearch.PerformClick();
            }

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtLicenseID.Text.Trim()))
            {
                this._licenseID = int.Parse(txtLicenseID.Text.Trim());
            }
            else return;




            // check if exist and get license info
            if ((this._clsLicenseInfo = clsLicenses.FindLicense(this._licenseID)) != null)
            {
                this.userControlDrivingLicenseInfo1.SetLicenseID(this._licenseID);
                this.linkLabelShoLicensesHistory.Enabled = true;

                if (!clsDetainLicense.IsLicenseDetainedByLicenseID(this._licenseID))
                {
                    this.userControlDetainInfo1.SetUsernameAndLicenseID(this._username, this._licenseID);
                    this.btnDetain.Enabled = true;
                    this.userControlDetainInfo1.FocusTextFees();
                }
                else
                {
                    MessageBox.Show($"Selected License is alredy deatined", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.userControlDetainInfo1.SetDefualtValue();
                    btnDetain.Enabled = false;
                    return;
                }
            }
            else
            {
                this.userControlDrivingLicenseInfo1.LoadDefaultData();
                this.userControlDetainInfo1.SetDefualtValue();
                this.linkLabelShoLicensesHistory.Enabled = false;
                this.btnDetain.Enabled = false;

                return;
            }
        }



        private void btnDetain_Click(object sender, EventArgs e)
        {

            decimal FineFees = this.userControlDetainInfo1.GetFineFeesValue();


            if (FineFees == -1)
            {
                MessageBox.Show("You must enter a fine amount.", "Input Required", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var detain = new clsDetainLicense();
            detain.LicenseID = this._licenseID;
            detain.CreatedByUserID = GetUserID();
            detain.DetainDate = DateTime.Now;
            detain.FineFees = FineFees;

            if (detain.Save())
            {
                MessageBox.Show("The license has been successfully detained.", "Detention Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);



                this.userControlDrivingLicenseInfo1.SetLicenseID(this._licenseID);
                this.userControlDetainInfo1.SetDetainID(detain.DetainID);
                this.btnDetain.Enabled = false;
                this.btnCanel.Enabled = false;
                this.btnClose.Enabled = true;

                this.linkLabelShowLicnesInfo.Enabled = true;

                this.userControlDetainInfo1.SetFineFeesTextBoxDisEnabled();
                this.GroupBoxFilter.Enabled = false;
            }
            else
            {
                MessageBox.Show("Failed to detain the license. Please try again.", "Detention Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        private void DefualtValue()
        {
            this._clsLicenseInfo = null;
            this._personID = -1;
            this._userID = -1;

            this.btnDetain.Enabled = false;
            this.linkLabelShoLicensesHistory.Enabled = false;
            this.linkLabelShowLicnesInfo.Enabled = false;

            this.userControlDetainInfo1.SetDefualtValue();
            this.userControlDrivingLicenseInfo1.LoadDefaultData();
        }

        private void txtLicenseID_TextChanged(object sender, EventArgs e)
        {
            DefualtValue();
        }

        private void FormDetainLicense_Load_1(object sender, EventArgs e)
        {
            this.btnClose.Visible = false;
            this.btnDetain.Enabled = false;
            this.linkLabelShoLicensesHistory.Enabled = false;
            this.linkLabelShowLicnesInfo.Enabled = false;
            this.userControlDetainInfo1.SetDefualtValue();



        }


    }
}
