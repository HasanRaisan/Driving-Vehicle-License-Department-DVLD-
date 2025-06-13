using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using BusinessLayer;
using DVLD.Applications;
using DVLD;
using DVLD.Driving_Licenses_Services;
using DVLD.Mange_Applications;

namespace DVLD
{
    public partial class FormLoginScreen : Form
    {
        public FormLoginScreen()
        {
            InitializeComponent();
        }

        clsUsers clsUser = new clsUsers();

        bool IsUserActive = false;

        private bool CheckUsernameAndPasswordValidition()
        {
            string Username = txtUserName.Text;
            string Password = txtPassword.Text;

            bool IsValied = true;

            if (Username == "") { errorProvider1.SetError(txtUserName, "Type the Username"); IsValied = false; }
            else errorProvider1.SetError(txtUserName, string.Empty);

            if (Password == "") { errorProvider1.SetError(txtPassword, "Type the Password"); IsValied = false; }
            else errorProvider1.SetError(txtPassword, string.Empty);

            return IsValied;
        }

        private bool _CheckUsernameAndPassword()
        {
            clsUser = clsUsers.FindByUsernameAndPassword(txtUserName.Text.Trim(),txtPassword.Text.Trim());
            if (clsUser != null)
            {
                IsUserActive = clsUser.IsActive;

                if (cbRememberMe.Checked)
                {
                    //store username and password
                    clsGlobal.RememberUsernameAndPassword2(txtUserName.Text.Trim(), txtPassword.Text.Trim());
                }
                else
                {
                    //remove stored username and password
                    clsGlobal.RememberUsernameAndPassword2("", "");
                }
                return true;

            }
            else return false;
        }

      



        private void btnLogin_Click(object sender, EventArgs e)
        {

            if (CheckUsernameAndPasswordValidition())
            {

                if (_CheckUsernameAndPassword())
                {

                    if (IsUserActive)
                    {
                        clsGlobal.CurrentUser = clsUser;
                        this.Hide();
                        MainForm mainForm = new MainForm(this);
                        mainForm.ShowDialog();

                    }
                    else
                    {

                        MessageBox.Show("This user is not active.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtUserName.Text = "";
                        txtPassword.Text = "";
                        txtUserName.Focus();    
                    }

                }
                else
                {
                    MessageBox.Show("The username or password you entered is incorrect.", " Wrong Credintials", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    txtUserName.Text = "";
                    txtPassword.Text = "";
                    txtUserName.Focus();
                }

            }
        }

        private void FormLoginScreen_Load(object sender, EventArgs e)
        {
            string UserName = "", Password = "";

            if (clsGlobal.GetStoredCredential2(ref UserName, ref Password))
            {
                txtUserName.Text = UserName;
                txtPassword.Text = Password;
                cbRememberMe.Checked = true;
            }
            else
                cbRememberMe.Checked = false;

        }
    }

}
