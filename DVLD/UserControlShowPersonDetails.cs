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
    public partial class UserControlShowPersonDetails : UserControl
    {
        private int PersonID = -1;

        public UserControlShowPersonDetails()
        {
            InitializeComponent();
        }

        public void SetPersonID(int PersonID)
        {
            this.PersonID = PersonID;
        }


        public delegate void ColseTheForm();
        public ColseTheForm Close;

        private void btnClose_Click_1(object sender, EventArgs e)
        {
            Close.Invoke();
        }

        

        private clsPersonBusinessLayer GetPerson()
        {
            clsPersonBusinessLayer Person = clsPersonBusinessLayer.FindPerson(this.PersonID);

            if (Person != null)
            {
                return Person;
            }
            else
            {
                return null;
            }
        }


        public void RestData()
        {


            lblPersonIDValue.Text = "???";
            lblName.Text = "???";
            lblNationlNo.Text = "???";
            lblAddress.Text = "???";
            lblDateOfBirth.Text = "???";
            lblPhone.Text = "???";

            ProfileImage.Image = Image.FromFile("C:\\Programming\\DVLD Icons\\person_woman.png");
            //ProfileImage.Image = Image.FromFile("C:\\Programming\\DVLD Icons\\person_man.png");

            lblGender.Text = "???";
            lblEmail.Text = "???";
            lblCountry.Text = "???";

            EnabledLinkLabelEditPerson(false);


        }
 
        public void LoadPersonDetail()
        {
            clsPersonBusinessLayer Person = GetPerson();


            if (Person == null)
            {
                //  MessageBox.Show("Person data not found.");

                return;
            }

            lblPersonIDValue.Text = Person._PersonID.ToString();
            lblName.Text = Person.FullName();
            lblNationlNo.Text = Person._NationalNo;
            lblAddress.Text = Person._Address;
            lblDateOfBirth.Text = Person._DateOfBirth.ToString();
            lblPhone.Text = Person._Phone;

            if (!string.IsNullOrEmpty(Person._ImagePath) && System.IO.File.Exists(Person._ImagePath))
            {
                ProfileImage.Image = Image.FromFile(Person._ImagePath);
            } else
            {
                if (Person._Gendor) ProfileImage.Image = Image.FromFile("C:\\Programming\\DVLD Icons\\person_woman.png");
                else ProfileImage.Image = Image.FromFile("C:\\Programming\\DVLD Icons\\person_man.png");
            }
            lblGender.Text = Person._Gendor ? "Female" : "Male";

            if (Person._Email != null) lblEmail.Text = Person._Email;
            lblCountry.Text = clsCountryBusinessLayer.FindCountry(Person._NationalityCountryID).CountryName.ToString();
        }
       

        private void UserControlShowPersonDetails_Load_1(object sender, EventArgs e)
        {
           LoadPersonDetail();
        }


        private void linlEditPerson_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int PersonID = GetPerson()._PersonID;
            AddEditPerson EidtPerson = new AddEditPerson(PersonID);
            
            EidtPerson.ShowDialog();
            LoadPersonDetail();

           
        }

        public void EnabledLinkLabelEditPerson(bool EnablOrDisEnabled)
        {
            linlEditPerson.Enabled = EnablOrDisEnabled;
        }

      
    }
}
