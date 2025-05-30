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
    public partial class FormShowPersonDetails : Form
    {
  



        public FormShowPersonDetails(int PersonID)
        {
            InitializeComponent();
            this.userControlShowPersonDetails1.SetPersonID(PersonID);
            this.userControlShowPersonDetails1.Close = CloseShowDetails;
           //this.userControlShowPersonDetails1.ReFresh = Refresh;
        }
        private void CloseShowDetails()
        {
            this.Close();
        }

        private void userControlShowPersonDetails1_Load(object sender, EventArgs e)
        {

        }

        //private void ReFreshTheForm()
        //{
        //    this.Refresh();
        //}
    }
}
