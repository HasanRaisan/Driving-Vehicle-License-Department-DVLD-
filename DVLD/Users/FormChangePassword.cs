using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using BusinessLayer;

namespace DVLD
{
    public partial class FormChangePassword : Form
    {
        int UserID = -1; 
        clsUser clsUser = new clsUser();

        bool Validation = true;

        public FormChangePassword(int UserID)
        {
            InitializeComponent();
            this.UserID = UserID;

            clsUser = clsUser.FindUser(UserID);

            this.userControlShowPersonDetails1.SetPersonID(clsUser.PersonID);
            this.userControlShowPersonDetails1.EnabledLinkLabelEditPerson(true);

            this.userControlUserDetails1.SetUserID(clsUser.UserID);

        }


        private void txtCurrentPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtCurrentPassword.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtCurrentPassword, "Username cannot be blank");
                Validation = false;

                return;
            }


            if (clsUser.Password != txtCurrentPassword.Text.Trim())
            {
                errorProvider1.SetError(txtCurrentPassword, "Current password is not correct");
                Validation = false;

            }
            else
            { 
                errorProvider1.SetError(txtCurrentPassword, string.Empty);
                Validation = true;
            }
        }


        private void _RestData()
        {
            txtConfirmNewPassword.Text = string.Empty;
            txtCurrentPassword.Text = string.Empty;
            txtNewPassword.Text = string.Empty;

            txtCurrentPassword.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.Validation)
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            clsUser.Password= txtNewPassword.Text;

            if (clsUser.Save())
            {
                MessageBox.Show("The password has been changed successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _RestData();
            }
            else
            {
                MessageBox.Show("Failed to change the password. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void txtNewPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtNewPassword.Text.Trim()))
            {
                errorProvider1.SetError(txtNewPassword, "New Password cannot be blank");
                Validation = false;
            }
            else
            {
                errorProvider1.SetError(txtNewPassword, null);
                Validation = true;
            };

        }

        private void txtConfirmNewPassword_Validating(object sender, CancelEventArgs e)
        {


            if (string.IsNullOrEmpty(txtConfirmNewPassword.Text.Trim()))
            {
                errorProvider1.SetError(txtConfirmNewPassword, "Username cannot be blank");
                Validation = false;

                return;
            }

            if (txtConfirmNewPassword.Text.Trim() != txtNewPassword.Text.Trim())
            {
                e.Cancel = true;
                errorProvider1.SetError(txtConfirmNewPassword, "Password Confirmation does not match New Password!");
                Validation = false;
            }
            else
            {
                errorProvider1.SetError(txtConfirmNewPassword, null);
                Validation = true;
            };
        }

        private void txtCurrentPassword_Validated(object sender, EventArgs e)
        {

        }
    }
}
