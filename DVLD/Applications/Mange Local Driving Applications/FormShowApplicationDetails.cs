using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Mange_Applications
{
    public partial class FormShowApplicationDetails : Form
    {
        public FormShowApplicationDetails(int LDLAppID)
        {
            InitializeComponent();
            this.UserConShowBasicApplicationInfo1.SetLDLAppID(LDLAppID);
        }

      
    }
}
