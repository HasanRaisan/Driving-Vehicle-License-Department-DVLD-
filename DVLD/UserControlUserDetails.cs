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
        }


        public void LoadUserInfo()
        {
            clsUserBusinessLayer clsUser = clsUserBusinessLayer.FindUser(this.UserID);

            if (clsUser == null)
            {
                MessageBox.Show("No user information found. Please check and try again.", "User Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            lblUserIDValue.Text = clsUser.UserID.ToString();
            lblUsernameValue.Text = clsUser.UserName.ToString();
            lblIsActiveValue.Text = clsUser.IsActive ? "Yes" : "No";
        }


     
    }
}
