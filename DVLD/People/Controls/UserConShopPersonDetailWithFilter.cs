using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using BusinessLayer;

namespace DVLD
{



    public partial class UserConShopPersonDetailWithFilter : UserControl
    {

        public delegate void DelSendPersonIDBack(int PersonID);
        public DelSendPersonIDBack SendPersonIDBack;

        public event Action<int> OnPersonSelected;

        public void PersonSelected(int PersonID)
        {
            Action<int> Handler = OnPersonSelected;
            if (Handler != null)
                Handler(PersonID);
        }

        int PersonID =-1;
        public UserConShopPersonDetailWithFilter()
        {
            InitializeComponent();
           
        }

        private void btnAddPerson_Click(object sender, EventArgs e)
        {
            AddEditPerson AddPerson = new AddEditPerson(-1);
            AddEditPerson.SendPersonIDFromForm = RecievPersonIDFormAddNewPerson;
            AddPerson.ShowDialog();
        }
        private bool IsPersonExist()
        {
            if (cbFindBy.SelectedIndex == 1)
            {
                if (clsPerson.IsPersonExist(txtFindBy.Text.ToString()))
                {
                    this.PersonID = clsPerson.GetIDByNationalNO(txtFindBy.Text.ToString());
                    return true;
                }
            }
            else
            {
               int.TryParse(txtFindBy.Text, out int PersonID);
               if(clsPerson.IsPersonExist(PersonID))
               {
                    this.PersonID = PersonID;
                    return true;
               }
            }

            return false;
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {

            if (IsPersonExist())
            {
                LoadPersonInfo(this.PersonID);
            }
            else
            {
                MessageBox.Show("Person is not found","Infromation",MessageBoxButtons.OK,MessageBoxIcon.Information);
                this.PersonID = -1;
                this.userControlShowPersonDetails1.RestData();

            }
            
        }
        public void LoadPersonInfo(int PersonID)
        {
            // send person id using delegate
            if (SendPersonIDBack != null) SendPersonIDBack.Invoke(PersonID);

            // send person id usin EVNT OnPersonSelected
            PersonSelected(PersonID);


            this.userControlShowPersonDetails1.SetPersonID(PersonID);
            this.userControlShowPersonDetails1.EnabledLinkLabelEditPerson(true);
        }
        private void UserConShopPersonDetailWithFilter_Load(object sender, EventArgs e)
        {
            cbFindBy.Items.Add("Person ID");
            cbFindBy.Items.Add("Nationl ID");
            cbFindBy.SelectedIndex = 0;
            this.userControlShowPersonDetails1.EnabledLinkLabelEditPerson(false);
        }
        public void EnabledGroupBoxSearchPerson(bool Enable)
        {
            groupBox1.Enabled = Enable;
        }
        public void SetPersonIDInTextBox(string PersonID)
        {
            this.txtFindBy.Text = PersonID; 
        }    
        private void TextBoxKeyPressOnlyNumbers(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void TextBoxKeyPressOnlyLettersOrNumbers(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetterOrDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void cbFindBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFindBy.SelectedIndex == 0)
            {
                txtFindBy.KeyPress -= TextBoxKeyPressOnlyLettersOrNumbers;
                txtFindBy.KeyPress += TextBoxKeyPressOnlyNumbers;

            }
            else
            {
                txtFindBy.KeyPress -= TextBoxKeyPressOnlyNumbers;
                txtFindBy.KeyPress += TextBoxKeyPressOnlyLettersOrNumbers;

            }
        }

        // two function for get person id (1- add new person  2- search)
        private int RecievPersonIDFormAddNewPerson(int PersonID)
        {
            this.PersonID = PersonID;

            LoadPersonInfo(PersonID);
            txtFindBy.Text = PersonID.ToString();
            groupBox1.Enabled = false;
            //if (DelSendPersonIDBack !=  null) DelSendPersonIDBack.Invoke(PersonID);
            return PersonID;
        }
        public int GetPersonID()
        {
           // from both add new and search
            return this.PersonID;
        }

        public void FilterFocus()
        {
            this.txtFindBy.Focus();
        }

    }
}
