using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using BusinessLayer;

namespace DVLD
{
    public partial class ucnAddEitedPerson : UserControl
    {

        private int PersonID = -1;
        clsPersonBusinessLayer clsPerson = new clsPersonBusinessLayer();
        
        private bool IsEmailValid = true;

        // to ensure save the picture that user has chosen not the defult 
        private bool SavePicture = false;
        private string _ImagePath= string.Empty;
        private DataTable dtPerson = clsPersonBusinessLayer.GetPeople();


        public ucnAddEitedPerson()
        {
            InitializeComponent();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;

        }
        public void SetPersonID(int personID)
        {
            this.PersonID = personID;
        }

        private void ucnAddEitedPerson_Load(object sender, EventArgs e)
        {
            LoadComboBoxCountries();
            LoadGenralStaf();


            if (PersonID != -1) { LoadPersonInfoForEdit(); linlRemovePicture.Visible = true; }


        }
        private void LoadGenralStaf()
        {
            lblHeadline.Text = PersonID == -1 ? "Add Person" : "Edit Person";
            if (PersonID != -1) lblPersonIDValue.Text = PersonID.ToString();
            else{ 
                cbCountry.SelectedIndex = 90; rbMale.Checked = true;
                        linlRemovePicture.Visible = false;
            }


            
        }
        private void RadioButtonChangeImage(object sender, EventArgs e)
        {
            SavePicture = false;
            if (rbFemale.Checked) 
            {
                ProfileImage.Image = Image.FromFile("C:\\Programming\\DVLD Icons\\person_woman.png");
            }
            else
            {
                ProfileImage.Image = Image.FromFile("C:\\Programming\\DVLD Icons\\person_man.png");
            }
        }

        private void LoadPersonInfoForEdit()
        {
            clsPerson =  clsPersonBusinessLayer.FindPerson(PersonID);
            SavePicture = true;
            if (clsPerson != null)
            {
                txbFirstName.Text=clsPerson._FirstName;
                txbAddress.Text=clsPerson._Address;

               if (clsPerson._Email != null)
               {
                    txbEmail.Text = clsPerson._Email;
               }
                txbSecondName.Text=clsPerson._SecondName;
                txbLastName.Text=clsPerson._LastName;
                txbThirdName.Text=clsPerson._ThirdName;
                txbPhone.Text=clsPerson._Phone;
                cbCountry.SelectedIndex = clsPerson._NationalityCountryID-1;
                dtpDateOfBirth.Value = clsPerson._DateOfBirth;
                txbNationalNo.Text = clsPerson._NationalNo;

                if(!string.IsNullOrEmpty(clsPerson._ImagePath) && System.IO.File.Exists(clsPerson._ImagePath))
                {ProfileImage.Image = Image.FromFile(clsPerson._ImagePath);}

                if (clsPerson._Gendor) rbFemale.Checked = true; else rbMale.Checked = true;          
            }
        }

       public delegate void ColseTheForm();
       public ColseTheForm Close;
        private void btnClose_Click(object sender, EventArgs e)
        {
            Close.Invoke();
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!IsAllInfoComplete()) return;

            clsPerson._FirstName = txbFirstName.Text;
            clsPerson._SecondName = txbSecondName.Text;
            clsPerson._ThirdName = txbThirdName.Text;
            clsPerson._LastName = txbLastName.Text;
            clsPerson._DateOfBirth = dtpDateOfBirth.Value;
            clsPerson._Phone = txbPhone.Text;
            clsPerson._NationalNo = txbNationalNo.Text;
            clsPerson._Address = txbAddress.Text;

            clsPerson._NationalityCountryID = clsCountryBusinessLayer.FindCountry(cbCountry.SelectedIndex+1).CountryID;
            if (IsEmailValid) clsPerson._Email = txbEmail.Text; else return;

            if (SavePicture )
            {
               clsPerson._ImagePath = _ImagePath;
            }
            else{ MessageBox.Show("Picture is requird."); return; }

            clsPerson._Gendor = rbFemale.Checked ? false : true;

            if (clsPerson.Save())
            {
                if (PersonID == -1)
                MessageBox.Show("Person detail has saved successfully");
                else MessageBox.Show("Person detail has edited successfully");                this.Close();

                SendPersonID.Invoke(clsPerson._PersonID);

            }        
        }

        public delegate int DlSendPersonID(int PersonID);
        public DlSendPersonID SendPersonID;



        private void linlSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {               
            openFileDialogProfilePicture.AddExtension = true;
            openFileDialogProfilePicture.CheckFileExists = true;
            openFileDialogProfilePicture.CheckPathExists = true;
            openFileDialogProfilePicture.Title = "Chose the picture.";
            openFileDialogProfilePicture.Filter = "Picture Files (*.PNG; *.JPG; *.GIF; *.PMB;)|*.PNG; *.JPG; *.GIF; *.PMB;";
            if(openFileDialogProfilePicture.ShowDialog() == DialogResult.OK)
            {
                SavePicture = true;
                ProfileImage.Image = Image.FromFile(openFileDialogProfilePicture.FileName);
                _ImagePath = openFileDialogProfilePicture.FileName;
                linlRemovePicture.Visible = true;
            }
        }


