using System;
using System.Data;
using System.IO;
using System.Runtime.CompilerServices;

using DataAccessLayer;


namespace BusinessLayer
{
    public class clsPerson
    {

        public int _PersonID { get; set; }
        public string _NationalNo          { get; set; }
        public string _FirstName           { get; set; }
        public string _SecondName          { get; set; }
        public string _ThirdName           { get; set; }
        public string _LastName            { get; set; }
        public DateTime _DateOfBirth       { get; set; }
        public short _Gendor                { get; set; }
        public string _Address { get; set; }
        public string _Phone               { get; set; }
        public string _Email               { get; set; }
        public int _NationalityCountryID   { get; set; }
        public string _ImagePath           { get; set; }
        private Status _Status { get; set; } 

        private enum Status { AddNew = 0, Update = 1 };

        public string FullName()
        {
            return _FirstName+" " + _SecondName + " " + _ThirdName + " " + _LastName;
        }

        public clsPerson() {
            this._Status = Status.AddNew;
            this._NationalNo = "";
            this._ImagePath = "";
            this._Email = "";
        }

        private clsPerson(int PersonID ,string NationalNo, string FirstName, string SecondName, string ThirdName,
              string LastName, DateTime DateOfBirth, short Gendor, string Address, string Phone, string Email,
              int NationalityCountryID, string ImagePath)
        {
            this._PersonID = PersonID;
            this._NationalNo = NationalNo;
            this._FirstName = FirstName;
            this._SecondName = SecondName;
            this._ThirdName = ThirdName;
            this._LastName = LastName;
            this._DateOfBirth = DateOfBirth;
            this._Gendor = Gendor;
            this._Address = Address;
            this._Phone = Phone;
            this._Email = Email;
            this._NationalityCountryID = NationalityCountryID;
            this._ImagePath = ImagePath;
            this._Status = Status.Update;

        }



        static  public clsPerson FindPerson(int PersonID)
        {

            if (clsPersonDataAccess.IsPersonExistByPersonID(PersonID)) 
            {
                string _NationalNo = "";
                string _FirstName = "";
                string _SecondName = "";
                string _ThirdName = "";
                string _LastName = "";
                DateTime _DateOfBirth = DateTime.Now;
                short _Gendor = 0;
                string _Address = "";
                string _Phone = "";
                string _Email = "";
                int _NationalityCountryID = -1;
                string _ImagePath = "";

              clsPersonDataAccess.FindPersonByID(PersonID, ref _NationalNo, ref _FirstName, ref _SecondName, ref _ThirdName, ref _LastName
                    , ref _DateOfBirth, ref _Gendor, ref _Address, ref _Phone, ref _Email, ref _NationalityCountryID, ref _ImagePath);

                return new  clsPerson( PersonID,_NationalNo, _FirstName,  _SecondName, _ThirdName, _LastName
                    , _DateOfBirth, _Gendor, _Address, _Phone, _Email, _NationalityCountryID, _ImagePath);

            }
          else { return null; }
        }

        static public clsPerson FindPerson(string NationalID)
        {

            if (clsPersonDataAccess.IsPersonExist(NationalID))
            {
                int _PersonID = -1;
                string _FirstName = "";
                string _SecondName = "";
                string _ThirdName = "";
                string _LastName = "";
                DateTime _DateOfBirth = DateTime.Now;
                short _Gendor = 0;
                string _Address = "";
                string _Phone = "";
                string _Email = "";
                int _NationalityCountryID = -1;
                string _ImagePath = "";

                clsPersonDataAccess.FindPersonByNationalNo(NationalID, ref _PersonID,  ref _FirstName, ref _SecondName, ref _ThirdName, ref _LastName
                      , ref _DateOfBirth, ref _Gendor, ref _Address, ref _Phone, ref _Email, ref _NationalityCountryID, ref _ImagePath);

                return new clsPerson(_PersonID, NationalID, _FirstName, _SecondName, _ThirdName, _LastName
                    , _DateOfBirth, _Gendor, _Address, _Phone, _Email, _NationalityCountryID, _ImagePath);

            }
            else { return null; }
        }

        private bool _AddNewPerson()
        {
            this._PersonID = clsPersonDataAccess.AddPerson(this._NationalNo, this._FirstName, this._SecondName, this._ThirdName, this._LastName,
              this._DateOfBirth, this._Gendor, this._Address, this._Phone, this._Email, this._NationalityCountryID, this._ImagePath);
          return (this._PersonID !=- 1);
        }

        private bool _UpdatePerson()
        {
            return clsPersonDataAccess.UpdatePerson(this._PersonID, this._NationalNo, this._FirstName, this._SecondName, this._ThirdName, this._LastName,
                this._DateOfBirth, this._Gendor, this._Address, this._Phone, this._Email, this._NationalityCountryID, this._ImagePath);
        }

        static public bool IsPersonExist(int PersonID)
        {
            return clsPersonDataAccess.IsPersonExistByPersonID(PersonID);
        }

        static public bool IsPersonExist(string NationalNo)
        {
            return clsPersonDataAccess.IsPersonExist(NationalNo);
        }

        static public DataTable GetPeople()
        {
            return  clsPersonDataAccess.GetPeople();
        }

        static public bool DeletePerson(int PersonID)
        {
            return clsPersonDataAccess.DeletePerson(PersonID);
        }

        static public bool IsNationalNoExists(string NationalNo)
        {
            return clsPersonDataAccess.IsNationalNoExists(NationalNo);
        }

        static public string GetNatioanlNoByID(int PersonID)
        {
            return clsPersonDataAccess.GetNationalNoByPersonID((int)PersonID);
        }
        static public int GetIDByNationalNO(string NationalNo)
        {
            return clsPersonDataAccess.GetPersonIDByNationalNo(NationalNo);
        }

        public bool Save()
        {
            switch (this._Status)
            {
                case Status.AddNew: this._Status = Status.Update; return _AddNewPerson();
                case Status.Update: return _UpdatePerson();
                default: return false;

            }
        }
    }

    public class clsCountryBusinessLayer
    {
        public string CountryName { get; }
        public int CountryID { get; }

       public clsCountryBusinessLayer() { }
      private  clsCountryBusinessLayer(string countryName, int countryID)
        {
            CountryName = countryName;
            CountryID = countryID;
        }

        static public clsCountryBusinessLayer FindCountry(int CountryID)
        {
            string CountryName = "";

            if (clsCountriesDataAccess.FindCountry(CountryID, ref CountryName))
                return new clsCountryBusinessLayer(CountryName,CountryID);
            else return null;
            

            
        }
        static public clsCountryBusinessLayer FindCountry(string countryName)
        {
            int CountryID = -1;
            if (clsCountriesDataAccess.FindCountry(countryName, ref CountryID)) 
                return new clsCountryBusinessLayer(countryName, CountryID); else return null;

        }

        static public DataTable GetCountries()
        {
            return clsCountriesDataAccess.GetAllCountries();
        }

       


    }

}
