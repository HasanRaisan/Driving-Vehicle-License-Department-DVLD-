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
using DVLD.Driving_Licenses_Services;
using DVLD.Mange_Applications;

namespace DVLD
{
    public partial class FormLoginScreen : Form
    {
        public FormLoginScreen()
        {
            InitializeComponent();
            var culture = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
        }

        clsUserBusinessLayer clsUser = new clsUserBusinessLayer();

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

        private bool CheckLogin()
        {
            clsUser = clsUserBusinessLayer.FindUser(txtUserName.Text);
            if (clsUser != null)
            {
                if (clsUser.Password == txtPassword.Text)
                {
                    IsUserActive = clsUser.IsActive;
                    return true;
                }
                else return false;
            }
            else return false;
        }

        private void CloseThisForm()
        {
            this.Close();
        }



        private void btnLogin_Click(object sender, EventArgs e)
        {

            if (CheckUsernameAndPasswordValidition())
            {

                if (CheckLogin())
                {

                    if (IsUserActive)
                    {
                        this.Hide();

                        MainForm mainForm = new MainForm(clsUser);
                        mainForm.ShowDialog();

                    }
                    else
                    {

                        MessageBox.Show("This user is not active.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtUserName.Text = "";
                        txtPassword.Text = "";
                    }

                }
                else
                {
                    MessageBox.Show("The username or password you entered is incorrect.", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    txtUserName.Text = "";
                    txtPassword.Text = "";
                }

            }
        }






      
    }

}
