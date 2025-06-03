using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using BusinessLayer;

namespace DVLD
{
    public partial class FormShowUserDetails : Form
    {
        int UserID = 0;
        public FormShowUserDetails(int UserID)
        {
            InitializeComponent();
            this.UserID = UserID;
            
        }


        void FormShowUserDetails_Load(object sender, EventArgs e)
        {
            this.userControlShowPersonDetails1.SetPersonID(clsUsers.FindUser(this.UserID).PersonID);
            this.userControlUserDetails1.SetUserID(this.UserID);
            
            this.userControlShowPersonDetails1.Close = CloseShowDetails;
        }

        private void CloseShowDetails()
        {
            this.Close();
        }


    }
}
