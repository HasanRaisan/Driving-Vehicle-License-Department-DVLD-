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

    public delegate void BoolValueChangedDelegate(bool newValue,int PersonID);


    public partial class UserConShopPersonDetailWithFilter : UserControl
    {

        public BoolValueChangedDelegate BoolValueChanged;

        int PersonID =-1;
        public UserConShopPersonDetailWithFilter()
        {
            InitializeComponent();
           
        }

        private void btnAddPerson_Click(object sender, EventArgs e)
        {

            AddEditPerson AddPerson = new AddEditPerson(-1);
            AddEditPerson.SendPersonIDFromForm = GetPersonID;

            AddPerson.ShowDialog();

           

        }
        private int GetPersonID(int PersonID)
        {
            this.PersonID = PersonID;

            LoadPersonInfo(PersonID);
            txtFindBy.Text = PersonID.ToString();
            groupBox1.Enabled = false;

            // to add user form ?? so we will be able to press NEXT (swap between pages) 

            if (BoolValueChanged !=  null)BoolValueChanged(true,PersonID);


            return PersonID;
        }

        clsPersonBusinessLayer clsPerson = new clsPersonBusinessLayer();

        private void GetTheUser()
        {
            if (cbFindBy.SelectedIndex == 1)
            {
                clsPerson = clsPersonBusinessLayer.FindPerson(txtFindBy.Text.ToString());               
            }
            else
            {
               int.TryParse(txtFindBy.Text, out int PersonID);
               clsPerson = clsPersonBusinessLayer.FindPerson(PersonID);
            }

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {//÷/////////////÷/////
            ///////////// new clsPerson
            GetTheUser();
            if (clsPerson != null)
            {
                this.PersonID = clsPerson._PersonID;
                LoadPersonInfo(clsPerson._PersonID);
            }
            else
            {
                MessageBox.Show("Person is not found","Infromation",MessageBoxButtons.OK,MessageBoxIcon.Information);
                this.userControlShowPersonDetails1.RestData();
            }
            
        }

        public void LoadPersonInfo(int PersonID)
        {

            this.userControlShowPersonDetails1.SetPersonID(PersonID);
            this.userControlShowPersonDetails1.LoadPersonDetail();
            this.userControlShowPersonDetails1.EnabledLinkLabelEditPerson(true);
        }

        public int GetPersonID()
        {
          //  return clsPerson == null ? 0 : clsPerson._PersonID;
          //  return clsUser._PersonID > 0? clsUser._PersonID : -1;
          //b


            // from both add new and search
            return this.PersonID;
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
    }
}
