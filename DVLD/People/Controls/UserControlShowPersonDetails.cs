using BusinessLayer;
using DVLD.Properties;
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
            this.LoadPersonDetail();
        }


        public delegate void ColseTheForm();
        public ColseTheForm Close;

        private void btnClose_Click_1(object sender, EventArgs e)
        {
            Close.Invoke();
        }


        private clsPerson GetPerson()
        {
            clsPerson Person = clsPerson.FindPerson(this.PersonID);

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

            this.PersonID = -1;
            lblPersonIDValue.Text = "???";
            lblName.Text = "???";
            lblNationlNo.Text = "???";
            lblAddress.Text = "???";
            lblDateOfBirth.Text = "???";
            lblPhone.Text = "???";

            ProfileImage.Image = Image.FromFile("C:\\Programming\\DVLD Icons\\person_man.png");
            //pbPersonPicture.Image = Image.FromFile("C:\\Programming\\DVLD Icons\\person_man.png");

            lblGender.Text = "???";
            lblEmail.Text = "???";
            lblCountry.Text = "???";

            EnabledLinkLabelEditPerson(false);


        }
 
        public void LoadPersonDetail()
        {
            clsPerson Person = GetPerson();


            if (Person == null)
            {
                 MessageBox.Show("Person data was not found.","Missing Data",MessageBoxButtons.OK,MessageBoxIcon.Error);

                return;
            }

            lblPersonIDValue.Text = Person._PersonID.ToString();
            lblName.Text = Person.FullName();
            lblNationlNo.Text = Person._NationalNo;
            lblAddress.Text = Person._Address;
            lblDateOfBirth.Text = Person._DateOfBirth.ToString("dd/MMM/yyyy");
            lblPhone.Text = Person._Phone;

            if (!string.IsNullOrEmpty(Person._ImagePath) && System.IO.File.Exists(Person._ImagePath))
            {
                ProfileImage.ImageLocation =Person._ImagePath;
            } else
            {
                if (Person._Gendor) ProfileImage.Image = Resources.person_man;
                else ProfileImage.Image = Resources.admin_female;
            }
            lblGender.Text = Person._Gendor ? "Male" : "Female";

            if (Person._Email != null) lblEmail.Text = Person._Email;
            lblCountry.Text = clsCountryBusinessLayer.FindCountry(Person._NationalityCountryID).CountryName.ToString();
        }
       

        private void UserControlShowPersonDetails_Load_1(object sender, EventArgs e)
        {
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
