using BusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace DVLD
{
    public partial class UserControlUserDetails : UserControl
    {
        int UserID = -1;
        public UserControlUserDetails()
        {
            InitializeComponent();
        }

        public void SetUserID(int UserID)
        {
            this.UserID = UserID;
            LoadUserInfo();
        }


        private void LoadUserInfo()
        {
            clsUser clsUser = clsUser.FindUser(this.UserID);

            if (clsUser == null)
            {
                MessageBox.Show("No user information found. Please check and try again.", "User Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            lblUserIDValue.Text = clsUser.UserID.ToString();
            lblUsernameValue.Text = clsUser.UserName.ToString();
            lblIsActiveValue.Text = clsUser.IsActive ? "Yes" : "No";
        }

        private void linlEditUserInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var editUser = new FormAddEditUser(this.UserID);
            editUser.ShowDialog();
            LoadUserInfo();

        }
    }
}
