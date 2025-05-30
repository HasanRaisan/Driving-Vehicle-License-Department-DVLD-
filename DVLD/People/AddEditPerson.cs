using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD
{
    public partial class AddEditPerson : Form
    {
        public AddEditPerson(int personID /* -1 if add new */)
        {
            InitializeComponent();

            if( personID == -1 ) 
                this.Text = "Add Person";
            else
            { 
                this.Text = "Edit Person Infromation";
                this.ucnAddEitedPerson1.SetPersonID(personID);

            }


            /// Get Person ID From USER CONTROL After Saving A New Person By DELEGATE 
            this.ucnAddEitedPerson1.SendPersonID = GetPersonID;


        }

        public delegate int DlSendPersonIBack(int PersonID);
        static public DlSendPersonIBack SendPersonIDFromForm;

        int GetPersonID(int PersonID)
        {
            /// Send Person ID (Back) To user control from This Form
            /// After The Recieving From User Control By DELEGATE 

            if (SendPersonIDFromForm != null)
            {
                SendPersonIDFromForm.Invoke(PersonID);
            }
            return PersonID;          
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
