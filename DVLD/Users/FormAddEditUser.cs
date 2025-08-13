using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using BusinessLayer;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;

namespace DVLD
{
    public partial class FormAddEditUser : Form
    {


        int PersonID = -1;
        int UserID = -1;

        bool IsEdit = false;

        bool Validation  = true;


        clsUser clsUser;

        public FormAddEditUser(int UserID)
        {
            InitializeComponent();
            this.UserID = UserID;

            this.userConShopPersonDetailWithFilter1.SendPersonIDBack = RecievedPersonID;
        }

        // recieved person id using event OnPersonSelected
        private void userConShopPersonDetailWithFilter1_OnPersonSelected(int obj)
        {

        }

        // recieved person id using delelgate
        private void RecievedPersonID(int PersonID)
        {
            this.PersonID = PersonID;
        }

        private void FormAddUser_Load(object sender, EventArgs e)
        {

            if(this.UserID != -1)
            {
                lblHeadLine.Text = "Edit User";
                LoadUserInfoToEdit();
                IsEdit = true;
            }
            else
            {
                lblHeadLine.Text = "Add User";
                clsUser = new clsUser();
                this.userConShopPersonDetailWithFilter1.ResetPersonData();
            }

        }
        private void LoadUserInfoToEdit()
        {
             clsUser = clsUser.FindUser(this.UserID);
            this.PersonID = clsUser.PersonID;
            this.userConShopPersonDetailWithFilter1.LoadPersonInfo(PersonID);
            this.userConShopPersonDetailWithFilter1.SetPersonIDInTextBox(PersonID.ToString());
            this.userConShopPersonDetailWithFilter1.EnabledGroupBoxSearchPerson(false);

            txtPassword.Text = clsUser.Password;
            txtConfirmPassword.Text = clsUser.Password;
            txtUserName.Text = clsUser.UserName;
            lblUserIDValue.Text = clsUser.UserID.ToString();
            chBIsActive.Checked = clsUser.IsActive;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {

            if(IsEdit){ tabControl1.SelectedTab = tabPageLoginInfo; return; }


         //   this.PersonID = this.userConShopPersonDetailWithFilter1.GetPersonID();


            if (tabControl1.SelectedIndex == 0)
            {
                if (clsUser.IsUserExistByPersonID(PersonID))
                {
                    MessageBox.Show("This person is already user.","Already user",MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (PersonID == -1)
                {
                    MessageBox.Show("You must retrieve the person's information first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                } else tabControl1.SelectedTab = tabPageLoginInfo;

            }


        }

        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (IsEdit) return;

            if(tabControl1.SelectedIndex == 1)
            {

                //this.PersonID = this.userConShopPersonDetailWithFilter1.GetPersonID();

                if (clsUser.IsUserExistByPersonID(PersonID))
                {
                    MessageBox.Show("This person is already user.", "Already user", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Cancel = true;
                    return;
                }

                if (PersonID == -1)
                {
                    MessageBox.Show("You must get person information first", "Erorr", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Cancel = true;
                }
            }
        }
        



        private void txtUserName_Validating(object sender, CancelEventArgs e)
        {

            if (string.IsNullOrEmpty(txtUserName.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtUserName, "This field is required!");
                return;
            }
            else
            {
                errorProvider1.SetError(txtUserName, null);
            }

            if (this.clsUser.UserName != txtUserName.Text.Trim() &&  clsUser.IsUserExist(txtUserName.Text.Trim()))
            {
                errorProvider1.SetError(txtUserName, "this username is already used");
                this.Validation = false;
            }
            else
            {
                errorProvider1.SetError(txtUserName, string.Empty);
                this.Validation = true;
            }
        }

        private bool IsPasswordValid()
        {
            if (string.IsNullOrEmpty(txtPassword.Text.Trim())) return false;

            if (txtPassword.Text.Trim() == txtConfirmPassword.Text.Trim()) return true; else return false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            //if (!this.ValidateChildren())
            //{
            //    MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;

            //}

            if (!Validation)
            {
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }



            if (!IsPasswordValid())
            {
                MessageBox.Show("The password does not match. Please try again.", "Validation Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; 
            }
         
            clsUser.PersonID = this.PersonID;
            clsUser.UserName = txtUserName.Text;
            clsUser.Password = txtPassword.Text;
            clsUser.IsActive = chBIsActive.Checked;

            if (clsUser.Save())
            {
                MessageBox.Show("User data has been saved successfully.", "Save Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.lblUserIDValue.Text = clsUser.UserID.ToString();
                btnSave.Visible = false;
                btnCancel.Visible = false;
                txtConfirmPassword.Enabled = false;
                txtPassword.Enabled = false;
                txtUserName.Enabled = false;
                this.chBIsActive.Enabled = false;
                this.IsEdit = true;

            }
            else
            {
                MessageBox.Show("User data could not be saved. Please try again.", "Save Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

       

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtUserName_TextChanged(object sender, EventArgs e)
        {

        }


    }
}