        private void TextBoxKeyPrassOnlyLettersAndControl(object sender, KeyPressEventArgs e)
        {

            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; 
            }
        }

        private void TextBoxKeyPrassDontStartWithSpace(object sender, KeyPressEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox.Text.Length == 0 && e.KeyChar == ' ') 
            {
                e.Handled = true;
            }
        }

        private void ChangeBackColorToRed(TextBox text)
        {
            text.BackColor = Color.FromArgb(255, 255, 145, 145);
        }
        private void ChangeBackColorToWhite(TextBox text)
        {
            text.BackColor = Color.White;

        }
        private bool IsAllInfoComplete()
        {
            bool isCom = true;
            string TExtBoxName = string.Empty;
            if (txbAddress.Text == "" )
            {
                isCom = false;
                TExtBoxName = "Address";
                ChangeBackColorToRed(txbAddress);
            }
            else ChangeBackColorToWhite(txbAddress);


            if (txbNationalNo.Text == "")
            {
                isCom = false;
                TExtBoxName += ", National No.";
                ChangeBackColorToRed(txbNationalNo);
            }
            else ChangeBackColorToWhite(txbNationalNo);

            if (txbPhone.Text == "")
            {
                isCom = false;
                TExtBoxName += ", Phone";
                ChangeBackColorToRed(txbPhone);
            }
            else ChangeBackColorToWhite(txbPhone);

            if (txbFirstName.Text == "" )
            {
                isCom = false;
                TExtBoxName += ", First Name";
                ChangeBackColorToRed(txbFirstName);
            }
            else ChangeBackColorToWhite(txbFirstName);

            if (txbSecondName.Text == "")
            {
                isCom = false;
                TExtBoxName += ", Second Name";
                ChangeBackColorToRed(txbSecondName);
            }
            else ChangeBackColorToWhite(txbSecondName);

            if (txbThirdName.Text == "")
            {
                isCom = false;
                TExtBoxName += ", Third Name";
                ChangeBackColorToRed(txbThirdName);
            }
            else ChangeBackColorToWhite(txbThirdName);

            if (txbLastName.Text == "")
            {
                isCom = false;
                TExtBoxName += ", Last Name";
                ChangeBackColorToRed(txbLastName);
            }
            else ChangeBackColorToWhite(txbLastName);

           



            if (TExtBoxName.StartsWith(","))
            {  
                TExtBoxName = TExtBoxName.Remove(0,1);
            }

            if (!isCom)
            {    
                if (TExtBoxName.Contains(", "))
                {
                    MessageBox.Show($"{TExtBoxName} are requird.");
                }
                else
                {
                    MessageBox.Show($"{TExtBoxName} is requird");
                }
            }
           return isCom;
        }



        private bool IsNationalNoUsed(string NationalNo)
        {
            foreach (DataRow row in dtPerson.Rows)
            {
                if(NationalNo == row["National No"].ToString())
                {
                    return true;
                }
            }
            return false;
        }
        private void txbNationalNo_Validating(object sender, CancelEventArgs e)
        {
            
            if (IsNationalNoUsed(txbNationalNo.Text))
            {
                errorProvider.SetError(txbNationalNo,"This nationl No. is already used");
                e.Cancel = true;

            }
            else
            {
                errorProvider.SetError(txbNationalNo,string.Empty);
                e.Cancel = false;

            }
        }

        
        private void txbEmail_Validating(object sender, CancelEventArgs e)
        {
            if (txbEmail.Text.Length > 0)
            {
                if (!txbEmail.Text.Contains("@"))
                {
                    errorProvider.SetError(txbEmail, "This Email is not valid");
                    IsEmailValid = false;
                }
                else
                {
                    errorProvider.SetError(txbEmail, string.Empty);
                    IsEmailValid = true;
                }
            }
            else
            {
                errorProvider.SetError(txbEmail, string.Empty);
                IsEmailValid = true;
            }
        }
            
        private void LoadComboBoxCountries()
        {
            DataTable dtCountris = clsCountryBusinessLayer.GetCountries();
            
            foreach ( DataRow c in dtCountris.Rows)
            {
                cbCountry.Items.Add(c["CountryName"]);
            }
        }

        

        private void linlRemovePicture_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            RadioButtonChangeImage(sender, e);
            linlRemovePicture.Visible = false;
        }

        private void txbEmail_TextChanged(object sender, EventArgs e)
        {

        }

        private void txbNationalNo_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
